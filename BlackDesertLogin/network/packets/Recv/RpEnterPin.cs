using BlackDesertLogin.network.packets.Send;

namespace BlackDesertLogin.network.packets.Recv
{
    class RpEnterPin : ARecvPacket
    {
        public override void Read()
        {
            
        }

        public override void Process()
        {
            new SpPinOk().Send(Connection);
            new SpConnectionInfo().Send(Connection);
        }
    }
}
