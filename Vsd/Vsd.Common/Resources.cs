namespace Vsd.Common
{
    public class Resources
    {
        public const int X = 600;

        public const int Y = 400;

        public static string LocalIp = "127.0.0.1";

        public static string NodeMulticastIp = "127.0.0.1";

        public static int Ps => X * Y;

        public static int Rps => X * Y * 3;

        public static int Dps => X * Y * 4;
    }
}