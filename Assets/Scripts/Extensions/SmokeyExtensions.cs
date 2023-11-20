using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace ArcadeShuffle.Extensions
{
	public static class SmokeyExtensions
	{
		public static T RandomItem<T>(this T list) where T : IList<T>
		{
			return list[Random.Range(0, list.Count)];
		}

		public static T RandomItem<T>(this T[] array)
		{
			return array[Random.Range(0, array.Length)]; 
		}
	}
}