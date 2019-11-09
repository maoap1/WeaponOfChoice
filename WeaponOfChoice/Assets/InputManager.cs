using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public string HorizontalAxisName;
	public string JumpAxisName;
	public string FireButtonName;

	// Update is called once per frame
	void FixedUpdate()
	{
		CurrInput = new InputResults(
			horizontal: Input.GetAxis(HorizontalAxisName),
			jumpAxis: Input.GetAxis(JumpAxisName),
			fired: Input.GetButtonDown(FireButtonName)
			);
	}
	public InputResults CurrInput { get; private set; }
}
public struct InputResults
{
	public InputResults(float horizontal, float jumpAxis, bool fired)
	{
		Horizontal = horizontal;
		JumpAxis = jumpAxis;
		Fired = fired;
	}
	public float JumpAxis { get; }
	public bool Jumped => JumpAxis > 0;
	public float Horizontal { get; }
	public bool Fired { get; }
}
