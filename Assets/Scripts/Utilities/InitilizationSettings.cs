using UnityEngine;

namespace ArcadeShuffle
{
	public enum InitilizationSettings
	{
		OnStart,
		OnEnable,
		[InspectorName("Off (Manual Only)")]
		Manual
	}
}