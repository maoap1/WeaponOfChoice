using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour
{

	public Side AimingAt;
	public Player Shooter;
	public int Speed = 5;

    // Update is called once per frame
    void FixedUpdate()
	{
		Vector3 movement = new Vector3((int)AimingAt, 0, 0);

		transform.position += movement * Speed * Time.fixedDeltaTime;
	}
}
