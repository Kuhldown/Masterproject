using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;
using UnityEngine;
using UnityEditor;

public class GenerateEnumEditor : EditorWindow 
{
	private const string FilePath = "/Scripts/Enums/";

	private static string NamespaceName{get;set;}
	private static string ClassName{get;set;}
			
	private static CodeCompileUnit codeBase;
	private static CodeNamespace enumNamespace;
	private static CodeTypeDeclaration codeType;

	protected static void Main(string namespaceName,string className, string[] enums) 
	{
		NamespaceName = namespaceName;
		ClassName = className;

		codeBase = new CodeCompileUnit();
		enumNamespace = new CodeNamespace(namespaceName);
		codeType = new CodeTypeDeclaration(Path.GetFileNameWithoutExtension(className)) { IsEnum = true };

		GenerateEnums (enums);
		GenerateFile ();
	}

	private static CodeMemberField CreateNewEnum (string name,int id)
	{
		CodeMemberField field = new CodeMemberField 
		{
			Name = name,
			InitExpression = new CodePrimitiveExpression (id)
		};
		return field;
	}

	private static void GenerateEnums (string[] enums)
	{
		for(int i = 0; i < enums.Length;i++)
		{
			codeType.Members.Add (CreateNewEnum (enums[i], i));
		}
	}

	private static CodeGeneratorOptions Options
	{
		get
		{
			CodeGeneratorOptions options = new CodeGeneratorOptions();
			options.BracingStyle = "C";
			options.BlankLinesBetweenMembers = false;
			return options;
		}
	}
		
	private static void GenerateFile()
	{
		CSharpCodeProvider provider = new CSharpCodeProvider ();
		string path = Application.dataPath + FilePath + ClassName + ".cs";
		codeBase.ReferencedAssemblies.Add (typeof(string).Assembly.Location);
		codeBase.Namespaces.Add (enumNamespace);
		enumNamespace.Types.Add (codeType);

		using (FileStream fs = new FileStream (path, FileMode.Create, FileAccess.Write)) 
		{
			using (StreamWriter sw = new StreamWriter (fs)) 
			{
				provider.GenerateCodeFromCompileUnit (codeBase, sw, Options);
			}
		}

		Debug.Log("automatically update enum file: " + path);
	}
}

