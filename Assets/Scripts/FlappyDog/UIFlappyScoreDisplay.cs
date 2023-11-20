using System;
using TMPro;
using UnityEngine;

namespace ArcadeShuffle.FlappyDog
{
	public class UIFlappyScoreDisplay : MonoBehaviour
	{
		private TMP_Text _text;

		private void Awake()
		{
			_text = GetComponent<TMP_Text>();
		}

		private void OnEnable()
		{
			FlappyManager.OnFlappyScoreChange += ScoreChange;
		}

		private void OnDisable()
		{
			FlappyManager.OnFlappyScoreChange += ScoreChange;
		}


		private void ScoreChange(int score)
		{
			_text.text = score.ToString();
		}
	}
}