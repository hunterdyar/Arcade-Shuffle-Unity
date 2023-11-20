using ArcadeShuffle.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArcadeShuffle
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteShuffler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        [Header("Sprite Shuffle")]
        public InitilizationSettings ShuffleSpriteWhen;
        public bool avoidPreviousSprite;
        public Sprite[] SpriteOptions;

        [Header("Color Shuffle")]
        public InitilizationSettings ChooseRandomColorWhen;
        public bool avoidPreviousColor;
        public Color[] ColorOptions;
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            if (ShuffleSpriteWhen == InitilizationSettings.OnStart)
            {
                ShuffleSprite();
            }

            if (ChooseRandomColorWhen == InitilizationSettings.OnStart)
            {
                NewRandomColor();
            }
        }

        private void OnEnable()
        {
            if (ShuffleSpriteWhen == InitilizationSettings.OnEnable)
            {
                ShuffleSprite();
            }

            if (ChooseRandomColorWhen == InitilizationSettings.OnEnable)
            {
                NewRandomColor();
            }
        }

        public void ShuffleSprite()
        {
            if (avoidPreviousSprite && SpriteOptions.Length > 1)
            {
                Sprite oldSprite = _spriteRenderer.sprite;
                while (oldSprite == _spriteRenderer.sprite)
                {
                    _spriteRenderer.sprite = SpriteOptions.RandomItem();
                }
            }
            else
            {
                if (SpriteOptions.Length > 0)
                {
                    _spriteRenderer.sprite = SpriteOptions.RandomItem();
                }
                else
                {
                    Debug.LogWarning("Can't shuffle sprite, no options to choose from.", this);
                }
            }
        }

        public void NewRandomColor()
        {
            if (avoidPreviousColor && ColorOptions.Length > 1)
            {
                Color oldColor = _spriteRenderer.color;
                while (oldColor == _spriteRenderer.color)
                {
                    _spriteRenderer.color = ColorOptions.RandomItem();
                }
            }
            else
            {
                if (ColorOptions.Length > 0)
                {
                    _spriteRenderer.color = ColorOptions.RandomItem();
                }
                else
                {
                    Debug.LogWarning("Can't shuffle color, no options to choose from.",this);
                }
            }
        }
    }
}
