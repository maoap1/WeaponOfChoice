﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public string horizontalAxisName;
	public KeyCode jumpName;
	public string fireButtonName;

	// Update is called once per frame
	void FixedUpdate()
	{
        CurrInput = new InputResults(
            horizontal: Input.GetAxis(horizontalAxisName),
            startJump: Input.GetKeyDown(jumpName),
            endJump: Input.GetKeyUp(jumpName),
            fired: Input.GetButtonDown(fireButtonName)
            ); ;
	}
	public InputResults CurrInput { get; private set; }
}
public struct InputResults
{
	public InputResults(float horizontal, bool startJump, bool endJump, bool fired)
	{
		Horizontal = horizontal;
		StartJumping = startJump;
		EndJumping = endJump;
        Fired = fired;
	}
	public bool EndJumping { get; }
	public bool StartJumping { get; }
    public float Horizontal { get; }
	public bool Fired { get; }
}
