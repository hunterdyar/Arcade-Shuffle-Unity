using System;
using UnityEngine;

namespace ArcadeShuffle.Frogger
{
	public class Obstacle : MonoBehaviour
	{
		public FroggerPlatform FroggerPlatform { get; private set; }
		public float padding = 2;

		public bool IsDeadly = false;
		
		[HideInInspector]
		public CarDirection MoveDir;
		[HideInInspector]
		public float MoveSpeed;
		[HideInInspector]
		public bool InBounds;

		private void Awake()
		{
			//It's fine if this is null, it's just a cache so we don't do an extra GetComponent every frame on the player collision check.
			FroggerPlatform = GetComponent<FroggerPlatform>();
		}

		private void Update()
		{
			//MoveDir is an enum with left and right values non-default defined as to -1 and 1. We can cast it to an int and use it as a sign: times -1 or not.
			transform.Translate(Vector3.right * (MoveSpeed * Time.deltaTime * (int)MoveDir));
			//out of bounds?
			InBounds = WorldBounds.IsInBounds(transform.position + Vector3.right * ((int)MoveDir * padding));
		}
	}
}