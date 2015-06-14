using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class Character : MonoBehaviour 
{
	private Movement movement;
	private Animator animator;

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		movement = GetComponent<Movement>();
	}

	private void Dead()
	{
		animator.SetTrigger("death");
	}
}
