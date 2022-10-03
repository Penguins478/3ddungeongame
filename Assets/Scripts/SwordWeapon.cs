using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon
{
    void Start() {
        projectileDamage = 3.0f;
    }

    protected override void shootProjectile(GameObject monster) {
        if(monster != null) {
            //decrease monster hp and/or apply weapon effect if it is in distance to the weapon
        }
    }
}

