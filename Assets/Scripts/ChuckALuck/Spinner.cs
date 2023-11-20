using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;
namespace ArcadeShuffle
{
    public class Spinner : MonoBehaviour
    {
        public Action OnSpin;
        public Action OnSpinComplete;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float spinForce;
        [Tooltip("Percentage of spinforce to add/subtract to spinforce to get random range.")]
        [SerializeField] [Range(0, 1)] private float forceVariation;

        [SerializeField] private AnimationCurve AngularDragCurve;
        [Tooltip("This lets the animation curve be 0->1, and normalize here. The max angular velocity to calculate drag from - the value of f(1) of DragCurve. f(0) is drag when stopped.")]
        [SerializeField] private float dragCurveMultiplier;

        [Tooltip("Added to the drag after calculating. remember y=mx+b? this is b.")]
        [SerializeField] private float dragCurveOffset;
        
        public bool IsSpinning => _spinning;
        private bool _spinning;
        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.4f);
            Spin();
        }

        void FixedUpdate()
        {
            _rigidbody2D.angularDrag = AngularDragCurve.Evaluate(_rigidbody2D.angularVelocity / dragCurveMultiplier) *
                                       dragCurveMultiplier + dragCurveOffset;
;            if (_spinning)
            {
                if (_rigidbody2D.angularVelocity < 0.05f)
                {
                    _rigidbody2D.angularVelocity = 0;
                    _spinning = false;
                    OnSpinComplete?.Invoke();
                }
            }
        }
        
        void SpinFinished()
        {
            OnSpinComplete?.Invoke();
        }

        [ContextMenu("Spin")]
        void Spin()
        {
            float min = spinForce - spinForce * forceVariation;
            float max = spinForce + spinForce * forceVariation;
            _rigidbody2D.AddTorque(Random.Range(min,max),ForceMode2D.Impulse);
            _spinning = true;
            OnSpin?.Invoke();
        }
    }
}
