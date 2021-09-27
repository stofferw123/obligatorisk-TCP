using System;

namespace TCPclient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientWorker worker = new ClientWorker();
            worker.Start();

            Console.ReadLine();
        }
    }
}
