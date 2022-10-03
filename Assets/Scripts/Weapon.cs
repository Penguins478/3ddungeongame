using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Camera mainCamera;
    protected float attackSpeed = 3.0f; //in seconds
    protected float attackRange = 1.0f;
    protected float time = 0.0f;
    public GameObject projectile;
    protected float projectileSpeed = 0.0f;
    protected float projectileDamage = 0.0f;

    protected void Start()
    {


    }

    // Update is called once per frame
    protected void Update()
    {
        time += Time.deltaTime;
        if(Input.GetMouseButtonDown(0)) {
            GameObject monster = null;
            RaycastHit hit;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //NEED TO CHECK IF COLLIDER IS A MONSTER
                monster = hit.collider.gameObject;
            }
            attack(monster);
        }
    }

    void attack(GameObject monster) {
        if(time >= attackSpeed) {
            time = 0.0f;
            shootProjectile(monster);
        }
    }
    protected virtual void shootProjectile(GameObject monster) {
        
    }

    public void setDamage(float damage) {
        this.projectileDamage = damage;
        if(projectile != null) projectile.GetComponent<WeaponProjectile>().setDamage(damage);
    }
}
