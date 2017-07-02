using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlabServer
{
	public class Player
	{
		public Guid Guid { get; private set; }
		public Vector2 Position { get; private set; }

		public Player(Guid guid)
		{
			Guid = guid;
		}

		public void Init(Vector2 pos)
		{
			
			Position = pos;
		}

		public void ApplyMove(Vector2 move)
		{
			Position += move;
		}
	}
}
