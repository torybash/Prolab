using Fleck;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ProlabServer
{
	public class Server
	{

		public Action<Guid> OnPlayerConnected;
		public Action<Guid> OnPlayerDisconnected;

		List<IWebSocketConnection> _allSockets;

		public void StartServer()
		{
			FleckLog.Level = LogLevel.Debug;
			_allSockets = new List<IWebSocketConnection>();
			var server = new WebSocketServer("ws://0.0.0.0:8080");
			server.Start(socket =>
				{
					socket.OnOpen = () =>
						{
							Console.WriteLine("Opened connection to: " + ObjectDumper.Dump(socket.ConnectionInfo));
							_allSockets.Add(socket);

							if (OnPlayerConnected != null) OnPlayerConnected(socket.ConnectionInfo.Id);



						};
					socket.OnClose = () =>
						{
							Console.WriteLine("Closed connection to: " + ObjectDumper.Dump(socket.ConnectionInfo));
							_allSockets.Remove(socket);

							if (OnPlayerDisconnected != null) OnPlayerDisconnected(socket.ConnectionInfo.Id);


						};
					socket.OnMessage = message =>
						{
							Console.WriteLine("Message received from: " + ObjectDumper.Dump(socket.ConnectionInfo));
							Console.WriteLine("\nMessage: " + message);

							//var player = GetPlayer(socket.ConnectionInfo.Id);
							//if (player != null)
							//{
							//	Console.WriteLine("\nPlayer position: " + player);
							//}

							//Pass message to all clients
							_allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
						};
					socket.OnBinary = bytes =>
						{
							Console.WriteLine("Bytes received from: " + ObjectDumper.Dump(socket.ConnectionInfo));
							Console.WriteLine("\nBytes: " + bytes.Length);

							ReadClientMessage(bytes, socket.ConnectionInfo.Id);
						};
				});



		}

		public void WriteMessage(string msg)
		{
			foreach (var socket in _allSockets.ToList())
			{
				socket.Send(msg);
			}
		}

		public void SendGameUpdate(GameData gameData)
		{
			var stream = new MemoryStream();
			gameData.WriteTo(stream);
			var buffer = stream.GetBuffer();
			foreach (var socket in _allSockets.ToList())
			{
				socket.Send(buffer);
			}
		}

		public void ReadClientMessage(byte[] bytes, Guid id)
		{
			var input = ClientInput.Parser.ParseFrom(bytes);
			if (input != null)
			{
				Console.WriteLine("input.Move: " + input.MoveX + ", " + input.MoveY);
			}
		}
	}
}
