using System;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        static string userName { get; set; }

        static void Main(string[] args)
        {
            TcpClient client = null;
            int port = 0;
            string ip = string.Empty;

            try
            {
                port = 8080;
                ip = "127.0.0.1";

                Console.Write("Enter your user name: ");
                userName = Console.ReadLine();

                client = new TcpClient(ip, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] data = Encoding.Unicode.GetBytes("user name:" + userName);
                    stream.Write(data, 0, data.Length);

                    string ab = Input();
                    data = Encoding.Unicode.GetBytes(ab);

                    stream.Write(data, 0, data.Length);

                    data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string answ = builder.ToString();

                    Console.WriteLine("Responce [{0}] = {1}", ab, answ);

                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex);
                Console.ReadKey();
            }
            finally
            {
                client.Close();
            }
        }

        /// <summary>
        /// Ввод матрицы и преобразование в cтроку
        /// </summary>
        /// <returns></returns>
        static string Input()
        {
            string strMatrix = string.Empty;
            try
            {
                Console.Write("Enter beginning of the segment: ");
                double a = double.Parse(Console.ReadLine());
                strMatrix += a + ";";
                Console.Write("Enter end of the segment: ");
                double b = double.Parse(Console.ReadLine());
                strMatrix += b;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
            return strMatrix;
        }
    }
}
