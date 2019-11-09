using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour
{

	public Side AimingAt;
	public Player Shooter;
	public int Speed { get; set; } = 5;
	public int damage { get; set; } = 25;
	public float dieAtDistance;

	public float GravityScale
	{
		get => GetComponent<Rigidbody2D>().gravityScale;
		set => GetComponent<Rigidbody2D>().gravityScale = value;
	}

	private bool dead = false;

	private void Start()
	{
	}

	void FixedUpdate()
	{
		Vector3 movement = new Vector3((int)AimingAt, 0, 0);

		transform.position += movement * Speed * Time.fixedDeltaTime;

		if((Shooter.transform.position - transform.position).magnitude >= dieAtDistance)
		{
			dead = true;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!dead)
		{
			Debug.Log((Shooter.transform.position - transform.position).magnitude);
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
