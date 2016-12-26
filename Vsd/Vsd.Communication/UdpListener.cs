namespace Vsd.Communication
{
    using System.Net;
    using System.Net.Sockets;

    public class UdpListener : UdpBase
    {
        public UdpListener(IPEndPoint endpoint)
        {
            Client = new UdpClient(endpoint);
        }
    }
}