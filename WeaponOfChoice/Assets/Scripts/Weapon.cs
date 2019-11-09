using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public Player pc { set; protected get; }
	public virtual float reloadTime => 0.8f;
	public virtual int attackStrength => 10;
	protected abstract bool Melee { get; }

	public GameObject Projectile;
	public virtual float ProjectileDiesAfter => 500_000;
	public virtual int projectileSpeed => 5;
	public virtual float ProjectileGravityScale => 0.15f;
	float lastTimeAttacked = int.MinValue;
	public bool Attack()
	{
		if (lastTimeAttacked + reloadTime < Time.timeSinceLevelLoad)
		{
			lastTimeAttacked = Time.timeSinceLevelLoad;
			ProjectileBehaviour projectile;
			if (Melee)
			{
				projectile =
				Instantiate(Projectile,
				GetComponent<Transform>().position,
				GetComponent<Transform>().rotation,
				pc.transform)
				.GetComponent<ProjectileBehaviour>();
				//projectile.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
				projectile.dieAtDistance = ProjectileDiesAfter;
			}
			else
			{
				projectile =
				Instantiate(Projectile,
				GetComponent<Transform>().position,
				GetComponent<Transform>().rotation)
				.GetComponent<ProjectileBehaviour>();
				projectile.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
			}
			projectile.GravityScale = ProjectileGravityScale;
			projectile.Shooter = pc;
			projectile.AimingAt = pc.LookingAt;
			projectile.Speed = projectileSpeed;
			projectile.damage = attackStrength;
			return true;
		}
		return false;
	}
}


