using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : WeaponProjectile
{
    public float explosionRadius = 2.0f;
    public override void destroy() {
        Debug.Log("dead rocket");
        applyDamage(null);
        Destroy(this.gameObject);
    }
    public override void applyDamage(GameObject monster)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.gameObject.transform.position, explosionRadius);
        //this.gameObject.transform.position
        Debug.Log(hitColliders.Length);
        foreach(Collider c in hitColliders) {
            Debug.Log(c);
            if(c.tag == "Monster") {
                BasicMonster script = c.GetComponent<BasicMonster>();
                script.setHitpoints(script.getHitpoints() - damage);
                Debug.Log("Monster hp: " + script.getHitpoints());
            }
        }
    }
}
