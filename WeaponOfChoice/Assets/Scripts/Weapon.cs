using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public PlayerController pc { set; protected get; }
	public float reloadTime = 0.8f;
	public int attackStrength = 10;
	public abstract void Attack();
}
public abstract class RangedWeapon : Weapon
{
	public GameObject Projectile;
	float lastTimeAttacked = int.MinValue;
	public override void Attack()
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
		}
	}
}
public abstract class MeleeWeapon : Weapon
{
}

