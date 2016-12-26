namespace Vsd.Slave.Slaves
{
    using SharpGL;

    public class SlaveOne : ISlave
    {
        public Settings Settings { get; set; }

        public void Draw(OpenGL gl, double rotation)
        {
            var quad = gl.NewQuadric();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Scale(0.1, 0.1, 0.1);

            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            gl.Translate(0, 1, 30);
            gl.Sphere(quad, 12, 20, 20);
        }
    }
}