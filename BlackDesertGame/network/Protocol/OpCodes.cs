using System;
using System.Collections.Generic;
using BlackDesertGame.network.packets.Recv;
using BlackDesertGame.network.packets.Send;

namespace BlackDesertGame.network.Protocol
{
    class OpCodes
    {
        public static Dictionary<ushort, Type> Recv = new Dictionary<ushort, Type>
        {
           {0x0bc0, typeof(RpCharacterList)},
           {0x0BDF, typeof(RpEnterWorld)},
        };
        public static Dictionary<Type, ushort> Send = new Dictionary<Type, ushort>
        {
              {typeof (SpCharacterList), 0x0bc1},
              {typeof (SpEnterWorldResponse), 0x0BEE},
              {typeof (SpUnk0E90), 0x0E90},
              {typeof (SpCharacterInfo), 0x0BE0},
              {typeof (SpUnk0BF0), 0x0BF0},
              {typeof (SpUnk0BFC_1), 0x0BFC},
              {typeof (SpUnk0BFC_2), 0x0BFC},
        };
    }
}
