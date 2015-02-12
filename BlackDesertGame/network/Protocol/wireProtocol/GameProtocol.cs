using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlackDesertGame.network.Protocol.message;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;

namespace BlackDesertGame.network.Protocol.wireProtocol
{
    class GameProtocol : IScsWireProtocol
    {
        protected MemoryStream Stream = new MemoryStream();


        public byte[] GetBytes(IScsMessage message)
        {
            return ((Message)message).Data;
        }

        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            Stream.Write(receivedBytes, 0, receivedBytes.Length);
            List<IScsMessage> messages = new List<IScsMessage>();
            while (ReadMessage(messages)) ;

            return messages;
        }

        public void Reset()
        {
        }

        private bool ReadMessage(List<IScsMessage> messages)
        {
            Stream.Position = 0;
            if (Stream.Position == Stream.Length || Stream.Length < 3)
                return false;

            var lenArray = new byte[3];
            Stream.Read(lenArray, 0, 3);

            ushort len = BitConverter.ToUInt16(lenArray, 1);

            //If connection buffer is to small for recv full game packet, just write it into base stream
            if (len > Stream.Length)
            {
                Stream.Position = Stream.Length;
                return false;
            }

            byte[] datas = new byte[len - 3];

            Stream.Read(datas, 0, datas.Length);

            messages.Add(new Message
            {
                OpCode = BitConverter.ToUInt16(datas, 0),
                Data = datas.Skip(2).ToArray()
            });

            TrimStream();

            return true;
        }

        private void TrimStream()
        {
            if (Stream.Position == Stream.Length)
            {
                Stream = new MemoryStream();
                return;
            }

            byte[] remaining = new byte[Stream.Length - Stream.Position];
            Stream.Read(remaining, 0, remaining.Length);
            Stream = new MemoryStream();
            Stream.Write(remaining, 0, remaining.Length);
        }
    }
}
