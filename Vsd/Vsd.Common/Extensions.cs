namespace Vsd.Common
{
    using System;

    public static class Extensions
    {
        public static float[] ConvertToFloatArray(this byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
            {
                throw new ArgumentException();
            }

            var floats = new float[bytes.Length / 4];

            for (var i = 0; i < floats.Length; i++)
            {
                floats[i] = BitConverter.ToSingle(bytes, i * 4);
            }

            return floats;
        }
    }
}