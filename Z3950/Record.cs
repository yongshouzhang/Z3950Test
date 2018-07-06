using System;
using System.Text;
using System.IO;
namespace Z3950
{
    public class Record 
    {

        private IntPtr _record;
        internal Record(IntPtr record, ResultSet resultSet)
        {
            _record = record;
        }

        public byte[] Content
        {
            get
            {
                var length = 99;
                return Yaz.ZOOM_record_get_bytes(_record, "raw", ref length);
            }
        }

        public string JsonResult
        {
            get
            {
                int length = 99;
                return Yaz.ZOOM_record_get_string(_record, "json", ref length);
               
            }
        }

        public RecordSyntax Syntax
        {
            get
            {
                var length = 0;
                var syntax = Yaz.ZOOM_record_get_string(_record, "syntax", ref length);
                if (syntax == null)
                    return RecordSyntax.Unknown;

                return (RecordSyntax) Enum.Parse(typeof(RecordSyntax), syntax, true);
            }
        }

        public string Database
        {
            get
            {
                var length = 0;
                var database = Yaz.ZOOM_record_get_string(_record, "database", ref length);
                return database;
            }
        }

        private bool _disposed = false;

        public void Dispose()
        {
            if (!_disposed)
            {
                _record = IntPtr.Zero;
                _disposed = true;
            }
        }

    }
}