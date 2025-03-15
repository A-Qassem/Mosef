using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Mosef_Server
{
    internal class Server
    {
        DataBaseLoader? _DataBaseLoader;
        TcpListener? listener;
        readonly object queueLock = new();

        Queue<(string request, TcpClient client)> RequestQueue = new();

        private Server()
        {
            _DataBaseLoader = new DataBaseLoader();

        }
        public void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("Server started on port 5000...");

            Task.Run(ProcessQueue);

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(client));
            }
        }
        void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (client.Connected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {request}");

                    lock (queueLock)
                    {
                        RequestQueue.Enqueue((request, client));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client disconnected: " + ex.Message);
            }

            client.Close();
        }
        private void ProcessQueue()
        {
            while (true)
            {
                if (RequestQueue.Count > 0)
                {
                    lock (queueLock)
                    {
                        if (RequestQueue.Count > 0)
                        {
                            var task = RequestQueue.Dequeue();
                            ProcessRequest(task.request, task.client);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        void ProcessRequest(string request, TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
