﻿namespace Vsd.Slave.Leaf
{
    using System;
    using System.Windows.Forms;

    using Vsd.Common;
    using Vsd.Communication;
    using Vsd.Slave.Leaf.Components;
    using Vsd.Slave.Leaf.Slaves;
    using Vsd.Slave.Leaf.Slaves.Utils;

    internal static class Startup
    {
        [STAThread]
        private static void Main(string[] argsParam)
        {
            var stringParam = argsParam[0];
            
            var slaveId = int.Parse(stringParam[0].ToString());
            var drawType = int.Parse(stringParam[1].ToString());
            var drawRotation = int.Parse(stringParam[2].ToString());
            var drawColor = int.Parse(stringParam[3].ToString());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var settings = new Settings { SlaveId = slaveId, DrawType = drawType, DrawColor = drawColor, DrawRotation = drawRotation};
            var slave = SlaveFactory.GetSlave(settings);
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