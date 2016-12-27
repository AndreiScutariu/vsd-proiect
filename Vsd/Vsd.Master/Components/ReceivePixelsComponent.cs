namespace Vsd.Master.Components
{
    using System;
    using System.Threading;

    using Vsd.Common;
    using Vsd.Communication;

    public class ReceivePixelsComponent
    {
        private readonly UdpListener udpListener;

        private readonly byte[] pixelsContainer;

        public ReceivePixelsComponent(UdpListener udpListener, byte[] pixelsContainer)
        {
            this.udpListener = udpListener;
            this.pixelsContainer = pixelsContainer;
        }

        public async void Start()
        {
            while (true)
            {
                byte[] receivedCompressed = await udpListener.ReceiveBytes();
                byte[] received = receivedCompressed.Decompress();
                Buffer.BlockCopy(received, 4, pixelsContainer, 0, Resources.Rps);
                Thread.Sleep(10);
            }
        }
    }
}