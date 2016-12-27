namespace Vsd.Master
{
    using System;

    using SharpGL;

    using Vsd.Common;

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

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            openGlControl = new OpenGLControl();

            ((System.ComponentModel.ISupportInitialize)(openGlControl)).BeginInit();

            SuspendLayout();

            openGlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            openGlControl.DrawFPS = true;
            openGlControl.FrameRate = 5;
            openGlControl.Location = new System.Drawing.Point(0, 0);
            openGlControl.Name = "openGLControl";
            openGlControl.RenderContextType = RenderContextType.FBO;
            openGlControl.Size = new System.Drawing.Size(Resources.X, Resources.Y);
            openGlControl.TabIndex = 0;

            openGlControl.OpenGLInitialized += new EventHandler(OpenGlControlOpenGlInitialized);
            openGlControl.OpenGLDraw += new RenderEventHandler(OpenGlControlOpenGlDraw);
            openGlControl.Resized += new EventHandler(OpenGlControlResized);

            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(Resources.X, Resources.Y);
            Controls.Add(value: openGlControl);

            Name = "SharpGLForm";
            Text = "Master";

            ((System.ComponentModel.ISupportInitialize)(openGlControl)).EndInit();

            ResumeLayout(false);
        }
    }
}

