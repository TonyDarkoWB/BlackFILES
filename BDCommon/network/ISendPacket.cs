namespace BDCommon.network
{
    public interface ISendPacket
    {
        void Send(IConnection connection,int clientState);
    }
}
