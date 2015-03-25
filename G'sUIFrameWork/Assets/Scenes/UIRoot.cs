//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class UIRoot : MonoBehaviour
//{
//	public AnimationClip TransitionInAnimation;
//	public AnimationClip TransitionOutAnimation;
//
//    protected Canvas _canvas;
//    private Animation _animationComp;
//    private Transform _threeDeeObjects;
//
//    public const string DEFAULT_TRANSITION_IN = "Default_In";
//    public const string DEFAULT_TRANSITION_OUT = "Default_Out";
//
//    public event System.Action StartTransitionnInEvent;
//
//    private string _defaultAnimationIn;
//    private string _defaultAnimationOut;
//
//
//
//    protected virtual void Awake()
//    {
//        var eventSystem = GetComponentInChildren<UnityEngine.EventSystems.EventSystem>();
//        if ( eventSystem != null ) Destroy( eventSystem.gameObject );
//
//        Transform rootCanvasTransform = transform.Find( "Canvas" );
//        Z2HDebug.Assert( rootCanvasTransform != null, "Unable to find root canvas! - " + name );
//
//        _canvas = rootCanvasTransform.GetComponent<Canvas>();
//		Z2HDebug.Assert( _canvas != null, "Unable to find canvas component! - " + name );
//
//        // May not have this if no transitions setup
//        //
//        _animationComp = GetComponent<Animation>();
//
//        Destroy( _canvas.worldCamera.gameObject );
//        _canvas.worldCamera = Camera.main;
//
//        _threeDeeObjects = transform.FindChild( "Canvas/3DObjects" );
//
//        SetSceneActiveState( false );
//        
//		Z2HDebug.Assert( name.StartsWith( "Root_" ), "Invalid screen name! Structure must be \"Root_ScreenName\" " + name );
//
//        string sceneName = name.Split( '_' )[1];
//
//        _defaultAnimationIn = string.Format( "UI_{0}_In", sceneName );
//        _defaultAnimationOut = string.Format( "UI_{0}_Out", sceneName );
//
//    }
//
//
//    public IEnumerator _TransitionIn( string clipName )
//    {
//        clipName = ( clipName == DEFAULT_TRANSITION_IN ) ? _defaultAnimationIn : clipName;
//
//        if ( _animationComp != null && _animationComp.GetClip( clipName ) != null )
//		{
//			_animationComp.Play( clipName );
//            while ( _animationComp.IsPlaying( clipName ) )
//            {
//                yield return null;
//            }
//        }
//    }
//
//
//    public IEnumerator _TransitionOut( string clipName, System.Action onFinishCB )
//    {
//        clipName = ( clipName == DEFAULT_TRANSITION_OUT ) ? _defaultAnimationOut : clipName;
//
//        if ( _animationComp != null && _animationComp.GetClip( clipName ) != null )
//        {
//            _animationComp.Play( clipName );
//            while ( _animationComp.IsPlaying( clipName ) )
//            {
//                yield return null;
//            }
//        }
//        if ( onFinishCB != null ) onFinishCB();
//    }
//
//
//    public void SetSceneActiveState( bool isActive )
//    {
//        _canvas.enabled = isActive;
//        Toggle3DObjects( isActive );
//    }
//
//
//    // Called before screen transition in has started
//    //
//    public virtual void OnPreTransitionIn() {}
//    
//
//    // Called when screen transition in has completed
//    //
//    public virtual void OnTransitionInComplete() {}
//    
//
//    // Called before screen is about to transition out
//    //
//    public virtual void OnPreTransitionOut() {}
//
//
//    // Called after screen has transitioned out
//    //
//    public virtual void OnTransitionOutComplete() {}
//
//
//    // Called immediatly once scene canvas renderer becomes enabled after transition
//    //
//    public virtual void OnSceneBecameVisible() {}
//
//
//    // Called immediatly once scene canvas renderer becomes disabled after transition
//    //
//    public virtual void OnSceneBecameInvisible() {}
//
//
//    public virtual void OnPreviousScreenTransitionedOut() {}
//
//
//    // Default screen initializer
//    public virtual IEnumerator Initialize()
//    {
//        yield break;
//    }
//
//
//    protected void PlayAnimation( string clipName )
//    {
//		Z2HDebug.Assert( _animationComp != null, string.Format( "Animation reference is null! {0}", name ) );
//		Z2HDebug.Assert( _animationComp.GetClip( clipName ) != null, string.Format( "Animation comp has no clip namedd {0}", clipName ) );
//
//        _animationComp.Play( clipName );
//    }
//
//
//    private void Toggle3DObjects( bool active )
//    {
//        if ( _threeDeeObjects != null )
//        {
//            _threeDeeObjects.gameObject.SetActive( active );
//        }
//    }
//
//
//    public virtual void BackButtonSelected()
//    {
//		//move background back, can be commented if we do not use scrolling background
//
//    }
//    
//
//    public void ReparentGameObjectToCanvas( GameObject gameObject )
//    {
//        gameObject.transform.SetParent( _canvas.transform, true );
//    }
//
//
//    public void StartTranisitonInAnimationEvent()
//    {
//        if ( StartTransitionnInEvent != null ) StartTransitionnInEvent();
//    }
//
//	public void ResetAnimationPosition()
//	{
//		StartCoroutine (_ResetAnimationPosition());
//	}
//
//	IEnumerator _ResetAnimationPosition()
//	{
//		_animationComp.Play();
//		_animationComp.Rewind ();
//		yield return null;
//		_animationComp.Stop();
//	}
//}
