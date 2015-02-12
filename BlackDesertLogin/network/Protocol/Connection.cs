using System;
using System.Collections.Generic;
using BDCommon.network;
using BlackDesertLogin.network.packets.Recv;
using BlackDesertLogin.network.Protocol.message;
using BlackDesertLogin.network.Protocol.wireProtocol;
using BlackDesertLogin.Utils;
using Hik.Communication.Scs.Server;
using NLog;

namespace BlackDesertLogin.network.Protocol
{
    public class Connection : IConnection
    {
        protected static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static List<Connection> Connections = new List<Connection>();

        public IScsServerClient Client { get; set; }
        public bool IsValid { get; private set; }

        protected List<byte[]> SendData = new List<byte[]>();

        protected int SendDataSize;

        protected object SendLock = new object();
        public byte[] Buffer;

        public Connection(IScsServerClient client)
        {
            Client = client;
            Client.WireProtocol = new GameProtocol();

            Client.Disconnected += Client_Disconnected;
            Client.MessageReceived += Client_MessageReceived;

            Connections.Add(this);
        }

        void Client_MessageReceived(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
        {
            Message message = (Message)e.Message;
            Buffer = message.Data;

            if (OpCodes.Recv.ContainsKey(message.OpCode))
            {


                ((ARecvPacket)Activator.CreateInstance(OpCodes.Recv[message.OpCode])).Process(this);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(message.OpCode).ToHex();
                Log.Debug("Unknown GsPacket opCode: 0x{0}{1} [{2}]",
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Buffer.Length);

                Log.Debug("Data:\n{0}", Buffer.FormatHex());
            }
        }

        void Client_Disconnected(object sender, EventArgs e)
        {
      
        }
        public void CloseConnection(bool force = false)
        {
            
        }

        public void SendDatas(byte[] data)
        {
            Client.SendMessage(new Message { Data = data });
        }
    }
}
