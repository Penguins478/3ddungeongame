using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    public float speed;
    public float range;
    public float damage;
    public float launchAngle;
    public GameObject projectile;
    private float yAngle = 0.0f;
    private Vector3 startPos;
    public GameObject weaponProjectileBody;
    private bool hasLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = projectile.transform.position;
        // projectile.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, 0), ForceMode.VelocityChange);
        // projectile.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // projectile.transform.position += new Vector3(Mathf.Cos(yAngle) * speed * Time.deltaTime, 0, Mathf.Sin(yAngle) * speed * Time.deltaTime);
        float dist = Mathf.Sqrt(Mathf.Pow(projectile.transform.position.x - startPos.x, 2) + Mathf.Pow(projectile.transform.position.z - startPos.z, 2));
        if(!hasLaunched) {
            Launch();
            hasLaunched = true;
        }
        if(hasLaunched) {
            Vector3 currentVelocity = projectile.GetComponent<Rigidbody>().velocity;
            projectile.transform.eulerAngles = new Vector3(Mathf.Atan2(currentVelocity.y, currentVelocity.z), projectile.transform.eulerAngles.y, projectile.transform.eulerAngles.z);
        }
        if(dist >= range) {
            destroy();
        }
    }

    public void setRotation(Vector3 angle) {
        projectile.transform.eulerAngles = angle;
        yAngle = (90 - angle.y) * Mathf.Deg2Rad;
    }

    public virtual void destroy() {
        Destroy(this.gameObject);
    }

    public virtual void applyDamage(GameObject monster) {
        BasicMonster script = monster.GetComponent<BasicMonster>();
        script.setHitpoints(script.getHitpoints() - damage);
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject collideObj = collider.gameObject;

        if(collideObj.tag == "Monster" || collideObj.tag == "Ground") {
            //collided with a monster
            applyDamage(collideObj);
            destroy();
        }
    }

    public void setDamage(float damage) {
        this.damage = damage;
        weaponProjectileBody.GetComponent<WeaponProjectileBody>().damage = this.damage;
    }

    private void Launch() {
        float gravity = -Physics.gravity.y;
        float tanAngle = Mathf.Tan(launchAngle * Mathf.Deg2Rad);

        if(tanAngle == 0.0f) tanAngle = 0.001f;
        // float Vz = Mathf.Sqrt(gravity * range / (2.0f * Mathf.Abs(tanAngle)));
        // float Vy = tanAngle * Vz;
        // Vector3 localVelocity = new Vector3(0f, Vy, Vz);
        Vector3 localVelocity = new Vector3(0f, speed * Mathf.Sin(launchAngle * Mathf.Deg2Rad), speed * Mathf.Cos(launchAngle * Mathf.Deg2Rad));
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);
        projectile.GetComponent<Rigidbody>().velocity = globalVelocity;
    }
}
