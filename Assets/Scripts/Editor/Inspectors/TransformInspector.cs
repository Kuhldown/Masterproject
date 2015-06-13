// Reverse engineered UnityEditor.TransformInspector

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor {
	
	private bool firstSet;
	private Vector3 rotation;
	private Quaternion oldQuaternion;
	
	
	public TransformInspector ()
	{
		this.firstSet = true;
	}
	
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls();
		Transform t = (Transform)this.target;
		EditorGUI.indentLevel = 0;
		if (!this.firstSet)
		{
			if (this.oldQuaternion != t.localRotation)
			{
				this.firstSet = false;
				this.rotation = t.localEulerAngles;
				this.oldQuaternion = t.localRotation;
			}
		}
		Vector3 position = EditorGUILayout.Vector3Field("Position", t.localPosition);
		this.rotation = EditorGUILayout.Vector3Field("Rotation", this.rotation);
		Vector3 scale = EditorGUILayout.Vector3Field("Scale", t.localScale);
		EditorGUILayout.TextArea("Test");
		if (GUI.changed)
		{
			Undo.RegisterUndo(t, "Transform Change");
			this.rotation = this.FixIfNaN(this.rotation);
			t.localPosition = this.FixIfNaN(position);
			t.localEulerAngles = this.rotation;
			this.oldQuaternion = t.localRotation;
			t.localScale = this.FixIfNaN(scale);
		}
		EditorGUIUtility.LookLikeInspector();
	}
	
	
	private Vector3 FixIfNaN (Vector3 v)
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

