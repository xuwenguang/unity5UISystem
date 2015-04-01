using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor (typeof(UIManager))]
public class UIManagerEditor : Editor 
{
	void OnEnable()
	{
//		foreach(Delegate d in EditorApplication.playmodeStateChanged.GetInvocationList())
//		{
//			Debug.LogWarning(d.Method.Name);
//			if(d.Method.Name=="CheckIfScreenIsNull")
//			{
//				return;
//			}
//		}
		EditorApplication.playmodeStateChanged += CheckIfScreenIsNull;
	}


	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUI.changed)
		{
			SceneTypeVerification();
			UpdateMasterScenePath();
		}
	}



	/// <summary>
	/// might have bug in here, need to debug
	/// to check if user drag wrong scene item into here
	/// </summary>
	public void SceneTypeVerification()
	{
		var um = target as UIManager;
		//check if the scene objects are unity scene
		for(int i=0;i<um.Screens.Length;i++)
		{
			var s=um.Screens[i].scene;
			if(s!=null)
			{
				string scenePath=AssetDatabase.GetAssetPath(s);
				if(!scenePath.Contains(".unity"))
				{
					EditorApplication.Beep();
					um.Screens[i].scene=null;
					Debug.LogError("the scene field should only contain a unity scene object");
				}

			}
		}
	}
	public void UpdateMasterScenePath()
	{
		//get the reference of the uimanager script
		var uiManager=target as UIManager;

		if(uiManager.LoadMasterSceneOnPlay)
		{
			PlayerPrefs.SetString(UIManager.MasterScenePathKey,EditorApplication.currentScene);
		}
		else
		{
			PlayerPrefs.SetString(UIManager.MasterScenePathKey,"");
		}
		PlayerPrefs.Save();
	}

	public void CheckIfScreenIsNull()
	{
		var um = target as UIManager;
		foreach(UIManager.Screen s in um.Screens)
		{
			if(s.scene==null)
			{
				EditorApplication.isPlaying=false;
				EditorApplication.Beep();
				Debug.LogError("The scene in screens should not be null");
				return;
			}
		}
	}
}
