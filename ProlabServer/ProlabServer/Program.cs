using System;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using Fleck;

namespace ProlabServer
{
	

	public class Program
	{
		Server _server;
		Game _game;

		public static void Main(string[] args)
		{
			var program = new Program();
			program.Start();
		}

		public void Start()
		{
			_game = new Game();
			_server = new Server();

			_server.OnPlayerConnected += _game.OnPlayerConnected;
			_server.OnPlayerDisconnected += _game.OnPlayerDisconnected;
			_server.StartServer();
		}


	}
}
