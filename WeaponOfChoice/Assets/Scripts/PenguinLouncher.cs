using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinLouncher : Weapon
{
	protected override bool Melee => false;
	public override float ProjectileAlpha => 1;
}
