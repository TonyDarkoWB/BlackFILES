using BlackDesertLogin.network.packets.Send;

namespace BlackDesertLogin.network.packets.Recv
{
    class RpLogIn : ARecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
          new SpLogIn().Send(Connection);
        }
    }
}
