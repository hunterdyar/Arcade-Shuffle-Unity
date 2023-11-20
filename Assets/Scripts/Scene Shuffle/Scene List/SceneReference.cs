using System;
using UnityEngine;
using Object = System.Object;

namespace ArcadeShuffle
{
	[Serializable]
	public struct SceneReference : IEquatable<SceneReference>
	{
		public string displayName;
		public string scenePath;
		public Sprite sceneIcon;

		public override bool Equals(object obj)
		{
			if (obj is null)
			{
				return false;
			}

			if (obj is SceneReference other)
			{
				//only compare paths, really. display names can be different but the scene is the scene is the scene is the scene.
				return scenePath == other.scenePath;
			}

			return false;
		}

		public bool Equals(SceneReference other)
		{
			return scenePath == other.scenePath;
		}

		public override int GetHashCode()
		{
			return (scenePath != null ? scenePath.GetHashCode() : 0);
		}

		public static bool operator ==(SceneReference obj1, SceneReference obj2)
		{
			return obj1.Equals(obj2);
		}

		public static bool operator !=(SceneReference obj1, SceneReference obj2) => !(obj1 == obj2);

		public override string ToString()
		{
			return scenePath;
		}
	}
}