using UnityEngine;
using System.Collections;

/*Input registry. has callbacks for inputs*/
public class InputManager : MonoBehaviour {

	//input callbacks
	public static TouchCallbackDelegate mTouchEvent;					//to be called when triggered a touch events	
	public static MouseCallbackDelegate mMouseEvent;					//to be called when triggered mouse events
	public static KeyboardCallbackDelegate mKeyboardEvent;				//to be called when triggered keyboard events
	
	public static string mLastKeyboardInput = "";
	public static bool[] mMouseIndexPressed = {false, false, false};	//true if the current mouse id pressed		
	public static Touch[] mTouches;
	
	//to be called every frame
	void Update()
	{
		#if UNITY_EDITOR
				
			if( mMouseEvent != null )
			{
				bool isPressed = Input.GetMouseButton( 0);
				if( mMouseIndexPressed[0] != isPressed )
				{
					mMouseEvent( InputType.Mouse, 0, isPressed );
					mMouseIndexPressed[0] = isPressed;
				}
					
				isPressed = Input.GetMouseButton( 1);
				if( mMouseIndexPressed[1] != isPressed )
				{
					mMouseEvent( InputType.Mouse, 1, isPressed );
					mMouseIndexPressed[1] = isPressed;
				}
					
				isPressed = Input.GetMouseButton( 2);
				if( mMouseIndexPressed[2] != isPressed )
				{
					mMouseEvent( InputType.Mouse, 2, isPressed );
					mMouseIndexPressed[2] = isPressed;
				}
			}
			
			string mCurrentKeys = Input.inputString;
			if( mKeyboardEvent != null && mLastKeyboardInput != mCurrentKeys )
			{
				mLastKeyboardInput = mCurrentKeys;
				mKeyboardEvent( InputType.Keyboard, mCurrentKeys.ToCharArray() , mCurrentKeys.Length );
			}
		#else
			#if UNITY_ANDROID
				if( mTouchEvent != null )
				{
					Touch[] touches = Input.touches;
					if( touches.Length > 0 && touches != mTouches )
					{
						mToucheEvent( InputType.Touch, touches );
						mTouches = touches;	
					}
				}
			#endif
		#endif	
	}
}
