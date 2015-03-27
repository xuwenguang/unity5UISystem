using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(UIManager))]
public class UIManagerEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if(GUI.changed)
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
	}
	
}
