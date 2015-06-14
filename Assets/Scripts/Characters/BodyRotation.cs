using UnityEngine;
using System.Collections;

public class BodyRotation : MonoBehaviour 
{

	[SerializeField]
	private InputManager input;

	private void Awake()
	{
	}
	
	protected virtual void Update()
	{
//		Vector3 moveDirection = new Vector3(0,-input.RightHorizontal,0);
//		Vector3 rotation = Vector3.zero;
//		rotation += moveDirection;
//		rotation = transform.rotation.eulerAngles + rotation;
//		rotation.y = Mathf.Clamp(rotation.y,230,320);
//		Debug.Log(rotation);
//		transform.rotation = Quaternion.Euler( rotation);

	}
}
