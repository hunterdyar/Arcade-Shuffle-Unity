using System;
using UnityEngine;

namespace Utilities.ScenePathAttribute
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class ScenePathAttribute : PropertyAttribute
	{
		
	}
}