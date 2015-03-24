using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

/// <summary>
/// this code makes sure each of the scenes in the list can be automically added into build setting
/// need to add master scene into build setting manully though
/// </summary>
public class BuildSettingTest : MonoBehaviour {

	public Object[] Scenes;
	// Use this for initialization
	void Awake()
	{
#if UNITY_EDITOR
		EditorApplication.playmodeStateChanged += UpdateBuildSettingScenes;
#endif
	}
#if UNITY_EDITOR
	public void UpdateBuildSettingScenes()
	{
		List<EditorBuildSettingsScene> sceneList = new List<EditorBuildSettingsScene> ();
		foreach(EditorBuildSettingsScene s in EditorBuildSettings.scenes)
		{
			sceneList.Add(s);
		}
		foreach(Object scene in Scenes)
		{
			bool inList=false;
			string scenePath=AssetDatabase.GetAssetPath(scene);
			EditorBuildSettings.scenes.ToList().ForEach(x=>{
				if(x.path==scenePath)
				{
					inList=true;
				}
			});

			if(!inList)
				sceneList.Add(new EditorBuildSettingsScene(scenePath,true));
		}
		EditorBuildSettings.scenes = sceneList.ToArray ();
	}
#endif
}
