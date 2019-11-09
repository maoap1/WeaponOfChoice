using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour
{

	public Side AimingAt;
	public Player Shooter;
	public int Speed = 5;
	public int damage = 25;

	private bool dead = false;

	private void Start()
	{
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3((int)AimingAt, 0, 0);

		transform.position += movement * Speed * Time.fixedDeltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!dead)
		{
			if (other.gameObject.layer == 10 || other.gameObject.layer == 9)
			{
				if (!ReferenceEquals(Shooter, other.GetComponent<Player>()))
				{
					other.GetComponent<Player>().CurrHealth -= damage;
					Destroy(gameObject);
					dead = true;
				}
			}
			else
			{
				Destroy(gameObject);
				dead = true;
			}
		}
	}
}
