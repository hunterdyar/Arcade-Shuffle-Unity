using System;
using UnityEngine;

namespace ArcadeShuffle.Frogger
{
	public class WorldBounds : MonoBehaviour
	{
		public static Bounds Bounds;
		private void Awake()
		{
			Bounds = GetComponent<BoxCollider2D>().bounds;
		}

		public static bool IsInBounds(float x, float y)
		{
			return Bounds.Contains(new Vector3(x, y, 0));
		}

		public static bool IsInBounds(Vector3 position)
		{
			return Bounds.Contains(position);
		}
	}
}