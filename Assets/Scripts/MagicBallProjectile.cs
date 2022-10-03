using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallProjectile : WeaponProjectile
{
    public bool canFreeze = false;
    public float freezeLength = 0.0f;

    public override void destroy()
    {
        base.destroy();
    }
    public override void applyDamage(GameObject monster)
    {
        base.applyDamage(monster);
        if(canFreeze) {
            //freeze monster
        }
    }
    
}
