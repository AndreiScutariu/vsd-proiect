namespace Vsd.Slave.Leaf.Slaves
{
    using SharpGL;

    using Vsd.Slave.Leaf.Slaves.Utils;

    public interface ISlave
    {
        Settings Settings { get; set; }

        void Draw(OpenGL gl, double rotation);
    }
}