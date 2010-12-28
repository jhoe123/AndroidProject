using UnityEngine;
using System.Collections;

//cube that displays the current activity view
public class CubeObject : SceneObject {
	enum TouchState
	{
		Nothing,
		Touch,
		UnTouch
	}
	
	public Texture mFront;
	public Texture mLeft;
	public Texture mBack;
	public Texture mRight;
	
	ArrayList mActivities;
	int mActivityIndex;
	
	//use for doing cube rotations
	TouchState mTouchState;				//the current touch state
	Vector2 mElapsedTouch;				
	Vector3 mRotation;					//the current rotation
	float mTargetRotation;
	float mStartRotation;
	float mElapsedTime;
	
	protected override void Awake()
	{
		base.Awake();
		mRotation = mTransform.eulerAngles;
		
		#if UNITY_EDITOR
			InputManager.mMouseEvent += OnMouseInput;	
		#else
			InputManager.mTouchEvent += OnTouchInput;
		#endif
	}
	
	#if UNITY_EDITOR
	void OnMouseInput( InputType pType, int pIndex, bool pIsPressed )
	{
		if( pIndex == 0 )
		{
			if( pIsPressed )
			{
				mTouchState = TouchState.Touch;
				mElapsedTouch = Input.mousePosition;
				//mOrigRot = mRotation.y;
			}
			else 
			{
				mTouchState = TouchState.UnTouch;
				mStartRotation = mTransform.eulerAngles.y;
				float rot = (mStartRotation % 90);
				//Debug.Log(  " " + (70 * mDragDirection));
				if(rot > 45 )
					mTargetRotation =  mStartRotation - rot + 90;
				else
					mTargetRotation = mStartRotation - rot;
				mElapsedTime = Time.time;
			}
		}	
	}
	#endif
	
	//to be called when the cube will be rotated
	//@param: the value to be added to the euler rotation of cube
	protected virtual void OnRotate( float pVal )
	{
		mRotation.y = pVal;
		mTransform.eulerAngles = mRotation;
	}
	
	void OnTouchInput( InputType pType, Touch[] pTouches )
	{
		Touch touch = pTouches[0];
		if(touch.phase == TouchPhase.Began )
		{
			mTouchState = TouchState.Touch;
			mElapsedTouch = touch.position;
			//mOrigRot = mRotation.y;	
		}else
		{
			if( touch.phase == TouchPhase.Ended )
			{
				mTouchState = TouchState.UnTouch;
				mStartRotation = mTransform.eulerAngles.y;
				float rot = (mStartRotation % 90);

				if(rot > 45 )
					mTargetRotation =  mStartRotation - rot + 90;
				else
					mTargetRotation = mStartRotation - rot;
				mElapsedTime = Time.time;	
			}	
		}
	}
	
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		Vector2 currentTouch = Vector2.zero;
		switch( mTouchState)
		{
			case TouchState.Touch:
				mTargetRotation = 0;
				#if UNITY_EDITOR
					currentTouch = Input.mousePosition;
				#else
					currentTouch = Input.touches[0].position;
				#endif
				
					
				OnRotate( (mElapsedTouch.x - currentTouch.x) * 0.35f + mRotation.y );
				//Debug.Log( mRotation.y - mOrigRot);
				mElapsedTouch = currentTouch;
				break;
			
			case TouchState.UnTouch:
				
				float time = (Time.time - mElapsedTime) / 0.3f;
				OnRotate( Mathf.Lerp( mStartRotation, mTargetRotation, time) );	
				
				if( time >= 1)
				
					mTouchState = TouchState.Nothing;	
					
				break;
		}
	}
}
