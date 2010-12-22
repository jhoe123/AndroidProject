using UnityEngine;
using System.Collections;

//delegate to be called when recieved a input
//@param 1: the input type
//@param 2: the touches
public delegate void TouchCallbackDelegate( InputType pType, Touch[] pTouches );

//to be called when a mouse button was pressed
//@param 1: the input type
//@param 2: the mouse index that was pressed
//@param 3: true if the button was pressed false if released
public delegate void MouseCallbackDelegate( InputType pType, int pMouseIndex, bool mIsPressed);

public delegate void KeyboardCallbackDelegate( InputType pType, char[] pInputs, int pInputCount);