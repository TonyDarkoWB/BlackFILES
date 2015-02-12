using Hik.Communication.Scs.Server;

namespace BDCommon.network
{
    public interface IConnection
    {
        IScsServerClient Client { get; set; }

        bool IsValid { get; }

        void CloseConnection(bool force = false);
        void SendDatas(byte[] data);
    }
}
