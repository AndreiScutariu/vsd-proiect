namespace Vsd.Slave.Node.Components
{
    using System;
    using System.Collections.Concurrent;

    using Vsd.Common;
    using Vsd.Communication;

    public class AggregatePixelsComponent
    {
        private const byte LeftKey = 1;

        private const byte RightKey = 2;

        private readonly ConcurrentDictionary<int, byte[]> pixelContainer;

        private readonly UdpUser connectionToParrent;

        private readonly byte[] leftDepthsBytes = new byte[Resources.Dps];

        private readonly byte[] rightDepthsBytes = new byte[Resources.Dps];

        private float[] leftDepths = new float[Resources.Ps];

        private float[] rightDepths = new float[Resources.Ps];

        public AggregatePixelsComponent(ConcurrentDictionary<int, byte[]> pixelContainer, int parrentPort)
        {
            this.pixelContainer = pixelContainer;
            connectionToParrent = UdpUser.ConnectTo(Resources.LocalIp, parrentPort);
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    Buffer.BlockCopy(pixelContainer[LeftKey], Resources.Rps, leftDepthsBytes, 0, Resources.Dps);
                    Buffer.BlockCopy(pixelContainer[RightKey], Resources.Rps, rightDepthsBytes, 0, Resources.Dps);

                    leftDepths = leftDepthsBytes.ConvertToFloatArray();
                    rightDepths = rightDepthsBytes.ConvertToFloatArray();

                    //Parallel.For(
                    //    0,
                    //    Resources.Ps,
                    //    i =>
                    //        {
                    //            byte[] pixelRef = leftDepths[i] > rightDepths[i]
                    //                                  ? pixelContainer[LeftKey]
                    //                                  : pixelContainer[RightKey];
                    //        });

                    var pixels = new byte[Resources.Rps];
                    var idx = 0;
                    //Buffer.BlockCopy(pixelContainer[LeftKey], 0, pixels, 0, Resources.Rps);
                    for (var i = 0; i < Resources.Ps; i++)
                    {
                        byte[] pixelRef = leftDepths[i] > rightDepths[i]
                                              ? pixelContainer[LeftKey]
                                              : pixelContainer[RightKey];

                        pixels[idx] = pixelRef[idx++];
                        pixels[idx] = pixelRef[idx++];
                        pixels[idx] = pixelRef[idx++];
                    }

                    
                    connectionToParrent.Send(pixels.Compress());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}