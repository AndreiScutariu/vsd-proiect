namespace Vsd.Slave.Leaf.Components
{
    using System;
    using System.Threading;

    using Vsd.Common;
    using Vsd.Communication;
    using Vsd.Slave.Leaf.Slaves;
    using Vsd.Slave.Leaf.Slaves.Utils;

    public class SendPixelsComponent
    {
        private readonly UdpUser udpUser;

        private readonly byte[] pixelsReadOnlyContainer;

        private readonly byte[] depthsReadOnlyContainer;

        private readonly Settings slaveSettings;

        public SendPixelsComponent(
            UdpUser udpUser,
            byte[] pixelsReadOnlyContainer,
            byte[] depthsReadOnlyContainer,
            Settings slaveSettings)
        {
            this.udpUser = udpUser;
            this.pixelsReadOnlyContainer = pixelsReadOnlyContainer;
            this.depthsReadOnlyContainer = depthsReadOnlyContainer;
            this.slaveSettings = slaveSettings;
        }

        public void Send()
        {
            var pixelToSend = new byte[4 + pixelsReadOnlyContainer.Length + depthsReadOnlyContainer.Length];

            Buffer.BlockCopy(BitConverter.GetBytes(slaveSettings.SlaveKey), 0, pixelToSend, 0, 4);
            Buffer.BlockCopy(pixelsReadOnlyContainer, 0, pixelToSend, 4, pixelsReadOnlyContainer.Length);
            Buffer.BlockCopy(depthsReadOnlyContainer, 0, pixelToSend, 4 + pixelsReadOnlyContainer.Length, depthsReadOnlyContainer.Length);

            byte[] commpressedPixels = pixelToSend.Compress();

            udpUser.Send(commpressedPixels);

            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        }
    }
}