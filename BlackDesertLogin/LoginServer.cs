using BlackDesertLogin.network;
using NLog;

namespace BlackDesertLogin
{
    class LoginServer
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();
        protected static TcpServer Server;

        static void Main()
        {
            LogManager.Configuration = Utils.Funcs.NLogDefaultConfiguration;

            Server = new TcpServer("127.0.0.1",8888,100);
            Server.BeginListening();
        }
    }
}
