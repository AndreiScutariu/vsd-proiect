namespace Vsd.Communication
{
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<byte[]> ReceiveBytes()
        {
            var result = await Client.ReceiveAsync();

            return result.Buffer;
        }

        public void Close()
        {
            Client.Close();
        }
    }
}