using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Nokia : Weapon
{
    protected override bool Melee => true;
    public Nokia()
    {
        attackStrength = 49;
        ProjectileDiesAfter = 1.80347f;
        projectileSpeed = 7;
        ProjectileGravityScale = -0.14f;
        reloadTime = 2.5f;
    }
}
