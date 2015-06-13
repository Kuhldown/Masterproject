using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// SceneViewWindow class.
/// </summary>
public abstract class BasicEditorWindow : EditorWindow
{
	protected abstract void OnGUI();
}
