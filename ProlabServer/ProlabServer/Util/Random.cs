using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlabServer
{
	public class Random
	{
		private static System.Random s_Random = new System.Random();

		public static int Range(int min, int max)
		{
			return s_Random.Next(min, max);
		}

		
		public static float Range( float min, float max )
		{
		   return (float) (min + s_Random.NextDouble()*(max-min));
		}
	}
}
