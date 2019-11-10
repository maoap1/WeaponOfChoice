using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Toaster : Weapon
{
	protected override bool Melee => false;
	public override Vector3 ProjectileStartingAngle => new Vector3(5, 1, 0);
	public override int projectileSpeed => 17;
	public override float ProjectileGravityScale => 1f;
	public override Vector3 MakeAtLocalPosition => new Vector3(1.771221f, 1.526695f, 0);
	public override float reloadTime => 0.45f;
}
