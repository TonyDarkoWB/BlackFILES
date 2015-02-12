using BlackDesertGame.network;
using NLog;

namespace BlackDesertGame
{
    class GameServer
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();
        protected static TcpServer Server;

        static void Main()
        {
            LogManager.Configuration = Utils.Funcs.NLogDefaultConfiguration;

            Server = new TcpServer("127.0.0.1",8889,100);
            Server.BeginListening();
        }
    }
}
