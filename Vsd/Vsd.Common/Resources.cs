namespace Vsd.Common
{
    public class Resources
    {
        public const int X = 600;

        public const int Y = 400;

        public static int PixelsSize => X * Y;

        public static int RgbPixelsSize => X * Y * 3;

        public static int DepthPixelsSize => X * Y * 4;
    }
}