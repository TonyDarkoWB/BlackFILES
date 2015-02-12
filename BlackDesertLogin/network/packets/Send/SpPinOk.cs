using System.IO;

namespace BlackDesertLogin.network.packets.Send
{
    class SpPinOk : ASendPacket
    {
        //01 11 00 BC 0B 00 14 C3 00 81 DA 05 78 9B 0E 00 ...&#188;...&#195;.&#218;.x›..
        //00 .

        public SpPinOk()
        {
            
        }
        public override void Write(BinaryWriter writer)
        {
            WriteB(writer, "0014C30081DA05789B0E0000");
        }
    }
}
