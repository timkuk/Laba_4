using Laba_4;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Server
    {
        static int port = 0;
        static TcpListener server;
        static IPAddress ip;

        static void Main(string[] args)
        {
            try
            {
                port = 8080;
                ip = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(ip, port);
                server.Start();

                Console.WriteLine("Server started. Waiting for connections...");

                while (true)
                {
                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected. Executing a request...");
                    ClientObject clientobject = new ClientObject(client);

                    Thread clientThread = new Thread(new ThreadStart(clientobject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (server != null)
                    server.Stop();
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
