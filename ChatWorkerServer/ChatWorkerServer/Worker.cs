using ChatWorkerServer.Server;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWorkerServer
{
    public class Worker : BackgroundService
    {
        private static ChatSocketServer _server;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _server = new ChatSocketServer(IPAddress.Any, 6767);
            _server.Start();
            Logger.Info("Server started successfully.");

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _server.Stop();
            Logger.Info("Server stopped successfully.");

            return base.StopAsync(cancellationToken);
        }
    }
}
