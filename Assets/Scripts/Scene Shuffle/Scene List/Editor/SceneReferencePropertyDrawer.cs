using System.Linq;
using ArcadeShuffle;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferencePropertyDrawer : PropertyDrawer
{
	public override VisualElement CreatePropertyGUI(SerializedProperty property)
	{
		VisualElement container = new VisualElement();

		var sceneDisplayNameProp = property.FindPropertyRelative("displayName");
		if (sceneDisplayNameProp != null)
		{
			var displayField = new PropertyField(sceneDisplayNameProp);
			container.Add(displayField);
		}

		var sceneSelect = new PopupField<string>();
		// var bpo = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(new BuildPlayerOptions());
		// sceneSelect.choices = bpo.scenes.ToList();
		sceneSelect.choices = EditorBuildSettings.scenes.Select(x=>x.path).ToList();
		sceneSelect.Bind(property.serializedObject);
		
		var pathProp = property.FindPropertyRelative("scenePath").propertyPath;
		if (pathProp != null)
		{
			sceneSelect.bindingPath = property.FindPropertyRelative("scenePath").propertyPath;
		}
		sceneSelect.label = "Scene";
		//

		var sceneIconProp = property.FindPropertyRelative("sceneIcon");
		if (sceneIconProp != null)
		{
			var iconField = new PropertyField(sceneIconProp);
			container.Add(iconField);
		}
		
		container.Add(sceneSelect);
		return container;
	}
}
