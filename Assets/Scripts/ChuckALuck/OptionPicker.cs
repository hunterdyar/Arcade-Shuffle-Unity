using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using Utilities.NoSceneObjects;

namespace ArcadeShuffle
{
    public class OptionPicker : MonoBehaviour
    {
        public SceneShuffler _Shuffler;
        private SceneReference[] GameplayScenes => _Shuffler.GameplaySceneList.SceneReferences;
        public Transform pickPoint;
        [NoSceneObjects][SerializeField] private GameObject Peg;
        [NoSceneObjects][SerializeField] private SpriteRenderer SceneWedgeIcon;
        public int pegCount = 64;
        public float pegRadius = 3;
        public float iconRadius;
        private Spinner _spinner;

        private Dictionary<int, SceneReference> _wheelLookup;//i can't be bothered to do trig.
        void Awake()
        {
            _spinner = GetComponent<Spinner>();
            CreatePegs();
        }

        void CreatePegs()
        {
            _wheelLookup = new Dictionary<int, SceneReference>();
            int sceneCount = GameplayScenes.Length;
            float onePegPercent = 2 * Mathf.PI / (pegCount);
            for (int i = 0; i < pegCount; i++)
            {
                // float percentage = (i / (float)pegCount) * 2 * Mathf.PI;
                float percentage = i * onePegPercent;
                Vector2 pos = new Vector2(Mathf.Cos(percentage), Mathf.Sin(percentage)) * pegRadius;
                Instantiate(Peg, pos, Quaternion.LookRotation(Vector3.forward, pos), transform);
                
                //instantiate graphics for the options. "wedge"
                //take a half step over.
                float wedgePercentage = percentage + onePegPercent/2;
                pos = new Vector2(Mathf.Cos(wedgePercentage), Mathf.Sin(wedgePercentage)) * iconRadius;
                var icon = Instantiate(SceneWedgeIcon, pos, Quaternion.LookRotation(Vector3.forward, pos), transform);
                int s = i % sceneCount;
                var scene = GameplayScenes[s];
                icon.sprite = scene.sceneIcon;
                _wheelLookup.Add(i, scene);
            }
        }

        void OnEnable()
        {
            _spinner.OnSpinComplete += OnSpinComplete;
        }

        void OnDisable()
        {
            _spinner.OnSpinComplete -= OnSpinComplete;
        }

        void OnSpinComplete()
        {   
            Debug.Log("Spin Finished!");
            //now figure out which thing is up!
            var pickedPoint = transform.InverseTransformVector(pickPoint.transform.position);
            var pickedDirection = Vector3.SignedAngle(Vector3.up, pickedPoint, Vector3.forward);
            pickedDirection = (pickedDirection + 360) % 360;
            int pick = Mathf.FloorToInt((pickedDirection / 360) / (float)pegCount);
            Debug.Log(_wheelLookup[pick].displayName);
            _Shuffler.LoadScene(_wheelLookup[pick]);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
