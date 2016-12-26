namespace Vsd.Slave.Leaf
{
    using Vsd.Slave.Leaf.Slaves;
    using Vsd.Slave.Leaf.Slaves.Utils;

    internal static class SlaveFactory
    {
        public static ISlave GetSlave(Settings settings)
        {
            if (settings.DrawType == 1)
            {
                return new CircularSphere { Settings = settings };
            }

            return new StaticSphere { Settings = settings };
        }
    }
}