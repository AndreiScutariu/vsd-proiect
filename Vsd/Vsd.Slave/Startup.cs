namespace Vsd.Slave
{
    using System;
    using System.Windows.Forms;

    using Vsd.Common;
    using Vsd.Communication;
    using Vsd.Slave.Components;
    using Vsd.Slave.Slaves;

    internal static class Startup
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var slave = new SlaveOne { Settings = new Settings { SlaveId = 1 } };
            var displayForm = new DisplayForm(slave);

            var udpConnection = UdpUser.ConnectTo(Resources.MasterMulticastIp, Resources.MasterMulticastPort);

            var sendPixelsComponent = new SendPixelsComponent(
                udpConnection,
                displayForm.PixelsBuffer,
                displayForm.DepthsBuffer,
                slave.Settings);

            displayForm.SendPixelsComponent = sendPixelsComponent;

            Application.Run(displayForm);

            udpConnection.Close();
        }
    }
}