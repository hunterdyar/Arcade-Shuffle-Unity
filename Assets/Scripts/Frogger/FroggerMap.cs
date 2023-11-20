using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ArcadeShuffle.Frogger
{
    public class FroggerMap : MonoBehaviour
    {
        public float LaneHeight => _laneHeight;
        [Tooltip("How much moving up or down moves the player, in world units.")]
        [SerializeField] private float _laneHeight;

        public float LaneWidth => _laneWidth;
        [Tooltip("How much moving left or right moves the player, in world units.")] [SerializeField]
        private float _laneWidth;


        //todo: change to tryGet pattern
        public Vector3 GetPosition(Vector3 pos, int dx, int dy)
        {
            //dx = Mathf.Clamp(dx, -1, 1);
            //dy = Mathf.Clamp(dy, -1, 1);
            float x = pos.x+ (dx * _laneWidth);
            float y = pos.y+ (dy * _laneHeight);

            //off edge limits
            if (WorldBounds.IsInBounds(x, y))
            {
                return new Vector3(x, y,pos.z);
            }
            else
            {
                //don't move
                return pos;
            }
        }
    }
}
