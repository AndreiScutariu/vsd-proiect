namespace Vsd.Slave.Components
{
    using System;

    using Vsd.Common;
    using Vsd.Communication;
    using Vsd.Slave.Slaves;

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
            var pixelToSend = new byte[pixelsReadOnlyContainer.Length + depthsReadOnlyContainer.Length];

            Buffer.BlockCopy(pixelsReadOnlyContainer, 0, pixelToSend, 0, pixelsReadOnlyContainer.Length);
            Buffer.BlockCopy(depthsReadOnlyContainer, 0, pixelToSend, pixelsReadOnlyContainer.Length, depthsReadOnlyContainer.Length);

            byte[] commpressedPixels = pixelToSend.Compress();

            udpUser.Send(commpressedPixels);
        }
    }
}