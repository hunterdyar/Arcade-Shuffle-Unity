using UnityEngine;
using Blooper.TransitionEffects;

namespace ScottyDog
{
	public class FadeInOnStart : MonoBehaviour
	{
		public float fadeTime = 0.85f;
		void Start()
		{
			StartCoroutine(Transition.TransitionInToScene(TransitionType.Fade,0.05f, fadeTime, Color.black));
		}
	}
}