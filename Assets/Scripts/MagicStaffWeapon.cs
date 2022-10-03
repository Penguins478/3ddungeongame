using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStaffWeapon : Weapon
{
    public float freezeFreq = 7.0f; //able to freeze a target every number of seconds
    public float freezeLength = 5.0f; //in seconds
    private float freezeTime = 0.0f;

    void Start() {
        attackRange = 20.0f;
        projectileSpeed = 50.0f;
        projectileDamage = 4.0f;
    }
    
    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        freezeTime += Time.deltaTime;
    }

    protected override void shootProjectile(GameObject monster) {
        GameObject magicBall = Instantiate(projectile, new Vector3(player.position.x , player.position.y, player.position.z), Quaternion.identity) as GameObject;
        magicBall.GetComponent<WeaponProjectile>().setRotation(new Vector3(0, player.eulerAngles.y, 0));
        magicBall.GetComponent<WeaponProjectile>().speed = projectileSpeed;
        magicBall.GetComponent<WeaponProjectile>().range = attackRange;
        magicBall.GetComponent<WeaponProjectile>().setDamage(projectileDamage);
        magicBall.GetComponent<WeaponProjectile>().launchAngle = 360 - mainCamera.transform.eulerAngles.x;
        magicBall.GetComponent<MagicBallProjectile>().freezeLength = freezeLength;
        if(freezeTime >= freezeFreq) {
            freezeTime = 0.0f;
            magicBall.GetComponent<MagicBallProjectile>().canFreeze = true;
        }
    }
}
