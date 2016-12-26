namespace Vsd.Slave.Leaf.Slaves.Utils
{
    using SharpGL;

    public static class Extensions
    {
        public static void Color(this OpenGL gl, int color)
        {
            if (color == 0)
            {
                gl.Color(0f, 0f, 0f);
            }

            if (color == 1)
            {
                gl.Color(1f, 0f, 0f);
            }

            if (color == 2)
            {
                gl.Color(0f, 1f, 0f);
            }

            if (color == 3)
            {
                gl.Color(0f, 0f, 1f);
            }

            if (color == 4)
            {
                gl.Color(0f, 1f, 1f);
            }

            if (color == 5)
            {
                gl.Color(1f, 1f, 0f);
            }

            if (color == 6)
            {
                gl.Color(1f, 0f, 1f);
            }
        }

        public static void Rotate(this OpenGL gl, double rotation, int rotate)
        {
            if (rotate == 0)
            {
                gl.Rotate(rotation, 0f, 1f, 0f);
            }

            if (rotate == 1)
            {
                gl.Rotate(rotation, -1f, 1f, 0f);
            }

            if (rotate == 2)
            {
                gl.Rotate(rotation, -1f, 0f, 0f);
            }

            if (rotate == 3)
            {
                gl.Rotate(rotation, 1f, 0f, 0f);
            }

            if (rotate == 4)
            {
                gl.Rotate(rotation, 1f, 1f, 0f);
            }

            if (rotate == 5)
            {
                gl.Rotate(rotation, 1f, -1f, 0f);
            }

            if (rotate == 6)
            {
                gl.Rotate(rotation, 0f, -1f, 0f);
            }
        }
    }
}