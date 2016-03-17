using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Labyrinth.Services
{
    public class NetworkService
    {
        public ManualResetEvent ConnectDone = new ManualResetEvent(false);
        public ManualResetEvent SendDone = new ManualResetEvent(false);
        public ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        public StringBuilder Responce;

        public bool IsBroken;

        private Socket _client;

        public void StartClientTest()
        {
            try
            {
                var ipAddress = IPAddress.Parse("127.0.0.1");
                var remoteEp = new IPEndPoint(ipAddress, 1000);

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _client.BeginConnect(remoteEp, ConnectCallBack, _client);
                ConnectDone.WaitOne();

                Send("This is a test<EOF>");
                SendDone.WaitOne();

                Receive();
                ReceiveDone.WaitOne();

                Console.WriteLine(@"Response received : {0}", Responce);

                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
            }
        }

        public void OpenConnection(string ipAddress, string port)
        {
            try
            {
                var remoteEp = new IPEndPoint(IPAddress.Parse(ipAddress), Int32.Parse(port));

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _client.BeginConnect(remoteEp, ConnectCallBack, _client);
                ConnectDone.WaitOne();
            }
            catch (Exception e)
            {
                ExceptionLogger.ExceptionLogger.AddException(e.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
            }
            catch (Exception e)
            {
                ExceptionLogger.ExceptionLogger.AddException(e.Message);
            }
        }

        public void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);

                ConnectDone.Set();
            }
            catch (Exception e)
            {
                ExceptionLogger.ExceptionLogger.AddException(e.Message);
            }
        }
        public void Receive()
        {
            try
            {
                var state = new StateObject { WorkSocket = _client };

                _client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    ReceiveCallback, state);
            }
            catch (Exception e)
            {
                ExceptionLogger.ExceptionLogger.AddException(e.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (StateObject)ar.AsyncState;
                var client = state.WorkSocket;

                var bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    var str = Encoding.Unicode.GetString(state.Buffer, 0, bytesRead);

                    state.Sb.Append(str);

                    lock (Responce)
                    {
                        Responce = state.Sb;
                    }

                    client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                        ReceiveCallback, state);
                }
                else
                {
                    if (state.Sb.Length > 1)
                    {
                        lock (Responce)
                        {
                            Responce = state.Sb;
                        }
                    }
                    ReceiveDone.Set();
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                IsBroken = true;
                ExceptionLogger.ExceptionLogger.AddException(e.Message);
            }
        }

        public void Send(string data)
        {
            byte[] byteData = Encoding.Unicode.GetBytes(data);

            _client.BeginSend(byteData, 0, byteData.Length, 0,
                SendCallback, _client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;

                var bytesSent = client.EndSend(ar);
                Console.WriteLine(@"Sent {0} bytes to server.", bytesSent);

                SendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool CheckConnection()
        {
            try
            {
                return _client.Connected;
            }
            catch (Exception e)
            {
                ExceptionLogger.ExceptionLogger.AddException(e.Message);
                return false;
            }
        }

        public string GetMessage()
        {
            var str = Responce.ToString();
            lock (Responce)
            {
                Responce.Clear();
            }

            return str;
        }

    }

    public class StateObject
    {
        public Socket WorkSocket;

        public const int BufferSize = 2048;

        public byte[] Buffer = new byte[BufferSize];

        public StringBuilder Sb = new StringBuilder();
    }
}
