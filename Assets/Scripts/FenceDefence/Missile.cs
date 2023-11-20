using System;
using System.Security.Cryptography;
using UnityEngine;

namespace ArcadeShuffle
{
	public class Missile : MonoBehaviour
	{
		public float moveSpeed;
		private void Update()
		{
			//We are rotated by the spawner, so we just move "up" - our local up.
			//Space.self is default. My choice to explicitly include it is to make this point visible.
			transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime), Space.Self);
		}

		public void Explode()
		{
			Destroy(gameObject);
		}
	}
}