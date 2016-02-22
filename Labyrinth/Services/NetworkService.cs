using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Labyrinth.Services
{    
    public class NetworkServices
    {
        private static readonly ManualResetEvent ConnectDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent SendDone = new ManualResetEvent(false);
        private static readonly ManualResetEvent ReceiveDone = new ManualResetEvent(false);

        private static string _responce = string.Empty;

        public static void StartClient()
        {
            try
            {
                var ipAddress = IPAddress.Parse("127.0.0.1");
                var remoteEp = new IPEndPoint(ipAddress, 1000);

                var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(remoteEp, ConnectCallBack, client);
                ConnectDone.WaitOne();

                Send(client, "This is a test<EOF>");
                SendDone.WaitOne();

                Receive(client);
                ReceiveDone.WaitOne();

                Console.WriteLine(@"Response received : {0}", _responce);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                client.EndConnect(ar);

                ConnectDone.Set();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
        private static void Receive(Socket client)
        {
            try
            {
                var state = new StateObject { WorkSocket = client };

                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                    ReceiveCallback, state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.WorkSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.Sb.Append(Encoding.Unicode.GetString(state.Buffer, 0, bytesRead));

                    client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0,
                        ReceiveCallback, state);
                }
                else
                {
                    if (state.Sb.Length > 1)
                    {
                        _responce = state.Sb.ToString();
                    }
                    ReceiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.Unicode.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0,
                SendCallback, client);
        }

        private static void SendCallback(IAsyncResult ar)
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
    }

    public class StateObject
    {
        public Socket WorkSocket;

        public const int BufferSize = 256;

        public byte[] Buffer = new byte[BufferSize];

        public StringBuilder Sb = new StringBuilder();
    }
}   
