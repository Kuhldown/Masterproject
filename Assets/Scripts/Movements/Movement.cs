using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour 
{
	[SerializeField]
	private float rotateSpeed = 200.0f;
	[SerializeField]
	private float moveSpeed = 600.0f;
	[SerializeField]
	private float jumpSpeed = 600.0f;
	
	private InputManager input;
	private Animator animator;
	private Rigidbody rigidbody;
	private Collider collider;
	
	private Vector3 movement;
	private Vector3 moveDirection;
	private float force;

	private bool IsGrounded {get;set;}
	private void Awake()
	{
		input = GetComponent<InputManager>();
		animator = GetComponentInChildren<Animator>();
		rigidbody = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
	}
	
	protected virtual void FixedUpdate()
	{
		float speed =  Mathf.Clamp01( Mathf.Abs(input.Horizontal) + Mathf.Abs(input.Vertical));
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		float sideStep = input.Right - input.Left;

		force = Mathf.Lerp(force,speed,Time.deltaTime);
		
		animator.SetFloat("movement",force);
		animator.SetFloat("sidestep",sideStep);

		speed = force * Time.deltaTime * moveSpeed;
		direction *= speed;
		direction.x += sideStep;
		direction.y = rigidbody.velocity.y;
		Jump();
		rigidbody.velocity = direction;
	}
	
	protected virtual void Update()
	{
		Vector3 moveDirection = new Vector3(input.Horizontal,0,input.Vertical);
		if (moveDirection != Vector3.zero)
		{
			movement = Vector3.RotateTowards(movement, moveDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000); //Direction
			
			movement.Normalize();
			
			transform.rotation = Quaternion.LookRotation(movement);
		}
	}

	private void Jump()
	{
		if(input.JumpDown && IsGrounded)
		{
			animator.SetTrigger("jump");
		}
	}

	/// <summary>
	///OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
	///In contrast to OnTriggerEnter, OnCollisionEnter is passed the Collision class and not a Collider. 
	///The Collision class contains information about contact points, impact velocity etc. 
	///If you don't use collisionInfo in the function, leave out the collisionInfo parameter as this avoids unneccessary calculations. 
	///Notes: Collision events are only sent if one of the colliders also has a non-kinematic rigidbody attached. 
	///Collision events will be sent to disabled MonoBehaviours, to allow enabling Behaviours in response to collisions.
	/// </summary>
	protected virtual void OnCollisionStay(Collision other) 
	{
		IsGrounded = true;
	}

	/// <summary>
	///OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
	///In contrast to OnTriggerExit, OnCollisionExit is passed the Collision class and not a Collider. 
	///The Collision class contains information about contact points, impact velocity etc. 
	///If you don't use collisionInfo in the function, leave out the collisionInfo parameter as this avoids unneccessary calculations. 
	///Notes: Collision events are only sent if one of the colliders also has a non-kinematic rigidbody attached. 
	///Collision events will be sent to disabled MonoBehaviours, to allow enabling Behaviours in response to collisions.
	/// </summary>
	protected virtual void OnCollisionExit(Collision other) 
	{
		IsGrounded = false;
	}
}