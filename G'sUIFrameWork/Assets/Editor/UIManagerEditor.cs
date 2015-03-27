using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(UIManager))]
public class UIManagerEditor : Editor 
{
	void OnEnable()
	{
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
			if(s=null)
			{
				if(!s.name.Contains(".unity"))
				{
					EditorApplication.Beep();
					s=null;
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
			PlayerPrefs.SetString(UIManager.MasterScenePath,EditorApplication.currentScene);
		}
		else
		{
			PlayerPrefs.SetString(UIManager.MasterScenePath,"");
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
				Debug.LogError("The scene in UIManager screens should not be null");
			}
		}
	}
}
