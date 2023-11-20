using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadeShuffle.FlappyDog
{
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
            _reader.OnActionPerformed -= StartGame;
        }

        private void OnDestroy()
        {
            if (!_gameHasBegun)
            {
                _reader.OnActionPerformed -= StartGame;
            }
        }

        
    }
}
