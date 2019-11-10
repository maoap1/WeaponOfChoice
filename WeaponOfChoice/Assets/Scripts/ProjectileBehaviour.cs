using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProjectileBehaviour : MonoBehaviour
{

	public Side AimingAt;
	public Player Shooter;
	public int Speed { get; set; }
	public int damage { get; set; }
	public float dieAtDistance { get; set; }
	Vector3 _startingAngle;
	public Vector3 StartingAngle
	{
		private get => _startingAngle;
		set => _startingAngle = value.normalized;
	}

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
		Vector3 movement = new Vector3((int)AimingAt * StartingAngle.x, StartingAngle.y, 0);

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
			//Debug.Log((Shooter.transform.position - transform.position).magnitude);
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
