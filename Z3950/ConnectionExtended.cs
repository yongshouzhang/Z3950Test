using System;
namespace Z3950
{
    public class ConnectionExtended : Connection
    {
        public ConnectionExtended(string host, int port) : base(host, port)
        {
        }

        public Package Package(string type)
        {
            EnsureConnected();

            var options = Yaz.ZOOM_options_create();
            var yazPackage = Yaz.ZOOM_connection_package(ZConnection, options);
            var pack = new Package(yazPackage, this, type);

            return pack;
        }
    }
}