using UnityEngine;

namespace ArcadeShuffle
{
    /// <summary>
    /// This is only needed when we have Domain Reloading turned off.
    /// It isn't required for the build, or even the game in normal unity settings.
    /// but it is required because I'm tired of waiting on green progress bars.
    /// </summary>
    public class InputInit : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        void Awake()
        {
            _inputReader.ForceInit();
        }
    }
}


//If we lazy cache a static reference to the inputReader, then we can automate this without needing the input init
//by listening to the playmodestatechanged event. But I'm lazy, why bother.

// [InitializeOnLoadAttribute]
// public static class PlayModeStateChangedExample
// {
//     // register an event handler when the class is initialized
//     static PlayModeStateChangedExample()
//     {
//         EditorApplication.playModeStateChanged += LogPlayModeState;
//     }
//
//     private static void LogPlayModeState(PlayModeStateChange state)
//     {
//         Debug.Log(state);
//     }
// }