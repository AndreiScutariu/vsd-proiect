namespace Vsd.Master
{
    using System;
    using System.Windows.Forms;

    using SharpGL;

    using Vsd.Common;

    public partial class DisplayForm : Form
    {
        public DisplayForm()
        {
            InitializeComponent();

            PixelsBuffer = new byte[Resources.RgbPixelsSize];
        }

        public byte[] PixelsBuffer { get; set; }

        // ReSharper disable once InconsistentNaming
        private OpenGL OpenGL => openGlControl.OpenGL;

        private void OpenGlControlOpenGlDraw(object sender, RenderEventArgs e)
        {
            OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            OpenGL.LoadIdentity();
            OpenGL.Scale(0.1, 0.1, 0.1);

            OpenGL.DrawPixels(Resources.X, Resources.Y, OpenGL.GL_RGB, PixelsBuffer);
        }

        private void OpenGlControlOpenGlInitialized(object sender, EventArgs e)
        {
            OpenGL.ClearColor(0, 0, 0, 0);
        }

        private void OpenGlControlResized(object sender, EventArgs e)
        {
            OpenGL.MatrixMode(OpenGL.GL_PROJECTION);
            OpenGL.LoadIdentity();
            OpenGL.Viewport(0, 0, Width, Height);
            OpenGL.Perspective(45.0f, Width / (double)Height, 1, 200.0);
            OpenGL.LookAt(0, 0, -15, 0, 0, 0, 0, 1, 0);
            OpenGL.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}