using UnityEngine;

namespace ArcadeShuffle
{
    public class GameplayLoader : MonoBehaviour
    {
        public SceneShuffler Shuffler;
        // Start is called before the first frame update
        void Start()
        {
            //play an intro animation or something?
            Shuffler.GoToPickerScene();
        }
        
    }
}
