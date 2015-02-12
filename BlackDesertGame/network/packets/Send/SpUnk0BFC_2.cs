﻿using System.IO;

namespace BlackDesertGame.network.packets.Send
{
    class SpUnk0BFC_2 : ASendPacket
    {
        public override void Write(BinaryWriter writer)
        {
            string data = ("010000010100003C4A0000"
  + "D2E8C60070A1C500F9AE47046788BE00"
  + "00000080BF763F0400D8000000E70000"
  + "00B8F6FFFFFFFFFFFFB8F6FFFFFFFFFF"
  + "FFB8F6FFFFFFFFFFFF71946DE4950000"
  + "00000000000000000000000000000300"
  + "00000000000000000000001003781800"
  + "00890043000000000000000100000095"
  + "1BC16FF2862300020000000100000003"
  + "0000000000000000006929000000FCFF"
  + "FF1C0000000000000000000000000556"
  + "01000000000000000000000000000000"
  + "00000000000000000000000000000000"
  + "00000000000000000000FCFFFF01140C"
  + "0000");
            WriteB(writer, data);
        }
    }
}