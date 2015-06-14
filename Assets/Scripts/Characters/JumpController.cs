using UnityEngine;
using System.Collections;

public class JumpController : MonoBehaviour 
{

	public void OnJumpStart()
	{
		Transform parent = transform.parent;
		parent.GetComponent<Collider>().isTrigger = true;
		parent.GetComponent<Rigidbody>().useGravity = false;
		parent.GetComponent<InputManager>().StartVibration(Vector2.one,0.1f);
	}
	
	public void OnJumpEnd()
	{
		Transform parent = transform.parent;
		parent.GetComponent<Collider>().isTrigger = false;
		parent.GetComponent<Rigidbody>().useGravity = true;
		parent.GetComponent<InputManager>().StartVibration(Vector2.one,0.1f);
	}
}
