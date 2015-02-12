using System;
using System.Collections.Generic;
using BlackDesertLogin.network.packets.Recv;
using BlackDesertLogin.network.packets.Send;

namespace BlackDesertLogin.network.Protocol
{
    class OpCodes
    {
        public static Dictionary<ushort, Type> Recv = new Dictionary<ushort, Type>
        {
            {0x0bb9, typeof(RpLogIn)},
            {0x0bbb, typeof(RpEnterPin)},
        };
        public static Dictionary<Type, ushort> Send = new Dictionary<Type, ushort>
        {
              {typeof (SpLogIn), 0x0bba},
              {typeof (SpPinOk), 0x0bbc},
              {typeof (SpConnectionInfo), 0x0bbf},
        };
    }
}
