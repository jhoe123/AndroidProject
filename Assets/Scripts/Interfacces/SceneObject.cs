using UnityEngine;
using System.Collections;

public class SceneObject : MonoBehaviour {
	
	public Transform mTransform;
	
	protected virtual void Awake()
	{
		mTransform = transform;	
	}
	
	//use to set the current state of the object
	public virtual void SetState( ObjectState pState, object[] pParams )
	{
	}
	
	protected virtual void FixedUpdate()
	{
	}
}
