namespace Vsd.Common
{
    public class Resources
    {
        public const int X = 600;

        public const int Y = 400;

        public static int MasterMulticastPort = 45678;

        public static string MasterMulticastIp = "127.0.0.1";

        public static int NodeMulticastPort = 45679;

        public static string NodeMulticastIp = "127.0.0.1";

        public static int PixelsSize => X * Y;

        public static int RgbPixelsSize => X * Y * 3;

        public static int DepthPixelsSize => X * Y * 4;
    }
}