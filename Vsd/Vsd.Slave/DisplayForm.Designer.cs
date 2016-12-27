namespace Vsd.Slave.Leaf
{
    using System;

    using SharpGL;

    using Vsd.Common;
    using Vsd.Slave.Leaf.Slaves.Utils;

    partial class DisplayForm
    {
        private System.ComponentModel.IContainer components = null;

        private OpenGLControl openGlControl;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent(Settings settings)
        {
            components = new System.ComponentModel.Container();

            openGlControl = new OpenGLControl();

            ((System.ComponentModel.ISupportInitialize)(openGlControl)).BeginInit();

            SuspendLayout();

            openGlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            openGlControl.DrawFPS = true;
            openGlControl.FrameRate = 5;
            openGlControl.Location = new System.Drawing.Point(0, 0);
            openGlControl.Name = "openGLControl" + settings.InternalId;
            openGlControl.RenderContextType = RenderContextType.FBO;
            openGlControl.Size = new System.Drawing.Size(Resources.X, Resources.Y);
            openGlControl.TabIndex = 0;

            openGlControl.OpenGLInitialized += new EventHandler(OpenGlControlOpenGlInitialized);
            openGlControl.OpenGLDraw += new RenderEventHandler(OpenGlControlOpenGlDraw);
            openGlControl.Resized += new EventHandler(OpenGlControlResized);

            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(Resources.X, Resources.Y);
            Controls.Add(value: openGlControl);

            Name = "SharpGLForm" + settings.InternalId;
            Text = "Slave Form " + settings.InternalId;

            ((System.ComponentModel.ISupportInitialize)(openGlControl)).EndInit();

            ResumeLayout(false);
        }
    }
}

