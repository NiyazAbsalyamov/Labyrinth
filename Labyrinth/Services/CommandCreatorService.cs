using System.Threading;

namespace Labyrinth.Services
{
    public class CommandCreatorService
    {
        private readonly NetworkService _networkService;
        
        public CommandCreatorService()
        {
            _networkService = new NetworkService();
        }

        public void Test()
        {
            _networkService.StartClientTest();
        }

        public void BeginConnection(string ipAddress, string port)
        {
            _networkService.OpenConnection(ipAddress, port);
            _networkService.ConnectDone.WaitOne();
        }

        public void EndConnection()
        {
            _networkService.CloseConnection();
        }

        public bool CheckConnection()
        {
            return _networkService.CheckConnection();
        }

        public void Send(string data)
        {
            _networkService.Send(data);
            _networkService.SendDone.WaitOne();
        }

        public string Receive()
        {
            _networkService.Receive();
            _networkService.ReceiveDone.WaitOne();
            return _networkService.GetMessage();
        }
    }
}