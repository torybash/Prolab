using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlabServer
{
	public class Game
	{
		public List<Player> players = new List<Player>();

		public void OnPlayerConnected(Guid id)
		{
			var player = new Player(id);
			player.Init(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
			players.Add(player);
		}
	
		public void OnPlayerDisconnected(Guid id)
		{
			var player = GetPlayer(id);
			players.Remove(player);
		}

		public void OnPlayerInput(Guid id, ClientInput input)
		{
			var player = GetPlayer(id);
			player.ApplyMove(new Vector2(input.MoveX, input.MoveY));
		}



		private Player GetPlayer(Guid id)
		{
			return players.FirstOrDefault(p => p.Guid == id);
		}

		public GameData GetGameData()
		{
			var gameData = new GameData();
			foreach (var item in players)
			{
				var playerData = new PlayerData
				{
					PosX = item.Position.x,
					PosY = item.Position.y
				};
				gameData.PlayerDatas.Add(playerData);
			}

			return gameData;
		}


	}
}
