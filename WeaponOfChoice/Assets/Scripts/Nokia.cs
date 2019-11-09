using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Nokia : Weapon
{
	protected override bool Melee => true;
	public override int attackStrength => 49;
	public override float ProjectileDiesAfter => 1.80347f;
	public override int projectileSpeed => 7;
	public override float ProjectileGravityScale => -0.14f;
	public override float reloadTime => 2.5f;
}
