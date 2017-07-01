using System;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using Fleck;
using System.Timers;

namespace ProlabServer
{
	

	public class Program
	{
		public enum State
		{
			IDLE,
			GAME
		}

		Server _server;
		Game _game;

		const double GameLoopTime = 200.0;

		int _frameCount;
		State _state;

		public int FrameCount { get { return _frameCount; } }


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

			

			var timer = new Timer(GameLoopTime);
			timer.Elapsed += Timer;
			timer.AutoReset = true;
			timer.Enabled = true;

			var input = Console.ReadLine();
			while (true)
			{
				if (input == "exit") break;
				if (_state == State.IDLE && input == "start")
				{
					Console.WriteLine("Started game!");

					_state = State.GAME;
					SendGameDate();
				}

				input = Console.ReadLine();
			}
		}

		private void Timer(Object source, ElapsedEventArgs e)
		{
			//Console.WriteLine("Timer - SignalTime: " + e.SignalTime.ToLongTimeString());
			
			if (_state == State.GAME)
			{
				_frameCount++;
				SendGameDate();
			}
			
		}

		private void SendGameDate()
		{
			var gameData = _game.GetGameData();
			gameData.FrameCount = FrameCount;
			_server.SendGameUpdate(gameData);
		}
	}
}
