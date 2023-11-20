using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ArcadeShuffle.FlappyDog
{
    public class PipeManager : MonoBehaviour
    {
        //constants
        //spawn prefabs to the right and check for off-screen pipes on the left; this many units past screen edge.
        private readonly float _padding = 3;
        
        //asset references
        [Header("Asset References")]
        [SerializeField] private GameObject pipePrefab;
        
        [Header("Gameplay Settings")]
        [SerializeField] private float pipeMovementSpeed;
        [SerializeField] private float _gapBetweenPipes;

        [Header("Configuration")]
        [FormerlySerializedAs("delayBeforeFirstPipe")] [SerializeField] private int _delayBeforeFirstPipe;
        [SerializeField] private Transform _minSpawnPos;
        [SerializeField] private Transform _maxSpawnPos;

        
        private Camera _camera;
        private readonly List<GameObject> _pipes = new List<GameObject>();
        private float _pipeSpawnTimer;
        private float _worldBoundsLeft;
        private float _pipeSpawnPos;
        void Start()
        {
            _camera = Camera.main;
            SetWorldBoundsFromView();
            if (pipeMovementSpeed != 0)
            {
                _pipeSpawnTimer =
                    -(_gapBetweenPipes / pipeMovementSpeed) *
                    _delayBeforeFirstPipe; // pipe gap before we start spawning pipes.
            }
            else
            {
                _pipeSpawnTimer = 0;
            }
        }

        private void SetWorldBoundsFromView()
        {
            var worldLeft = _camera.ScreenToWorldPoint(Vector3.zero);//zero is bottom left
            _worldBoundsLeft = worldLeft.x - _padding;
            var worldRight = _camera.ScreenToWorldPoint(new Vector2(_camera.pixelWidth,_camera.pixelHeight));//pix,pix is top right.
            _pipeSpawnPos = worldRight.x + _padding;
        }

        private void Update()
        {
            foreach (var pipe in _pipes)
            {
                if (pipe.activeInHierarchy)
                {
                    pipe.transform.Translate(Vector3.left * (pipeMovementSpeed * Time.deltaTime));
                    if (pipe.transform.position.x < _worldBoundsLeft)
                    {
                        pipe.SetActive(false);
                    }
                }
            }

            if (FlappyManager.GameStarted)
            {
                _pipeSpawnTimer += Time.deltaTime;
            }
            
            //the 1 comes from assumed width of pipes.
            if (pipeMovementSpeed != 0 && _pipeSpawnTimer > (_gapBetweenPipes+1) / pipeMovementSpeed)
            {
                _pipeSpawnTimer = 0;
                SpawnNewPipe();
            }

        }

        private void SpawnNewPipe()
        {
            var pipe = GetPipeGameObject();
            var pos = new Vector3(_pipeSpawnPos, Random.Range(_minSpawnPos.position.y, _maxSpawnPos.position.y), 0);
            pipe.transform.position = pos;
        }
      

        private GameObject GetPipeGameObject()
        {
            foreach (var pipe in (_pipes))
            {
                if (!pipe.activeInHierarchy)
                {
                    pipe.SetActive(true);
                    return pipe;
                }
            }

            var newPipe = Instantiate(pipePrefab, transform);
            _pipes.Add(newPipe);
            return newPipe;
        }
    }
}
