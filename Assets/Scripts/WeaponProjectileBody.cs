using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectileBody : MonoBehaviour
{
    public GameObject projectile;
    public float damage = 0.0f;

    void OnTriggerEnter(Collider collider)
    {
        GameObject collideObj = collider.gameObject;
        Debug.Log("collided");
        if(collideObj.tag == "Monster") {
            //collided with a monster
            BasicMonster script = collideObj.GetComponent<BasicMonster>();
            script.setHitpoints(script.getHitpoints() - damage);
            Debug.Log("Monster hp: " + script.getHitpoints());
            projectile.GetComponent<WeaponProjectile>().destroy();
        }
    }
}
