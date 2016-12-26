namespace Vsd.Master.Components
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Vsd.Common;
    using Vsd.Communication;

    public class Pixel
    {
        public float Depth { get; set; }

        public byte Red { get; set; }

        public byte Green { get; set; }

        public byte Blue { get; set; }
    }

    public class ReceivePixelsComponent
    {
        private readonly UdpListener udpListener;

        private readonly byte[] pixelsContainer;

        private readonly Dictionary<int, Pixel[]> receivedBytesGroupedBySlave = new Dictionary<int, Pixel[]>();

        private readonly Dictionary<int, List<Pixel>> receivedPixelsGroupedByIndex = new Dictionary<int, List<Pixel>>();

        public ReceivePixelsComponent(UdpListener udpListener, byte[] pixelsContainer)
        {
            this.udpListener = udpListener;
            this.pixelsContainer = pixelsContainer;
        }

        public async void Start()
        {
            while (true)
            {
                try
                {
                    byte[] receivedCompressed = await udpListener.ReceiveBytes();
                    byte[] received = receivedCompressed.Decompress();

                    var slaveIdBytes = new byte[4];
                    var rgbPixelsBytes = new byte[Resources.RgbPixelsSize];
                    var depthPixelsBytes = new byte[Resources.DepthPixelsSize];

                    Buffer.BlockCopy(received, 0, slaveIdBytes, 0, 4);
                    Buffer.BlockCopy(received, 4, rgbPixelsBytes, 0, Resources.RgbPixelsSize);
                    Buffer.BlockCopy(
                        received,
                        4 + Resources.RgbPixelsSize,
                        depthPixelsBytes,
                        0,
                        Resources.DepthPixelsSize);

                    var pixels = new Pixel[Resources.PixelsSize];
                    float[] depths = depthPixelsBytes.ConvertToFloatArray();

                    var j = 0;
                    for (var i = 0; i < Resources.PixelsSize; i++)
                    {
                        pixels[i] = new Pixel
                                        {
                                            Depth = depths[i],
                                            Red = rgbPixelsBytes[j++],
                                            Green = rgbPixelsBytes[j++],
                                            Blue = rgbPixelsBytes[j++]
                                        };
                    }

                    var slaveId = BitConverter.ToInt32(slaveIdBytes, 0);

                    if (receivedBytesGroupedBySlave.ContainsKey(slaveId))
                    {
                        receivedBytesGroupedBySlave[slaveId] = pixels;
                    }
                    else
                    {
                        receivedBytesGroupedBySlave.Add(slaveId, pixels);
                    }

                    UpdateMasterPixels();

                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"*** error:" + ex.Message);
                }
            }
        }

        private void UpdateMasterPixels()
        {
            var byteArray = new byte[Resources.RgbPixelsSize];
            var idx = 0;

            for (var i = 0; i < Resources.PixelsSize; i++)
            {
                var pixelToRender = GetPixelToRender(i);

                byteArray[idx++] = pixelToRender.Red;
                byteArray[idx++] = pixelToRender.Green;
                byteArray[idx++] = pixelToRender.Blue;
            }

            Buffer.BlockCopy(byteArray, 0, pixelsContainer, 0, Resources.RgbPixelsSize);
        }

        private Pixel GetPixelToRender(int index)
        {
            var maxPixel = receivedBytesGroupedBySlave[1][index];

            for (var slaveId = 1; slaveId < receivedBytesGroupedBySlave.Count; slaveId++)
            {
                var pixel = receivedBytesGroupedBySlave[slaveId][index];
                if (maxPixel.Depth < pixel.Depth)
                {
                    maxPixel = pixel;
                }
            }

            return maxPixel;
        }
    }
}