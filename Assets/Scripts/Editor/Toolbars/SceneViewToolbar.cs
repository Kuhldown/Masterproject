using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
[InitializeOnLoad]
public class SceneViewToolbar : EditorWindow
{
	public static int index = 0;
	private static List<string> scenes = new List<string>();
	private static Vector3 position;

	static SceneViewToolbar()
	{
		SceneView.onSceneGUIDelegate += OnScene;
		UpdateSceneNames();
	}
	
	private static void OnScene(SceneView sceneview)
	{
		Handles.BeginGUI();
		GUILayout.Space(-20);
		EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

		SetScenePopup();
		Vector3 current = EditorGUILayout.Vector3Field("",position);
		if(current != position)
		{
			sceneview.LookAt(current);
			position = current;
		}
		EditorGUILayout.EndHorizontal();
		
		Handles.EndGUI();
	}

	private static void SetScenePopup ()
	{
		int current = EditorGUILayout.Popup (index, scenes.ToArray ());
		if (current != index) 
		{
			if (EditorApplication.SaveCurrentSceneIfUserWantsTo ()) 
			{
				var scene = EditorBuildSettings.scenes [current];
				EditorApplication.OpenScene (scene.path);
				index = current;
			}
		}
	}

	private static void UpdateSceneNames()
	{
		for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			var scene = EditorBuildSettings.scenes[i];
			var sceneName = Path.GetFileNameWithoutExtension(scene.path);
			scenes.Add(sceneName);
			if(sceneName == EditorApplication.currentScene)
			{
				index = i;
			}
		}
	}
}