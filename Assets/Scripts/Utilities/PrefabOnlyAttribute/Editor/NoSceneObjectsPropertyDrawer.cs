using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities.NoSceneObjects
{
	[CustomPropertyDrawer(typeof(NoSceneObjectsAttribute))]
	public class NoSceneObjectsPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var container = new VisualElement();
			ObjectField field = new ObjectField
			{
				allowSceneObjects = false
			};
			field.Bind(property.serializedObject);
			field.bindingPath = property.propertyPath;
			field.label = property.displayName;
			field.objectType = Type.GetType(property.type);
			container.Add(field);
			return container;
			
		}

		// public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		// {
		// 	EditorGUI.PropertyField(position, property, label);
		// }
	}
}