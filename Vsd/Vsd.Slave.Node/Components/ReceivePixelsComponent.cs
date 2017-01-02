namespace Vsd.Slave.Node.Components
{
    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using System.Threading;

    using Vsd.Common;
    using Vsd.Communication;

    public class ReceivePixelsComponent : IDisposable
    {
        private readonly ConcurrentDictionary<int, byte[]> pixelContainer;

        private readonly UdpListener listener;

        private byte[] receivedCompressed;

        private byte[] received;

        public ReceivePixelsComponent(ConcurrentDictionary<int, byte[]> pixelContainer, int receivePort)
        {
            this.pixelContainer = pixelContainer;
            listener = new UdpListener(new IPEndPoint(IPAddress.Any, receivePort));
        }

        public async void Start()
        {
            while (true)
            {
                receivedCompressed = await listener.ReceiveBytes();
                received = receivedCompressed.Decompress();

                var slaveId = BitConverter.ToInt32(received, 0);

                if (pixelContainer.ContainsKey(slaveId))
                {
                    pixelContainer[slaveId] = received;
                }
                else
                {
                    pixelContainer.TryAdd(slaveId, received);
                }

                Thread.Sleep(10);
            }
        }

        public void Dispose()
        {
            listener.Close();
        }
    }
}