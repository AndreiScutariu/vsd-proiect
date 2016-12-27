namespace Vsd.Slave.Node.Components
{
    using System;
    using System.Collections.Concurrent;
    using System.Net;

    using Vsd.Common;
    using Vsd.Communication;

    public class ReceivePixelsComponent : IDisposable
    {
        private readonly ConcurrentDictionary<int, byte[]> pixelContainer;

        private readonly UdpListener listener;

        public ReceivePixelsComponent(ConcurrentDictionary<int, byte[]> pixelContainer, int receivePort)
        {
            this.pixelContainer = pixelContainer;
            listener = new UdpListener(new IPEndPoint(IPAddress.Any, receivePort));
        }

        public async void Start()
        {
            while (true)
            {
                byte[] receivedCompressed = await listener.ReceiveBytes();
                byte[] received = receivedCompressed.Decompress();

                var receivedPixels = new byte[received.Length - 4];
                Buffer.BlockCopy(received, 4, receivedPixels, 0, received.Length - 4);

                var slaveIdBytes = new byte[4];
                Buffer.BlockCopy(received, 0, slaveIdBytes, 0, 4);
                var slaveId = BitConverter.ToInt32(slaveIdBytes, 0);

                if (pixelContainer.ContainsKey(slaveId))
                {
                    pixelContainer[slaveId] = receivedPixels;
                }
                else
                {
                    pixelContainer.TryAdd(slaveId, received);
                }
            }
        }

        public void Dispose()
        {
            listener.Close();
        }
    }
}