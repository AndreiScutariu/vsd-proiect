namespace Vsd.Slave.Node.Components
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    using Vsd.Common;
    using Vsd.Communication;

    public class AggregatePixelsComponent
    {
        private const byte LeftKey = 1;

        private const byte RightKey = 2;

        private readonly int slaveNodeId;

        private readonly ConcurrentDictionary<int, byte[]> pixelContainer;

        private readonly UdpUser connectionToParrent;

        private readonly byte[] leftDepthsBytes = new byte[Resources.Dps];

        private readonly byte[] rightDepthsBytes = new byte[Resources.Dps];

        private readonly byte[] pixelsToSend = new byte[4 + Resources.Rps + Resources.Dps];

        private float[] leftDepths = new float[Resources.Ps];

        private float[] rightDepths = new float[Resources.Ps];

        public AggregatePixelsComponent(
            int slaveNodeId,
            ConcurrentDictionary<int, byte[]> pixelContainer,
            int parrentPort)
        {
            this.slaveNodeId = slaveNodeId;
            this.pixelContainer = pixelContainer;
            connectionToParrent = UdpUser.ConnectTo(Resources.LocalIp, parrentPort);
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    #region copy depths

                    Buffer.BlockCopy(pixelContainer[LeftKey], Resources.Rps, leftDepthsBytes, 0, Resources.Dps);
                    leftDepths = leftDepthsBytes.ConvertToFloatArray();

                    Buffer.BlockCopy(pixelContainer[RightKey], Resources.Rps, rightDepthsBytes, 0, Resources.Dps);
                    rightDepths = rightDepthsBytes.ConvertToFloatArray();

                    #endregion copy depths

                    Buffer.BlockCopy(BitConverter.GetBytes(slaveNodeId), 0, pixelsToSend, 0, 4);

                    var pIdx = 4;
                    var dIdx = 4 + Resources.Rps;

                    for (var i = 0; i < Resources.Ps; i++)
                    {
                        byte[] pixelRef = leftDepths[i] < rightDepths[i]
                                              ? pixelContainer[LeftKey]
                                              : pixelContainer[RightKey];

                        pixelsToSend[pIdx] = pixelRef[pIdx++];
                        pixelsToSend[pIdx] = pixelRef[pIdx++];
                        pixelsToSend[pIdx] = pixelRef[pIdx++];

                        pixelsToSend[dIdx] = pixelRef[dIdx++];
                        pixelsToSend[dIdx] = pixelRef[dIdx++];
                        pixelsToSend[dIdx] = pixelRef[dIdx++];
                        pixelsToSend[dIdx] = pixelRef[dIdx++];
                    }

                    connectionToParrent.Send(pixelsToSend.Compress());
                }
                catch
                {
                }
            }
        }
    }
}