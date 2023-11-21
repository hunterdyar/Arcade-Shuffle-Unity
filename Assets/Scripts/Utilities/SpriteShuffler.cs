using ArcadeShuffle.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArcadeShuffle
{
    //It's nice when we write a component intending on it being a multi-use component.... and it actually is!
    //This component and the frogger obstacle could be the same thing, but it makes sense to have nice little re-usable pieces.
    //shuffling a list of colors/sprites is likely a common thing to do, and will be nice to re-use.
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteShuffler : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        [Header("Sprite Shuffle")]
        public InitializationSettings ShuffleSpriteWhen;
        public bool avoidPreviousSprite;
        public Sprite[] SpriteOptions;

        [Header("Color Shuffle")]
        public InitializationSettings ChooseRandomColorWhen;
        public bool avoidPreviousColor;
        public Color[] ColorOptions;
        void Awake()
        {
            //[RequireComponent] enforces the component.
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            if (ShuffleSpriteWhen == InitializationSettings.OnStart)
            {
                ShuffleSprite();
            }

            if (ChooseRandomColorWhen == InitializationSettings.OnStart)
            {
                NewRandomColor();
            }
        }

        private void OnEnable()
        {
            if (ShuffleSpriteWhen == InitializationSettings.OnEnable)
            {
                ShuffleSprite();
            }

            if (ChooseRandomColorWhen == InitializationSettings.OnEnable)
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
