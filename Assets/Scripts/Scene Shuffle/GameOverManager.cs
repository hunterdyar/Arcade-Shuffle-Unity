using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShuffle
{
    public class GameOverSceneManager : MonoBehaviour
    {
        public SceneShuffler Shuffler;

        public void AnotherGame()
        {
            //fade
            Shuffler.GoToPickerScene();
        }

        public void RetryGame()
        {
            //fade
            Shuffler.RetryThisGame();
        }
    }
}
