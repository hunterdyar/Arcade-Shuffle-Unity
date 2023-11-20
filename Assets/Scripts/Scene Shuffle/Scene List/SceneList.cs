using System.Linq;
using ArcadeShuffle.Extensions;
using UnityEngine;

namespace ArcadeShuffle
{
	[CreateAssetMenu(fileName = "Scene List", menuName = "Scotty/Scene List", order = 0)]
	public class SceneList : ScriptableObject
	{
		public SceneReference[] SceneReferences;
		public int Length => SceneReferences.Length;
		public SceneReference GetRandomScene()
		{
			return SceneReferences.RandomItem();
		}

		public string[] GetScenesPaths()
		{
			return SceneReferences.Select(x => x.scenePath).ToArray();
		}
	}
}