using BlackDesertGame.network.packets.Send;

namespace BlackDesertGame.network.packets.Recv
{
    class RpCharacterList : ARecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
           new SpCharacterList().Send(Connection, 1);
        }
    }
}
