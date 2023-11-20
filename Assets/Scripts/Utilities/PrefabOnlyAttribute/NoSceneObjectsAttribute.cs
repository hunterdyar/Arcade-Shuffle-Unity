using System;
using UnityEngine;

namespace Utilities.NoSceneObjects
{
	[AttributeUsage(AttributeTargets.Field,AllowMultiple = false,Inherited = true)]
	public class NoSceneObjectsAttribute : PropertyAttribute
	{
		
	}
}