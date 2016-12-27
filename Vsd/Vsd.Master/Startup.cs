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
        private static void Main(string[] args)
        {
            var port = int.Parse(args[0]);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var displayForm = new DisplayForm();

            var listener = new UdpListener(new IPEndPoint(IPAddress.Any, port));

            var receivePixelsComponent = new ReceivePixelsComponent(listener, displayForm.PixelsBuffer);
            Task.Run(() => receivePixelsComponent.Start());

            Application.Run(displayForm);

            listener.Close();
        }
    }
}