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
	public override void Attack()
	{
		throw new System.NotImplementedException();
	}
}
public abstract class MeleeWeapon : Weapon
{
}
public sealed class Fork : MeleeWeapon
{
	public override void Attack()
	{
		throw new System.NotImplementedException();
	}
}
public sealed class Toaster : RangedWeapon
{
}

