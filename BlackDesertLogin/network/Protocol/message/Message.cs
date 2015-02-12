using Hik.Communication.Scs.Communication.Messages;

namespace BlackDesertLogin.network.Protocol.message
{
    class Message : ScsMessage
    {
        public ushort OpCode;
        public byte[] Data;
    }
}
