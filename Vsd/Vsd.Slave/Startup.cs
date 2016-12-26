namespace Vsd.Slave
{
    using System;
    using System.Windows.Forms;

    using Vsd.Slave.Slaves;

    internal static class Startup
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var slaveOne = new SlaveOne();

            Application.Run(new DisplayForm(slaveOne));
        }
    }
}