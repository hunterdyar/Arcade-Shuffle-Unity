using System.Collections.Generic;
using System.Linq;
using ArcadeShuffle.Extensions;
using Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.ReadOnlyAttribute;
using Utilities.ScenePathAttribute;

namespace ArcadeShuffle
{
	[CreateAssetMenu(fileName = "Scene Shuffler", menuName = "Scotty Dog/Scene Shuffler", order = 0)]
	public class SceneShuffler : ScriptableObject
	{
		public SceneList GameplaySceneList => _gameplaySceneList;
		[SerializeField] private SceneList _gameplaySceneList;
		[ScenePath]
		public string RandomScenePicker;
		[ScenePath]
		public string GameOverScene;
		public bool LoadGameOverAdditive = true;
		public bool LoadRandomPickerAdditive = true;
		private SceneReference previousPickedScene;
		
		public void LoadGameOverScene()
		{
			var goscene = SceneManager.GetSceneByPath(GameOverScene);
			//goscene will be invalid if the scene is not loaded. This prevents it from loading more than once.
			if (goscene.buildIndex == -1)
			{
				LoadSceneMode mode = LoadGameOverAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
				SceneManager.LoadScene(GameOverScene, mode);
			}
		}

		public void GoToPickerScene()
		{
			LoadSceneMode mode = LoadRandomPickerAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
			SceneManager.LoadScene(RandomScenePicker, mode);
		}

		public void LoadScene(SceneReference scene, LoadSceneMode mode = LoadSceneMode.Single)
		{
			previousPickedScene = scene;
			SceneManager.LoadScene(scene.scenePath, mode);
		}

		public void RetryThisGame()
		{
			//unload gameplay scenes, etc.
			//this doesn't work with a loader+gameplay setup, which we aren't using right now (instead, this scriptableObject!)
			SceneManager.LoadScene(previousPickedScene.scenePath);
		}
	}
}