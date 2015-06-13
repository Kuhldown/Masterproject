// Alternative version, with redundant code removed
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor
{
	private Transform follower;
	private TransformSetup positionSetup = new TransformSetup();
	private TransformSetup rotationSetup  = new TransformSetup();
	private TransformSetup scaleSetup  = new TransformSetup();

	private struct TransformSetup
	{
		public bool foldout;
		public Transform target;
	}
	public override void OnInspectorGUI()
	{
		
		Transform t = (Transform)target;
		
		// Replicate the standard transform inspector gui
		EditorGUIUtility.LookLikeControls();
		EditorGUI.indentLevel = 0;

		Vector3 position = PositionField (t);
		Vector3 eulerAngles = RotationField (t);
		Vector3 scale = ScaleField (t);

		string buttonName = "Reset";
		GUI.SetNextControlName(buttonName);
		if(GUILayout.Button(buttonName))
		{
			GUI.FocusControl(buttonName);
			positionSetup = new TransformSetup();
			rotationSetup  = new TransformSetup();
			scaleSetup  = new TransformSetup();
			position = Vector3.zero;
			eulerAngles = Vector3.zero;
			scale = Vector3.one;
		}

		EditorGUIUtility.LookLikeInspector();
		
		if (GUI.changed)
		{
			Undo.RegisterUndo(t, "Transform Change");
			
			t.localPosition = FixIfNaN(position);
			t.localEulerAngles = FixIfNaN(eulerAngles);
			t.localScale = FixIfNaN(scale);
		}
	}

	private Vector3 PositionField (Transform t)
	{	
		Vector3 position = t.localPosition;
		EditorGUILayout.BeginHorizontal ();
		positionSetup.foldout = EditorGUILayout.Foldout(positionSetup.foldout,"Position");
		position = EditorGUILayout.Vector3Field ("", position);
		EditorGUILayout.EndHorizontal ();

		if(positionSetup.foldout)
		{
			string name = "Reset";
			GUI.SetNextControlName(name);
			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button (name, ButtonStyle)) 
			{
				GUI.FocusControl(name);
				position = Vector3.zero;
				positionSetup.target = null;
			}
			
			positionSetup.target = (Transform) EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), "", positionSetup.target, typeof(Transform));
			EditorGUILayout.EndHorizontal ();
		}
		if(positionSetup.target)
		{
			position = positionSetup.target.position;
		}
		return position;
	}

	private Vector3 RotationField (Transform t)
	{
		EditorGUILayout.BeginHorizontal ();
		rotationSetup.foldout = EditorGUILayout.Foldout(rotationSetup.foldout,"Rotation");
		Vector3 eulerAngles = EditorGUILayout.Vector3Field ("", t.localEulerAngles);
		EditorGUILayout.EndHorizontal ();
		
		if(rotationSetup.foldout)
		{
			string name = "Reset";
			GUI.SetNextControlName(name);
			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button (name, ButtonStyle)) 
			{
				GUI.FocusControl(name);
				eulerAngles = Vector3.zero;
				rotationSetup.target = null;
			}
			
			rotationSetup.target = (Transform) EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), "", rotationSetup.target, typeof(Transform));
			EditorGUILayout.EndHorizontal ();
		}
		if(rotationSetup.target)
		{
			eulerAngles = rotationSetup.target.localRotation.eulerAngles;
		}
		return eulerAngles;
	}

	private Vector3 ScaleField (Transform t)
	{
		EditorGUILayout.BeginHorizontal ();
		scaleSetup.foldout = EditorGUILayout.Foldout(scaleSetup.foldout,"Scale");
		Vector3 scale = EditorGUILayout.Vector3Field ("", t.localScale);
		EditorGUILayout.EndHorizontal ();
		
		if(scaleSetup.foldout)
		{
			string name = "Reset";
			GUI.SetNextControlName(name);
			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Reset", ButtonStyle)) 
			{
				GUI.FocusControl(name);
				scale = Vector3.one;
				scaleSetup.target = null;
			}
			
			scaleSetup.target = (Transform) EditorGUI.ObjectField(EditorGUILayout.GetControlRect(), "", scaleSetup.target, typeof(Transform));
			EditorGUILayout.EndHorizontal ();
		}
		if(scaleSetup.target)
		{
			scale = scaleSetup.target.localScale;
		}

		return scale;
	}

	private GUIStyle ButtonStyle
	{
		get
		{
			GUIStyle style = new GUIStyle(EditorStyles.miniButton);
			style.fixedWidth = 50f;
			return style;
		}
	}
	
	private Vector3 FixIfNaN(Vector3 v)
	{
		if (float.IsNaN(v.x))
		{
			v.x = 0;
		}
		if (float.IsNaN(v.y))
		{
			v.y = 0;
		}
		if (float.IsNaN(v.z))
		{
			v.z = 0;
		}
		return v;
	}
	
}