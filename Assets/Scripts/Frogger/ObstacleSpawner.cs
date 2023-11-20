using System;
using System.Collections;
using System.Collections.Generic;
using ArcadeShuffle.Frogger;
using UnityEngine;
using Utilities.NoSceneObjects;
using Random = UnityEngine.Random;

namespace ArcadeShuffle
{
    public class ObstacleSpawner : MonoBehaviour
    {
        /// <summary>
        /// once per x frames, do we check if the objects are in bounds or not.
        /// </summary>
        private static readonly int BoundsCheckFrequency = 10;
        
        [Header("Obstacle Information")]
        [NoSceneObjects]
        public Obstacle ObstaclePrefab;

        [Header("Spawn Settings")]
        public float gapMinimum;

        public CarDirection Direction;
        public float gapMaximum;
        public float speed;
        public bool randomStartPhase;
        private float timer;
        private float nextSpawnTime;
        private float minSpawnTime =>  gapMinimum / speed;
        private float maxSpawnTime => gapMaximum / speed;

        private readonly List<Obstacle> _obstacles = new List<Obstacle>();
        private int boundsCheck = 0;
        void Start()
        {
            //randomly distribute what frame we check compared to other spawners. 
            boundsCheck = Random.Range(0, BoundsCheckFrequency);
            if (randomStartPhase)
            {
                timer = GetSpawnTime();
            }
            else
            {
                timer = minSpawnTime;
            }
        }

        private float GetSpawnTime()
        {
            return Random.Range(minSpawnTime, maxSpawnTime);
        }
        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = GetSpawnTime();
                Spawn();
            }

            boundsCheck--;
            if (boundsCheck <= 0)
            {
                boundsCheck = BoundsCheckFrequency;
                foreach (var obstacle in _obstacles)
                {
                    if (!obstacle.InBounds)
                    {
                        obstacle.gameObject.SetActive(false);
                    }
                }
            }
        }

        private void Spawn()
        {
            var position = GetSpawnPosition(Direction);
            var obstacle = GetObstacleObject(position);
            obstacle.MoveDir = Direction;
            obstacle.MoveSpeed = speed;
        }

        Obstacle GetObstacleObject(Vector3 position)
        {
            foreach (var obstacle in _obstacles)
            {
                if (!obstacle.gameObject.activeInHierarchy)
                {
                    obstacle.transform.position = position;
                    obstacle.gameObject.SetActive(true);
                    //do we need to inject which spawner "owns" the platform?
                    return obstacle;
                }
            }
            //nothing in pool. instantiate new gameobject.
            var newObstacle = Instantiate(ObstaclePrefab, position, ObstaclePrefab.transform.rotation);
            _obstacles.Add(newObstacle);
            return newObstacle;
        }

        private Vector3 GetSpawnPosition(CarDirection direction)
        {
            float edge = 0;
            if (direction == CarDirection.Right)
            {
                edge = WorldBounds.Bounds.min.x;
            }else if (direction == CarDirection.Left)
            {
                edge = WorldBounds.Bounds.max.x;
            }

            return new Vector3(edge, transform.position.y, transform.position.z);
        }
    }
}
