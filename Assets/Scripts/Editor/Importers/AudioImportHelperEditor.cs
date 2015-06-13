using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text;
[InitializeOnLoad]
public class AudioImportHelperEditor : AssetPostprocessor 
{
	private const string ExtensionMP3 = ".mp3";
	private const string ExtensionWAV = ".wav";
	private const string MusicPath = "Assets/Audio/Music/";
	private const string SoundPath = "Assets/Audio/Sound/";
	
	public AudioImportHelperEditor()
	{
		CreateFolderIsNotExist(MusicPath);
		CreateFolderIsNotExist(SoundPath);
	}

	private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) 
	{
		importedAssets = GetAllAudioPaths(importedAssets);
		movedAssets = GetAllAudioPaths(movedAssets);
		deletedAssets = GetAllAudioPaths(deletedAssets);

		OnChangeAudioName(importedAssets);
		OnChangeAudioName(movedAssets);
	}

	private static string[] GetAllAudioPaths(string[] assets)
	{
		List<string> paths = new List<string>();
		foreach(string asset in assets)
		{
			string path = Path.GetExtension (asset);
			if(path == ExtensionMP3 || path == ExtensionWAV)
			{
				paths.Add(asset);
			}
		}
		return paths.ToArray();
	}

	private static void OnChangeAudioName (string[] paths)
	{
		foreach (string path in paths) 
		{
			string fileName = Path.GetFileName(path);
			switch (Path.GetExtension (path)) 
			{
			case ExtensionMP3:
				RenameAudioClip(path);
				AssetDatabase.MoveAsset(path,MusicPath + fileName );
				break;
			case ExtensionWAV:
				RenameAudioClip(path);
				AssetDatabase.MoveAsset(path,SoundPath + fileName);
				break;
			}
		}
	}

	private static void OnDeletedAsset(string[] paths)
	{
		AudioGenerateEnumEditor.Update();
	}

	private static void CreateFolderIsNotExist(string path)
	{
		if(!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
	}

	private static void RenameAudioClip(string path)
	{
		string directory = GetDirectoryName (path);
		string[] names = GetFileName (path);
		string group = names[0];
		string name = names[names.Length - 1];

		if (directory != group) 
		{
			directory = CheckCurrentStringForProblems(directory);
			name = CheckCurrentStringForProblems(name);
			AssetDatabase.RenameAsset (path, directory + '_' + name);
			AudioGenerateEnumEditor.Update();
		}
	}

	private static string CheckCurrentStringForProblems(string text)
	{
		char[] elements = new char[]{'_',' ','*','+','-','/','%','!','"','§','$','(',')','=','?','#','€',':','.','{','}','\\','[',']'};
		string[] splits = text.Split(elements,System.StringSplitOptions.RemoveEmptyEntries);

		text = "";
		foreach(string split in splits)
		{
			text += UppercaseFirst(split);
		}
		return text;
	}
	
	private static string GetDirectoryPath(string path)
	{
		return Path.GetDirectoryName(path);
	}

	private static string GetDirectoryName(string path)
	{
		return Path.GetFileName(GetDirectoryPath(path)); 
	}

	private static string[] GetFileName(string path)
	{
		return Path.GetFileNameWithoutExtension(path).Split('_');
	}

	static string UppercaseFirst(string text)
	{
		// Check for empty string.
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		// Return char and concat substring.
		return char.ToUpper(text[0]) + text.Substring(1);
	}
}
