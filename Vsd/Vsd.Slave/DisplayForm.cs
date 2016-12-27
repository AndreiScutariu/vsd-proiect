namespace Vsd.Slave.Leaf
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SharpGL;

    using Vsd.Common;
    using Vsd.Slave.Leaf.Components;
    using Vsd.Slave.Leaf.Slaves;

    public partial class DisplayForm : Form
    {
        private readonly ISlave slave;

        //private int contor;

        private double rotation;

        public DisplayForm(ISlave slave)
        {
            this.slave = slave;

            InitializeComponent(slave.Settings);

            PixelsBuffer = new byte[Resources.Rps];
            DepthsBuffer = new byte[Resources.Dps];
        }

        public byte[] PixelsBuffer { get; set; }

        public byte[] DepthsBuffer { get; set; }

        public SendPixelsComponent SendPixelsComponent { get; set; }

        // ReSharper disable once InconsistentNaming
        private OpenGL OpenGL => openGlControl.OpenGL;

        private void OpenGlControlOpenGlDraw(object sender, RenderEventArgs e)
        {
            slave.Draw(OpenGL, rotation);
            rotation += 2.0f;

            OpenGL.ReadPixels(0, 0, Resources.X, Resources.Y, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, PixelsBuffer);
            OpenGL.ReadPixels(0, 0, Resources.X, Resources.Y, OpenGL.GL_DEPTH_COMPONENT, OpenGL.GL_FLOAT, DepthsBuffer);

            //if (contor++ < 2)
            //{
            //    return;
            //}

            //contor = 0;

            SendPixelsComponent.Send();
            Thread.Sleep(TimeSpan.FromMilliseconds(10));
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