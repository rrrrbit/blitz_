using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Control Scheme", menuName = "Tokens/Control Scheme", order = 1)]
public class TOKEN_ControlScheme : ScriptableObject
{
    public enum ControlScheme
	{
		Keyboard,
		Arcade
	}
	public ControlScheme controlScheme;

	public InputControlScheme GetScheme(InputSystem_Actions actions)
	{
		switch (controlScheme)
		{
			case ControlScheme.Keyboard:
				return actions.KeyboardMouseScheme;
			case ControlScheme.Arcade:
				return actions.JoystickScheme;
			default:
				return actions.KeyboardMouseScheme;
		}
	}
}
