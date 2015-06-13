using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	[SerializeField]
	private float rotateSpeed = 200.0f;
	[SerializeField]
	private float moveSpeed = 600.0f;

	private InputManager input;
	private Animator animator;
	private Rigidbody rigidbody;

	private Vector3 movement;
	private Vector3 moveDirection;
	private float force;

	private void Awake()
	{
		input = GetComponent<InputManager>();
		animator = GetComponentInChildren<Animator>();
		rigidbody = GetComponent<Rigidbody>();
	}
	
	private void Start()
	{

	}

	protected virtual void FixedUpdate()
	{
		float speed = Mathf.Clamp01( Mathf.Abs(input.Horizontal) + Mathf.Abs(input.Vertical));
		Vector3 direction = transform.TransformDirection(Vector3.forward);

		force = Mathf.Lerp(force,speed,Time.deltaTime);

		animator.SetFloat("movement",force);

		speed = force * Time.deltaTime * moveSpeed;
		direction *= speed;
		direction.y = rigidbody.velocity.y;
	
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
}
