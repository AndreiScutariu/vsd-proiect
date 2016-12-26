namespace Vsd.Master
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Vsd.Common;
    using Vsd.Communication;
    using Vsd.Master.Components;

    internal static class Startup
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var displayForm = new DisplayForm();

            var listener = new UdpListener(new IPEndPoint(IPAddress.Any, Resources.MasterMulticastPort));

            var receivePixelsComponent = new ReceivePixelsComponent(listener, displayForm.PixelsBuffer);
            Task.Run(() => receivePixelsComponent.Start());

            Application.Run(displayForm);

            listener.Close();
        }
    }
}