using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherWeapon : Weapon
{
   void Start() {
        attackRange = 15.0f;
        projectileSpeed = 30.0f;
        projectileDamage = 2.0f;
    }

    protected override void shootProjectile(GameObject monster) {
        GameObject rocket = Instantiate(projectile, new Vector3(player.position.x , player.position.y, player.position.z), Quaternion.identity) as GameObject;
        rocket.GetComponent<WeaponProjectile>().setRotation(new Vector3(0, player.eulerAngles.y, 0));
        rocket.GetComponent<WeaponProjectile>().speed = projectileSpeed;
        rocket.GetComponent<WeaponProjectile>().range = attackRange;
        rocket.GetComponent<WeaponProjectile>().setDamage(projectileDamage);
        rocket.GetComponent<WeaponProjectile>().launchAngle = 360 - mainCamera.transform.eulerAngles.x;
    }
}
