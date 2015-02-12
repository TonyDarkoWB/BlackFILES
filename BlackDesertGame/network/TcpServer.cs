using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BlackDesertGame.network.Protocol;
using BlackDesertGame.Utils;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using NLog;

namespace BlackDesertGame.network
{
    public class TcpServer
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected string BindAddress;
        protected int BindPort;
        protected int MaxConnections;
        protected Dictionary<string, long> ConnectionsTime;

        public IScsServer Server;

        public TcpServer(string bindAddress, int bindPort, int maxConnections)
        {
            BindAddress = bindAddress;
            BindPort = bindPort;
            MaxConnections = maxConnections;
            ConnectionsTime = new Dictionary<string, long>();
        }

        public void BeginListening()
        {
            Log.Info("Start TcpServer at {0}:{1}...", BindAddress, BindPort);
            Server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(BindAddress, BindPort));
            Server.Start();

            Server.ClientConnected += OnConnected;
            Server.ClientDisconnected += OnDisconnected;
        }

        public void ShutdownServer()
        {
            Log.Info("Shutdown TcpServer...");
            Server.Stop();
        }

        protected void OnConnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("Client connected!");
            new Connection(e.Client);
        }

        protected void OnDisconnected(object sender, ServerClientEventArgs e)
        {
            Log.Info("Client disconnected!");

        }
    }
}
