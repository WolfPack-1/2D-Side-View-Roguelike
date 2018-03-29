using UnityEngine;
using UnityEditor;
using System.IO;
 
public static class ScriptableObjectExtension
{
	public static void CreateAsset<T> () where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T> ();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/Data/ScriptableObject/" + typeof(T).ToString().Replace("Data","") + ".asset");
 
		AssetDatabase.CreateAsset (asset, assetPathAndName);
 
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}