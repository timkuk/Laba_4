using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Laba_4
{
    class ClientObject
    {
        public TcpClient client;
        static NetworkStream stream = null;
        static string clientName = string.Empty;

        public ClientObject(TcpClient tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                    }
                    while (stream.DataAvailable);

                    var read_string = builder.ToString();
                    Console.WriteLine(read_string);

                    if (read_string.Contains("Id: "))
                        clientName = read_string.Split(':')[1];

                    if(read_string.Contains(';'))
                        if(ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), new UniformSearchMethod(double.Parse(read_string.Split(';')[0]), double.Parse (read_string.Split(';')[1]))))
                        {
                            Console.WriteLine(clientName + ":thread start work");
                            Thread.Sleep(1000);
                            Console.WriteLine(clientName + "thread finish work");
                        }
                        else
                        {
                            Console.WriteLine(clientName + "thread not exist");
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
                Console.ReadKey();
            }
        }

        static void ThreadProc(Object stateInfo)
        {
            UniformSearchMethod ti = (UniformSearchMethod)stateInfo;
            var answer = Encoding.Unicode.GetBytes(ti.Start().ToString());
            stream.Write(answer, 0, answer.Length);
        }
    }
}
