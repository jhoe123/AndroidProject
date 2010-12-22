using UnityEngine;
using System.Collections;

public enum InputType
{
	Touch,
	Mouse,
	Keyboard,
	Joystick,	
}

public enum ObjectState
{
	//scene objects state
	STATE_ACTIVE = 0,
	STATE_INACTIVE = 1,
	STATE_IDLE = 2,
	STATE_HIT = 3,
}