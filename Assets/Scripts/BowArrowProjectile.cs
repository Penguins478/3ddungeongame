using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrowProjectile : WeaponProjectile
{
    public override void destroy()
    {
        base.destroy();
    }

    public override void applyDamage(GameObject monster)
    {
        base.applyDamage(monster);
    }
}
