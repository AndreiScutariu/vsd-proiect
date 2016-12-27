namespace Vsd.Slave.Leaf.Slaves
{
    using SharpGL;

    using Vsd.Slave.Leaf.Slaves.Utils;

    public class CircularSphere : ISlave
    {
        public Settings Settings { get; set; }

        public void Draw(OpenGL gl, double rotation)
        {
            var quad = gl.NewQuadric();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Scale(0.1, 0.1, 0.1);

            gl.Color(Settings.DrawColor);

            gl.Rotate(rotation, Settings.DrawRotation);
            gl.Translate(0, 1, Settings.InternalId * 20);
            gl.Sphere(quad, 12, 200, 200);
        }
    }
}