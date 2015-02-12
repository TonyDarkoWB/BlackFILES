using BlackDesertGame.network.packets.Send;

namespace BlackDesertGame.network.packets.Recv
{
    class RpEnterWorld : ARecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
           new SpEnterWorldResponse().Send(Connection);
          
            new SpUnk0E90().Send(Connection);
            new SpCharacterInfo().Send(Connection, 1);
            new SpUnk0BF0().Send(Connection, 1);
            new SpUnk0BFC_1().Send(Connection);
            new SpUnk0BFC_2().Send(Connection);
        }
    }
}
