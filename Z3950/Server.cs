using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Configuration;
namespace Z3950
{
    using Marc;
    public static class BibAttributes
    {
        public static string GetAttributes(this Bib1Attr attr)
        {
            return $"@attr 1={(int)attr} ";
        }
    }
    public enum Bib1Attr
    {
        Title = 4,
        ISBN = 7,
        LocNumber = 12,
        Date = 30,
        Author = 1003,
        Subject = 21,
    }
    public class Server
    {
        #region ×Ö¶Î
        /// <summary> Arbitrary name associated with this Z39.50 endpoint by the user</summary>
        public string Name { get; set; }

        /// <summary> Port for the connection to the Z39.50 endpoint </summary>
        public uint Port { get; set; }

        /// <summary> Name of the database within the Z39.50 endpoint </summary>
        public string DatabaseName { get; set; }

        /// <summary> URI / URL for the connection to the Z39.50 endpoint </summary>
        public string Uri { get; set; }

        /// <summary> Username for the connection to the endpoint, if one is needed </summary>
        public string Username { get; set; }

        /// <summary> Password for the connection to the endpoint, if one is needed </summary>
        public string Password { get; set; }

        /// <summary> Flag indicates if the password should be saved for this connection to the user's 
        /// personal settings </summary>
        public bool SavePasswordFlag { get; set; }

        #endregion

        #region  ¹¹Ôìº¯Êý
        public Server(string name, string uri, uint port, string databaseName,string userName,string password)
        {
            Name = name;
            Uri = uri;
            Port = port;
            DatabaseName = userName;
            Password = password;
            SavePasswordFlag = false;
        }
    
        public Server()
        {
            Name = string.Empty;
            Uri = string.Empty;
            Port = 0;
            DatabaseName = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            SavePasswordFlag = false;
        }

        public Server(string name)
        {
            ((Action<string, Server>)ConfigurationManager.GetSection("library/server"))(name, this);
        }

        #endregion

        private const string Prefix = "@attrset Bib-1 ";

        private IEnumerable<Record> _getRecord(Bib1Attr attr, string key, int pageSize, int pageIndex, out int count)
        {
            IEnumerable<Record> result = new List<Record>();
            count = 0;
            try
            {
                var connection = new Connection(this.Uri, Convert.ToInt32(this.Port))
                {
                    DatabaseName = this.DatabaseName
                };

                if (!string.IsNullOrEmpty(Username))
                    connection.Username = Username;

                if (!string.IsNullOrEmpty(Password))
                    connection.Password = Password;

                connection.Syntax = RecordSyntax.UsMarc;

                var query = new PrefixQuery(Prefix + attr.GetAttributes() + key);

                ResultSet records = connection.Search(query);
                count = (int)records.Size;
                if (records.Size > 0)
                {
                    int skip = (pageIndex - 1) * pageSize;
                    if (skip + pageSize > records.Size)
                    {
                        skip = (int)records.Size - pageSize;
                    }
                    result = records.Skip(skip).Take(pageSize);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
            }

        public IEnumerable<string> GetRecordsJson(Bib1Attr attr, string key, int pageSize, int pageIndex, out int count)
        {
            return _getRecord(attr, key, pageSize, pageIndex, out count).Select(obj => obj.JsonResult);
        }

        public IEnumerable<MarcRecord> GetRecordsMarc(Bib1Attr attr, string key, int pageSize, int pageIndex, out int count)
        {
            return _getRecord(attr, key, pageSize, pageIndex, out count)
                .Select(obj =>
                {
                    return new MarcRecord(obj.Content);
                });
        }

        #region  ToString()
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}