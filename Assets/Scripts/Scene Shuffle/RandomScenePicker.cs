using System.Collections;
using Blooper.TransitionEffects;
using ArcadeShuffle.Extensions;
using TMPro;
using UnityEngine;


namespace ArcadeShuffle
{
    public class RandomScenePicker : MonoBehaviour
    {
        public SceneShuffler Shuffler;

        public TMP_Text text;
        private SceneReference chosenScene;
        void Start()
        {
            StartCoroutine(PickRandomSceneAnimation());
        }

        private IEnumerator PickRandomSceneAnimation()
        {

            var scenes = Shuffler.GameplaySceneList;
            var chosenScene = scenes.SceneReferences.RandomItem();
            var prevChosenScene = chosenScene;
            text.text = chosenScene.displayName; 
            yield return new WaitForSeconds(0.1f);
            
            float randomEndTime = 0.1f;
            float randomStartTime = 0.01f;
            for (int i = 0; i < 10; i++)
            {
                //prevent same name repeating
                //i should make == work between scenereferences...
                if (scenes.SceneReferences.Length > 1)
                {
                    while (chosenScene.scenePath == prevChosenScene.scenePath)
                    {
                        chosenScene = scenes.SceneReferences.RandomItem();
                    }
                }

                text.text = chosenScene.displayName;
                yield return new WaitForSeconds(Random.Range(randomStartTime, randomEndTime));
                randomEndTime += 0.1f;//picking slows down ish
                randomStartTime += 0.05f;
            }

            text.enabled = false;
            yield return StartCoroutine(Transition.TransitionOutToColor(TransitionType.Fade, 0f, 0.7f, Color.black));
            Debug.Log("Load Scene: "+ chosenScene);
            Shuffler.LoadScene(chosenScene);
        }
    }
}
