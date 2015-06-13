using UnityEngine;
using System;
using System.Collections;
using XInputDotNetPure;

public class InputManager : MonoBehaviour
{
	[SerializeField]
	private PlayerIndex playerIndex;
	private GamePadState state;
	private GamePadState prevState;

	private void Start()
	{
		state = GamePad.GetState(playerIndex);
	}

	private void Update()
	{
		prevState = state;
		state = GamePad.GetState(playerIndex);
	}

	public bool AcceptDown
	{
		get
		{
			return prevState.Buttons.A == ButtonState.Released && Accept;
		}
	}

	public bool Accept
	{
		get
		{
			return	state.Buttons.A == ButtonState.Pressed;
		}
	}

	public bool PlayDown
	{
		get
		{
			return prevState.Buttons.Start == ButtonState.Released && Play;
		}
	}
	
	public bool Play
	{
		get
		{
			return	state.Buttons.Start == ButtonState.Pressed;
		}
	}

	public bool Pause
	{
		get
		{
			return	state.Buttons.Back == ButtonState.Pressed;
		}
	}

	
	public bool Cancel
	{
		get
		{
			return	state.Buttons.B == ButtonState.Pressed;
		}
	}

	public bool IsActive
	{
		get
		{
			return	state.IsConnected;
		}
	}

	public float Horizontal
	{
		get
		{
			return Input.GetAxis("Horizontal") + state.ThumbSticks.Left.X;
		}
	}

	public float Vertical
	{
		get
		{
			return Input.GetAxis("Vertical") + state.ThumbSticks.Left.Y;
		}
	}

	public float Jump
	{
		get
		{
			return Convert.ToInt32( state.Buttons.A == ButtonState.Pressed);
		}
	}

	public bool Action
	{
		get
		{
			return Input.GetKey(KeyCode.E);
		}
	}

	public void StartVibration(Vector2 direction,float duration)
	{
		StartCoroutine(Vibration(direction,duration));
	}

	private IEnumerator Vibration(Vector2 direction,float duration)
	{
		GamePad.SetVibration(playerIndex,direction.x, direction.y);
		yield return new WaitForSeconds(duration);
		GamePad.SetVibration(playerIndex,0,0);
	}

	public void StopVibration()
	{
		StopCoroutine("Vibration");
	}
}
