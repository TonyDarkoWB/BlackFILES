using Hik.Communication.Scs.Communication.Messages;

namespace BlackDesertGame.network.Protocol.message
{
    class Message : ScsMessage
    {
        public ushort OpCode;
        public byte[] Data;
    }
}
