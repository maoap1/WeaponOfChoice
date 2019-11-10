using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Toaster : Weapon
{
	protected override bool Melee => false;
	
    public void Start()
    {
        ProjectileStartingAngle = new Vector3(5, 1, 0);
        projectileSpeed = 17;
        ProjectileGravityScale = 1f;
        MakeAtLocalPosition = new Vector3(1.771221f, 1.526695f, 0);
        reloadTime = 0.45f;
    }
    
}
