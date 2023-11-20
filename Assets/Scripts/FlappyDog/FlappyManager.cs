using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShuffle.FlappyDog
{
    //This game manager is responsible for controlling the game state and the score.
    public class FlappyManager : MonoBehaviour
    {
        [SerializeField] private InputReader _reader;
        public static Action<int> OnFlappyScoreChange;
        public static Action OnStartGame;
        public int Score => _score;
        private int _score;

        public static bool GameStarted => _gameHasBegun;
        private static bool _gameHasBegun = false;
        
        // Start is called before the first frame update
        void Start()
        {
            _gameHasBegun = false;
            _score = 0;
            OnFlappyScoreChange?.Invoke(_score);
            _reader.OnActionPerformed += StartGame;
        }

        public void GetPoint()
        {
            _score++;
            OnFlappyScoreChange?.Invoke(_score);
        }
        
        public void StartGame()
        {
            _gameHasBegun = true;
            OnStartGame?.Invoke();
            //unsubscribe isntantly so we don't keep starting the game over and over
            _reader.OnActionPerformed -= StartGame;
        }

        private void OnDestroy()
        {
            //unsubscribe if we haven't yet. This could be OnDisable, but then it can get disabled multiple times without subscribing.
            if (!_gameHasBegun)
            {
                _reader.OnActionPerformed -= StartGame;
            }
        }

        
    }
}
