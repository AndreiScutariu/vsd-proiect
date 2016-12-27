namespace Vsd.Slave.Node
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    using Vsd.Slave.Node.Components;

    internal class Startup
    {
        private static ConcurrentDictionary<int, byte[]> receivedBytesMatrix;

        private static void Main(string[] args)
        {
            string[] stringParam = args[0].Split('-');

            var leftPort = int.Parse(stringParam[0]);
            var rightPort = int.Parse(stringParam[1]);
            var parrentPort = int.Parse(stringParam[2]);

            Console.WriteLine($"Slave node oppened on port {leftPort} - {rightPort}!");

            receivedBytesMatrix = new ConcurrentDictionary<int, byte[]>();

            var leftReceiver = new ReceivePixelsComponent(receivedBytesMatrix, leftPort);
            var rightReceiver = new ReceivePixelsComponent(receivedBytesMatrix, rightPort);

            var aggregateSender = new AggregatePixelsComponent(receivedBytesMatrix, parrentPort);

            Task.Run(() => leftReceiver.Start());
            Task.Run(() => rightReceiver.Start());
            Task.Run(() => aggregateSender.Start());

            Console.ReadKey();
        }
    }
}