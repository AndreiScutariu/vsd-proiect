namespace Vsd.Slave.Slaves
{
    using SharpGL;

    public interface ISlave
    {
        Settings Settings { get; set; }

        void Draw(OpenGL gl, double rotation);
    }
}