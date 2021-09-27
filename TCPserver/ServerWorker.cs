using ModelLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCPserver
{
    internal class ServerWorker
    {
        private static List<FootballPlayer> _playerList;
        private static List<FootballPlayer> _templist;
        private static bool readdone;
        private const int PORT = 2121;

        public ServerWorker()
        {
            _templist = new List<FootballPlayer>();
            _playerList = new List<FootballPlayer>();
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        DoClient(tmpSocket);
                    }
                );
            }
        }

        private void DoClient(TcpClient socket)
        {

            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;
                if (readdone != true)
                {
                    for (int i = 0; i < 1;)
                    {
                        string footballstring = sr.ReadLine();
                        if (footballstring == null || string.IsNullOrEmpty(footballstring))
                        {
                            i = 1;
                            readdone = true;
                        }
                        else
                        {
                            FootballPlayer player = JsonSerializer.Deserialize<FootballPlayer>(footballstring);
                            _templist.Add(player);
                            Console.WriteLine("recieved player : " + player.ToString());
                        }
                    }
                }
                else
                {
                    string command = sr.ReadLine();

                    if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
                    {
                        sw.WriteLine("Bad Request: can't be null or empty");
                        return;
                    }

                    if (command.Contains(" "))
                    {
                        string[] subs = command.Split(" ");
                        if (subs[0].ToLower() == "gem")
                        {
                            int saveID = int.Parse(subs[1]);
                            foreach (var player in _templist)
                            {
                                if (player.ID == saveID)
                                {
                                    _playerList.Add(player);
                                    socket.Close();
                                }
                                else
                                {
                                    Console.WriteLine("wrong format: gem [id]");
                                }
                            }
                        }
                        else if (subs[0].ToLower() == "hent")
                        {
                            int hentID = int.Parse(subs[1]);
                            foreach (var player in _playerList)
                            {
                                if (player.ID == hentID)
                                {
                                    string playerjson = JsonSerializer.Serialize(player);
                                    Console.WriteLine("found: " + playerjson);
                                }
                                else
                                {
                                    Console.WriteLine("wrong format: hent [id]");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("input not understood. use gem [id], hent [id] eller hentalle");
                        }


                    }
                    if (command == "hentalle")
                    {
                        foreach (var player in _playerList)
                        {
                            string playerstring = JsonSerializer.Serialize(player);
                            Console.WriteLine("found: " + playerstring);
                        }
                    }
                }


            }
            socket?.Close();
        }
    }
}