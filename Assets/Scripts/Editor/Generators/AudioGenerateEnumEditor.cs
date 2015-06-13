using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class AudioGenerateEnumEditor : GenerateEnumEditor 
{
	private const string MenuName = "Audioclip generator"; 
	[MenuItem (MenuEntryType.Company + MenuEntryType.Generator + MenuName)]
	public static void Update()
	{
		List<string> files = new List<string>();
		string supportedExtensions = "*.mp3,*.wav";
		foreach (string audioFile in Directory.GetFiles(Application.dataPath, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
		{
			files.Add(Path.GetFileNameWithoutExtension(audioFile));
		}
		Main("Audio","AudioClipEnum",files.ToArray());
	}
}
