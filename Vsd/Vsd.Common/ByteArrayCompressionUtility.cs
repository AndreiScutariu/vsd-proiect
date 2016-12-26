namespace Vsd.Common
{
    using System.IO;
    using System.IO.Compression;

    public static class ByteArrayCompressionUtility
    {
        private const int BufferSize = 64 * 1024; 

        public static byte[] Compress(this byte[] inputData)
        {
            using (var compressIntoMs = new MemoryStream())
            {
                using (
                    var gzs = new BufferedStream(new GZipStream(compressIntoMs, CompressionMode.Compress), BufferSize))
                {
                    gzs.Write(inputData, 0, inputData.Length);
                }
                return compressIntoMs.ToArray();
            }
        }

        public static byte[] Decompress(this byte[] inputData)
        {
            using (var compressedMs = new MemoryStream(inputData))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (
                        var gzs = new BufferedStream(
                            new GZipStream(compressedMs, CompressionMode.Decompress),
                            BufferSize))
                    {
                        gzs.CopyTo(decompressedMs);
                    }
                    return decompressedMs.ToArray();
                }
            }
        }
    }
}