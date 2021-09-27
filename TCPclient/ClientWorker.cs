using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ModelLib;

namespace TCPclient
{
    class ClientWorker
    {
        private const int SERVER_PORT = 2121;

        public ClientWorker()
        {
        }

        public void Start()
        {
            List<FootballPlayer> templist = new List<FootballPlayer>();
            TcpClient socket = new TcpClient("localhost", SERVER_PORT);
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;

                FootballPlayer player1 = new FootballPlayer(27 ,"Mikael Petersen", 8000, 12);
                FootballPlayer player2 = new FootballPlayer(48, "Lars Kiks", 1200, 65);
                FootballPlayer player3 = new FootballPlayer(96, "Uffe Magnusen", 20000, 98);

                templist.Add(player1);
                templist.Add(player2);
                templist.Add(player3);

                foreach (var player in templist)
                {
                    String json = JsonSerializer.Serialize(player);
                    sw.WriteLine(json);
                }
                sw.WriteLine();

            }
            socket?.Close();
        }
    }
}
