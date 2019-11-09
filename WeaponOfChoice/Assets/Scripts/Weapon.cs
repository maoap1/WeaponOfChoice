using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public Player pc { set; protected get; }
	public float reloadTime = 0.8f;
	public int attackStrength = 10;
	protected abstract bool Melee { get; }

	public GameObject Projectile;
	public float ProjectileDiesAfter;
	public int projectileSpeed = 5;
	float lastTimeAttacked = int.MinValue;
	public void Attack()
	{
		if (lastTimeAttacked + reloadTime < Time.timeSinceLevelLoad)
		{
			lastTimeAttacked = Time.timeSinceLevelLoad;
			ProjectileBehaviour projectile =
				Instantiate(Projectile,
				GetComponent<Transform>().position,
				GetComponent<Transform>().rotation)
				.GetComponent<ProjectileBehaviour>();
			projectile.Shooter = pc;
			projectile.AimingAt = pc.LookingAt;
			projectile.Speed = projectileSpeed;
			projectile.damage = attackStrength;
			if(Melee)
				projectile.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
		}
	}
}

