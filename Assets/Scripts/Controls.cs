using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Controls : MonoBehaviour
{
	public enum Action
	{
		UP,
		LEFT,
		RIGHT,
		DOWN,
		UP_P,
		LEFT_P,
		RIGHT_P,
		DOWN_P,
		UP_R,
		LEFT_R,
		RIGHT_R,
		DOWN_R,
		ACCEPT,
		BACK,
		PAUSE,
		RESET,
		CHEAT
	}

	public enum Control
	{
		UP,
		LEFT,
		RIGHT,
		DOWN,
		RESET,
		ACCEPT,
		BACK,
		PAUSE,
		CHEAT
	}

	public enum KeyboardScheme
	{
		Solo,
		Duo,
		None,
		Custom
	}

	private Player player;
	private Dictionary<Action, bool> actionStates = new Dictionary<Action, bool>();
	private Dictionary<Action, bool> previousActionStates = new Dictionary<Action, bool>();
	private KeyboardScheme currentScheme = KeyboardScheme.None;

	private void Awake()
	{
		player = ReInput.players.GetPlayer(0);
		InitializeActionStates();
	}

	private void InitializeActionStates()
	{
		foreach (Action action in System.Enum.GetValues(typeof(Action)))
		{
			actionStates[action] = false;
			previousActionStates[action] = false;
		}
	}

	private void Update()
	{
		UpdateActionStates();
	}

	private void UpdateActionStates()
	{
		foreach (var kvp in actionStates)
		{
			previousActionStates[kvp.Key] = kvp.Value;
		}

		actionStates[Action.UP] = GetButton(Control.UP);
		actionStates[Action.LEFT] = GetButton(Control.LEFT);
		actionStates[Action.RIGHT] = GetButton(Control.RIGHT);
		actionStates[Action.DOWN] = GetButton(Control.DOWN);

		actionStates[Action.UP_P] = GetButtonDown(Control.UP);
		actionStates[Action.LEFT_P] = GetButtonDown(Control.LEFT);
		actionStates[Action.RIGHT_P] = GetButtonDown(Control.RIGHT);
		actionStates[Action.DOWN_P] = GetButtonDown(Control.DOWN);

		actionStates[Action.UP_R] = GetButtonUp(Control.UP);
		actionStates[Action.LEFT_R] = GetButtonUp(Control.LEFT);
		actionStates[Action.RIGHT_R] = GetButtonUp(Control.RIGHT);
		actionStates[Action.DOWN_R] = GetButtonUp(Control.DOWN);

		actionStates[Action.ACCEPT] = GetButtonDown(Control.ACCEPT);
		actionStates[Action.BACK] = GetButtonDown(Control.BACK);
		actionStates[Action.PAUSE] = GetButtonDown(Control.PAUSE);
		actionStates[Action.RESET] = GetButtonDown(Control.RESET);
		actionStates[Action.CHEAT] = GetButtonDown(Control.CHEAT);

		foreach (var action in actionStates)
		{
			if (action.Value)
			{
				/*Debug.Log($"Action State: {action.Key} is TRUE");*/
			}
		}
	}

	public bool GetButton(Control control)
	{
		switch (control)
		{
			case Control.UP:
				return player.GetButton("UP");
			case Control.LEFT:
				return player.GetButton("LEFT");
			case Control.RIGHT:
				return player.GetButton("RIGHT");
			case Control.DOWN:
				return player.GetButton("DOWN");
			case Control.ACCEPT:
				return player.GetButton("ACCEPT");
			case Control.BACK:
				return player.GetButton("BACK");
			case Control.PAUSE:
				return player.GetButton("PAUSE");
			case Control.RESET:
				return player.GetButton("RESET");
			case Control.CHEAT:
				return player.GetButton("CHEAT");
			default:
				return false;
		}
	}

	public bool GetButtonDown(Control control)
	{
		switch (control)
		{
			case Control.UP:
				return player.GetButtonDown("UP");
			case Control.LEFT:
				return player.GetButtonDown("LEFT");
			case Control.RIGHT:
				return player.GetButtonDown("RIGHT");
			case Control.DOWN:
				return player.GetButtonDown("DOWN");
			case Control.ACCEPT:
				return player.GetButtonDown("ACCEPT");
			case Control.BACK:
				return player.GetButtonDown("BACK");
			case Control.PAUSE:
				return player.GetButtonDown("PAUSE");
			case Control.RESET:
				return player.GetButtonDown("RESET");
			case Control.CHEAT:
				return player.GetButtonDown("CHEAT");
			default:
				return false;
		}
	}

	public bool GetButtonUp(Control control)
	{
		switch (control)
		{
			case Control.UP:
				return player.GetButtonUp("UP");
			case Control.LEFT:
				return player.GetButtonUp("LEFT");
			case Control.RIGHT:
				return player.GetButtonUp("RIGHT");
			case Control.DOWN:
				return player.GetButtonUp("DOWN");
			case Control.ACCEPT:
				return player.GetButtonUp("ACCEPT");
			case Control.BACK:
				return player.GetButtonUp("BACK");
			case Control.PAUSE:
				return player.GetButtonUp("PAUSE");
			case Control.RESET:
				return player.GetButtonUp("RESET");
			case Control.CHEAT:
				return player.GetButtonUp("CHEAT");
			default:
				return false;
		}
	}

	public bool CheckAction(Action action)
	{
		return actionStates.ContainsKey(action) && actionStates[action];
	}

	public bool CheckActionPressed(Action action)
	{
		return actionStates.ContainsKey(action) && actionStates[action] && !previousActionStates[action];
	}

	public bool CheckActionReleased(Action action)
	{
		return previousActionStates.ContainsKey(action) && previousActionStates[action] && !actionStates[action];
	}

	public void SetKeyboardScheme(KeyboardScheme scheme)
	{
		currentScheme = scheme;
		// todo: actual scheme shit, despite it being unused in base fnf iirc.
	}
}
