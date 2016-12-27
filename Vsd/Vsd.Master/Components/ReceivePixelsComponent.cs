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

                //var slaveIdBytes = new byte[4];
                var rgbPixelsBytes = new byte[Resources.Rps];
                //var depthPixelsBytes = new byte[Resources.DepthPixelsSize];

                //Buffer.BlockCopy(received, 0, slaveIdBytes, 0, 4);
                Buffer.BlockCopy(received, 0, rgbPixelsBytes, 0, Resources.Rps);
                //Buffer.BlockCopy(received, 4 + Resources.RgbPixelsSize, depthPixelsBytes, 0, Resources.DepthPixelsSize);

                Buffer.BlockCopy(rgbPixelsBytes, 0, pixelsContainer, 0, pixelsContainer.Length);

                Thread.Sleep(10);
            }
        }
    }
}