using System.IO;

namespace BlackDesertLogin.network.packets.Send
{
    class SpConnectionInfo : ASendPacket
    {
        protected string Data =
        "01000B0001004300420054" +
        "0020001CC184BC000000000000000000" +
        "00000000000000000000000000000000" +
        "00000000000000000000000000000000" +
        "0000000000000000003132372E302E30" + 
        "2E3100000000000000B9225A";
       
        public override void Write(BinaryWriter writer)
        {
            WriteB(writer, Data);
        }
    }
}
