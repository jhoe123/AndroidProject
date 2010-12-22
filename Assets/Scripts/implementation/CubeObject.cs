using UnityEngine;
using System.Collections;

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

	TouchState mTouchState;
	Vector2 mElapsedTouch;
	Vector3 mRotation;
	float mTargetRotation;
	float mStartRotation;
	float mElapsedTime;
	float mOrigRot;
	int mDragDirection = 0;
	
	protected override void Awake()
	{
		base.Awake();
		mRotation = mTransform.eulerAngles;
		InputManager.mMouseEvent += OnMouseInput;	
	}
	
	void OnMouseInput( InputType pType, int pIndex, bool pIsPressed )
	{
		if( pIndex == 0 )
		{
			if( pIsPressed )
			{
				mTouchState = TouchState.Touch;
				mElapsedTouch = Input.mousePosition;
				mOrigRot = mRotation.y;
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
	
	//to be called when the cube will be rotated
	//@param: the value to be added to the euler rotation of cube
	protected virtual void OnRotate( float pVal )
	{
		mRotation.y = pVal;
		mTransform.eulerAngles = mRotation;
	}
	
	void OnTouchInput( InputType pType )
	{
		
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
					if( currentTouch.x > mElapsedTouch.x )
						mDragDirection = -1;
					else
						mDragDirection = 1;
					OnRotate( (mElapsedTouch.x - currentTouch.x) * 0.35f + mRotation.y );
					Debug.Log( mRotation.y - mOrigRot);
				#else
				#endif
				
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
