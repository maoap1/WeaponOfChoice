using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : Weapon
{
	protected override bool Melee => true;
	public override float ProjectileAlpha => 1;
}
