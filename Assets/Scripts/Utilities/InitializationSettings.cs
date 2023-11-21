using UnityEngine;

namespace ArcadeShuffle
{
	//This is mostly to make the inspector settings, and the code, more convenient and readable.
	public enum InitializationSettings
	{
		OnStart,
		OnEnable,
		[InspectorName("Off (Manual Only)")]
		Manual
	}
}