using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// this code makes sure each of the scenes in the list can be automically added into build setting
/// need to add master scene into build setting manully though
/// </summary>
public class UIManager : MonoBehaviour {

	public UnityEngine.Object[] Scenes;
	public UnityEngine.Object InitialScene;
	public UnityEngine.Object[]InvalidBackTransitionScene;


	[Serializable]
	public struct NamedImage {
		public string name;
		public Texture2D image;
	}
	
	public NamedImage[] pictures;


	// Use this for initialization
//	void Awake()
//	{
//#if UNITY_EDITOR
//		EditorApplication.playmodeStateChanged += UpdateBuildSettingScenes;
//#endif
//
//		EventSystemComp=GameObject.Find("Root_Boot/EventSystem").GetComponent<EventSystem>();
//		EventSystemComp.enabled = false;
//		
//		BootCanvasGO = GameObject.Find ("Boot_Canvas");
//		UICamera = GameObject.Find ("Root_Boot/Camera").GetComponent<Camera>();
//	}
//#if UNITY_EDITOR
//	public void UpdateBuildSettingScenes()
//	{
//		List<EditorBuildSettingsScene> sceneList = new List<EditorBuildSettingsScene> ();
//		foreach(EditorBuildSettingsScene s in EditorBuildSettings.scenes)
//		{
//			sceneList.Add(s);
//		}
//		foreach(UnityEngine.Object scene in Scenes)
//		{
//			bool inList=false;
//			string scenePath=AssetDatabase.GetAssetPath(scene);
//			EditorBuildSettings.scenes.ToList().ForEach(x=>{
//				if(x.path==scenePath)
//				{
//					inList=true;
//				}
//			});
//
//			if(!inList)
//				sceneList.Add(new EditorBuildSettingsScene(scenePath,true));
//		}
//		EditorBuildSettings.scenes = sceneList.ToArray ();
//	}
//#endif
//
////	//setup the initial scene for back button, will not back to anyother screens after reach initial screen
////	//means you can not go back in this screen
////	public List<Scenes> InitialScreenList=new List<Scenes>
////	{
////		//		Scenes.TestScene,
////		Scenes.BibaStart,
////	};
////	
////	//there are some screens can not be go back to, like the ui screen inside game play, specify this list in here
////	//you can not go back to this screen
////	//can also set the last screen in here(can't be transition back to)
////	public List<string> InvalidBackTransitionScreenList=new List<string>()
////	{
////		"BibaViper"
////	};
////	
//	
//	//screen list, saving the uiscreen script and screen,string should be the scene name
//	private Dictionary<string, UIRoot> _scenesDict;
//
//	
//	//[note] commented all the places that use canvasRoot, because want to add more flexibility to the ui system that all the screens can use different camera settings, also avoided the Scrollrect flickering problem
//	//if problem occurs, will add this back
//	//	public Canvas CanvasRoot;
//	private bool _isLoading;
//	public bool IsLoadingInProgress { get { return _isLoading; } private set { _isLoading = value; } }
//	
//	public EventSystem EventSystemComp;
//	public GameObject BootCanvasGO;
//	public Camera UICamera;
//	private string _currentScene;
//	private string _previousScene;
//	
//	//for toggle global input
//	public bool isBackgroundMoving=false;
//	
//	public System.Action OnDisableBackButton;
//	public System.Action OnEnableBackButton;
//	
//	//to store all the previous screens (for back btn)
//	private List<string> previousScreenList = new List<string>();
//	
//	//event, will be called when the screen changes, parameter is the new screen going to be transition in
//	public event System.Action<string> OnSceneChange; //the string should be the scene name
//		
//	//load all the screens
//	public IEnumerator Setup(System.Action setupCB)
//	{
//		_scenesDict=new Dictionary<string,UIRoot>();
//		_isLoading = true;
//		
//		foreach ( string sceneName in Scenes )
//		{
//			yield return Application.LoadLevelAdditiveAsync( sceneName );
//			
//			GameObject sceneGameObject = GameObject.Find( "Root_" + sceneName );
//			Z2HDebug.Assert( sceneGameObject != null, "can not find screen : " + sceneName );
//			
//			UIRoot sceneScript = sceneGameObject.GetComponent<UIRoot>();
//			
//			_scenesDict.Add( sceneName, sceneScript );
//		}
//		_isLoading = false;
//		
//		if (setupCB != null)
//		{
//			setupCB ();
//		}
//	}
//	
//	public void ShowInitialScreen()
//	{
//		LoadingSequence.loadingSequenceData.FindAll( seq => seq.StartEnabled )
//			.ForEach( sequenceData => ShowScreen( sequenceData.Scene ) );
//		
//		//Destory boot canvas
//		//
//		UnityEngine.Object.Destroy( BootCanvasGO );
//	}
//	
//	public void ToggleGlobalInput( bool inputEnabled )
//	{
//		EventSystemComp.enabled=inputEnabled;
//	}
//	
//	/// <summary>
//	/// Use named parameters to customize function behaviour
//	/// </summary>
//	/// <typeparam name="T"></typeparam>
//	/// <param name="animTransitionIn"></param>
//	/// <param name="animTransitionOut"></param>
//	/// <param name="setupCB"></param>
//	public void ShowScreen<T>( string animTransitionIn = UIRoot.DEFAULT_TRANSITION_IN, string animTransitionOut = UIRoot.DEFAULT_TRANSITION_OUT, System.Action<T> setupCB = null ) where T : UIRoot
//	{
//		Scenes scene = _scenesTypeDict[typeof( T )];
//		
//		
//		PushPreviousScreenToPreviousScreenList(scene);
//		
//		StartCoroutine( _TransitionScreenIn( scene, animTransitionIn, animTransitionOut, () =>
//		                                                     {
//			if ( setupCB != null )
//			{
//				setupCB( (T)_scenesDict[scene] );
//			}
//		} ) );
//		
//	}
//	
//	public void ShowScreen( string sceneName, string animTransitionIn = UIRoot.DEFAULT_TRANSITION_IN, string animTransitionOut = UIRoot.DEFAULT_TRANSITION_OUT, System.Action setupCB = null )
//	{
//		
//		PushPreviousScreenToPreviousScreenList(scene);
//		
//		StartCoroutine( _TransitionScreenIn( scene, animTransitionIn, animTransitionOut, null ) );
//	}
//	
//	IEnumerator _TransitionScreenIn( string newSceneName, string stateName, string outStateName, System.Action setupCB )
//	{
//		
//		ToggleGlobalInput( false );
//		
//		UIRoot screenIn = _scenesDict[newScene];
//		UIRoot screenOut = _scenesDict.ContainsKey( _currentScene ) ? _scenesDict[_currentScene] : null;
//		
//		// Overlay or going from boot -> splash, show immediatly
//		//
//		if( IsOverlay( newScene ) || _currentScene == Scenes.None )
//		{
//			yield return StartCoroutine( _PerformTransition( screenIn, setupCB ) );
//		}
//		else // Perform full transition between current and new
//		{       
//			yield return StartCoroutine( _PerformTransition( screenIn, screenOut, outStateName, setupCB ) );
//		}
//		
//		
//		screenIn.SetSceneActiveState( true );
//		screenIn.OnSceneBecameVisible();
//		
//		// Update scene vars if not transitioning into overlay
//		//
//		if ( !IsOverlay( newScene ) )
//		{         
//			_previousScene = _currentScene;
//			_currentScene = newScene;
//			
//			if ( OnSceneChange != null ) OnSceneChange( newScene );
//			
//		}
//		
//		
//		yield return StartCoroutine( screenIn._TransitionIn( stateName ) );
//		screenIn.OnTransitionInComplete();
//		
//		
//		if(!isBackgroundMoving)
//		{
//			ToggleGlobalInput( true );
//		}
//	}
//	
//	//to see the screen is a UIOverlay screen or not (uioverlay screen is like pop up menus)
//	private bool IsOverlay( string sceneName )
//	{
//		if ( !_scenesDict.ContainsKey( scene ) )
//		{
//			return false;
//		}
//		return _scenesDict[scene] is UIOverlay;
//	}
//	
//	IEnumerator _PerformTransition( UIRoot screenIn, UIRoot screenOut, string outStateName, System.Action setupCB )
//	{
//		screenOut.OnPreTransitionOut();
//		screenIn.OnPreTransitionIn();
//		
//		// Callback which allows screen coming in to start its transition in 
//		// animation early
//		//
//		bool startTransitionIn = false;
//		System.Action fn = () =>
//		{
//			startTransitionIn = true;
//		};
//		screenOut.StartTransitionnInEvent += fn;
//		
//		
//		if ( setupCB != null )
//		{
//			setupCB();
//		}
//		
//		// Transition out current screen
//		//
//		StartCoroutine( screenOut._TransitionOut( outStateName, () =>
//		                                                          {
//			startTransitionIn = true;
//			
//			screenOut.OnTransitionOutComplete();
//			screenOut.SetSceneActiveState( false );
//			screenOut.OnSceneBecameInvisible();
//			
//			screenIn.OnPreviousScreenTransitionedOut();
//		}
//		) );
//		
//		// Wait until transition finishes or we manually trigger out
//		while ( !startTransitionIn )
//		{
//			yield return null;
//		}
//		
//		screenOut.StartTransitionnInEvent -= fn;
//		
//		yield return StartCoroutine( SetupScreenIn( setupCB, screenIn ) );
//	}
//	
//	
//	IEnumerator _PerformTransition( UIRoot screenIn, System.Action setupCB )
//	{
//		if ( setupCB != null )
//		{
//			setupCB();
//		}
//		
//		screenIn.OnPreTransitionIn();
//		yield return StartCoroutine( SetupScreenIn( setupCB, screenIn ) );
//		screenIn.OnTransitionInComplete();
//	}
//	
//	IEnumerator SetupScreenIn( System.Action setupCB, UIRoot screenIn )
//	{
//		if ( screenIn == null )
//		{
//			yield break;
//		}
//		
//		//if ( setupCB != null )
//		//{
//		//    setupCB();
//		//}
//		yield return StartCoroutine( screenIn.Initialize() );
//	}
//	
//	//use to hide overlay screens, like top menu and message box
//	public void HideScreen<T>( string animTransitionOut = UIRoot.DEFAULT_TRANSITION_OUT ) 
//	{
//		var scene = _scenesTypeDict[typeof( T )];
//		StartCoroutine( _OverlayTransitionOut( scene, Scenes.None, animTransitionOut, null ) );
//	}
//	
//	//transition out function only for overlay screens	
//	IEnumerator _OverlayTransitionOut( string currentSceneName, string newSceneName, string stateName, System.Action setupCB )
//	{
//		UIRoot screenOut = _scenesDict[currentScene];
//		UIRoot screenIn = null;
//		
//		screenOut.OnPreTransitionOut();
//		
//		if ( newScene != Scenes.None )
//		{
//			screenIn = _scenesDict[newScene];
//			screenIn.OnPreTransitionIn();
//		}
//		
//		// Transition out current screen
//		// do not put all the screen active state function in the transition out function, this can separate the functionality of the code, will be easier to maintance in the future
//		yield return StartCoroutine( screenOut._TransitionOut( stateName, () =>
//		                                                                       {
//			screenOut.OnTransitionOutComplete();
//			screenOut.SetSceneActiveState( false );
//			screenOut.OnSceneBecameInvisible();
//			
//		}));
//		
//		
//		screenOut.OnTransitionOutComplete();
//		
//		yield return StartCoroutine( SetupScreenIn( setupCB, screenIn ) );
//	}
//	
//	
//	#region back button
//	
//	/// <summary>
//	/// should do all the functions in the screen which have the back button
//	/// use event & delegate
//	/// update visiability and etc.
//	/// </summary>
//	public void DisableBackButton()
//	{
//		if ( OnDisableBackButton != null ) OnDisableBackButton();
//		//clear previous screen list every time show or hide back button
//		ClearPreviousScreenList ();
//	}
//	
//	public void EnableBackButton()
//	{
//		if ( OnEnableBackButton != null ) OnEnableBackButton();
//	}
//	
//	
//	//should set the default parameter as the ui screen's default transition back animation string
//	public void BackButtonSelected(string animTransitionIn = null, string animTransitionOut = null)
//	{
//		//work on the move back animation
//		//default animation is null, or we can setup a default animation name
//		int lastIndex = previousScreenList.Count - 1;
//		
//		try
//		{
//			string previousScreenName = previousScreenList[lastIndex];
//			Scenes lastScreenInList=previousScreenName.ToEnum<Scenes>();
//			if(lastScreenInList!=_currentScene)
//			{
//				if(animTransitionIn==null)
//				{
//					//clear previous animation state
//					ResetScreenAnimation(lastScreenInList);
//					animTransitionIn=UIRoot.DEFAULT_TRANSITION_IN;
//				}
//				if(animTransitionOut==null)
//				{
//					ResetScreenAnimation(_currentScene);
//					animTransitionOut=UIRoot.DEFAULT_TRANSITION_OUT;
//				}
//				//do not use show screen is because do not want to add this screen to the previous screen list
//				if(CanBeTransitionBackTo(lastScreenInList))
//				{
//					//call the function on UIScreen script, the move background function is in there
//					StartCoroutine( _TransitionScreenIn( lastScreenInList, animTransitionIn, animTransitionOut, 
//					                                                     ()=>{_scenesDict[_currentScene].BackButtonSelected();} ) );
//					previousScreenList.RemoveAt (lastIndex);
//				}
//				//remove the last one in the list after press back button once
//			}
//			
//		}
//		catch (ArgumentOutOfRangeException e)
//		{
//			Debug.LogWarning("nothing in the previousScreenList, can not go back");
//		}
//	}
//	
//	public void ResetScreenAnimation(string sceneName)
//	{
//		UIRoot screenScript = _scenesDict[scene];
//		screenScript.ResetAnimationPosition ();
//	}
//	
//	
//	public bool CanBeTransitionBackTo(string sceneName)
//	{
//		return !InvalidBackTransitionScreenList.Contains (sceneName);
//	}
//	
//	
//	void PushPreviousScreenToPreviousScreenList( string newSceneName )
//	{
//		//push current screen to the previous list enless it is one of the initial screens
//		//if it is initial screen, clear previous screenlist then add itself in
//		if(!IsOverlay(newScene))
//		{
//			if(_currentScene==Scenes.None)
//			{
//				//when currentScene is None, means this is the first time calling show screen function through showInitialScreen function
//				//do not add any screens into the list
//				ClearPreviousScreenList();
//			}
//			else
//			{
//				//screens in the invalid list can not be transition back to
//				if(CanBeTransitionBackTo(_currentScene))
//				{
//					previousScreenList.Add (_currentScene.ToString());
//				}
//				else
//				{
//					ClearPreviousScreenList();
//				}
//				
//				//if new screen is in the initial list, clear the list and make the newScene the first one, so it will always be the last screen you can transit to
//				if(IsScreenInInitialList(newScene))
//				{
//					ClearPreviousScreenList();
//				}
//			}
//			
//		}
//		
//	}
//	
//	public void ClearPreviousScreenList()
//	{
//		if (previousScreenList != null)
//		{
//			previousScreenList.Clear();
//		}
//	}
//	
//	//screen in the initialList can not transition back to other screens, but the back button could appear on that screen
//	//should manually set it up
//	public bool IsScreenInInitialList(string currentSceneName)
//	{
//		return InitialScreenList.Contains (currentScene);
//	}
//	
//	public bool CanTransitionToScreen(string newSceneName)
//	{
//		if(_currentScene==newScene)
//		{
//			Debug.LogWarning("Attempt to transition to current screen : "+newScene.ToString());
//			return false;
//		}
//		return true;
//	}
//	#endregion
}



