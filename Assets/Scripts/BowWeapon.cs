using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeapon : Weapon
{
    void Start() {
        attackRange = 20.0f;
        projectileSpeed = 50.0f;
        projectileDamage = 4.0f;
    }

    protected override void shootProjectile(GameObject monster) {
        GameObject arrow = Instantiate(projectile, new Vector3(player.position.x , player.position.y, player.position.z), Quaternion.identity) as GameObject;
        arrow.GetComponent<WeaponProjectile>().setRotation(new Vector3(0, player.eulerAngles.y, 0));
        arrow.GetComponent<WeaponProjectile>().speed = projectileSpeed;
        arrow.GetComponent<WeaponProjectile>().range = attackRange;
        arrow.GetComponent<WeaponProjectile>().setDamage(projectileDamage);
        arrow.GetComponent<WeaponProjectile>().launchAngle = 360 - mainCamera.transform.eulerAngles.x;
        Debug.Log("angleeee: " + (360-mainCamera.transform.eulerAngles.x));
    }
}
