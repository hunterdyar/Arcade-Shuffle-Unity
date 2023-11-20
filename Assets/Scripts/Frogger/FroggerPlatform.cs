using System;
using System.Security.Cryptography;
using UnityEngine;

namespace ArcadeShuffle.Frogger
{
	public class FroggerPlatform : MonoBehaviour
	{
		//in frogger the turtles animate up and down, so we want to turn it on and off.
		//we should just enable and disable the box collider, but we have to call exitPlatform ourselves to kick the frog off?
		//not worrying about it until we implement that.
		//public bool IsActive;

		public void EnterPlatform(FroggerDogController frog)
		{
			frog.transform.SetParent(transform);
		}

		public void ExitPlatform(FroggerDogController frog)
		{
			frog.transform.SetParent(null);
		}
	}
}