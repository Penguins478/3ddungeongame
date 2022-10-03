using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BasicMonster
{
    
	private float rotateDeg = 3f;

    public float moveSpeed;
	public float attackSpeed;
	public float hitpoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        shoot();
        onDeath();
    }

    public void move(){
    	float angle = Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x);
        
    	float dist = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(transform.position.z - player.transform.position.z), 2)
        	 + 
        	Mathf.Pow(Mathf.Abs(transform.position.x - player.transform.position.x), 2));

    	//Debug.Log(dist);

        if(dist > 10){

	        transform.localPosition = new Vector3(transform.position.x + (moveSpeed * Mathf.Cos(angle)), 
	                                            transform.position.y,
	                                            transform.position.z + (moveSpeed * Mathf.Sin(angle)));

	    }
	    
	    float rotateBody = rotateDeg;
        if(Mathf.Abs(transform.rotation.y - angle * Mathf.Deg2Rad) < rotateDeg) {
          rotateBody = Mathf.Abs(transform.rotation.y - angle * Mathf.Deg2Rad);
        }

        if(transform.rotation.y > angle * Mathf.Deg2Rad) rotateBody *= -1f;
        transform.Rotate(transform.rotation.x, rotateBody, transform.rotation.z);
        
        
    }

    public void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("Hit player");

            GameObject p = col.gameObject;

            p.GetComponent<PlayerStats>().setHitpoints(p.GetComponent<PlayerStats>().getHitpoints() - 1);

            Debug.Log(p.GetComponent<PlayerStats>().getHitpoints());
        }
        //Debug.Log("collided with something");
    }


    public void onDeath(){
        if(hitpoints <= 0){
            Destroy(this.gameObject);
        }
    }

    public void shoot(){
    	// GameObject arrow = Instantiate(projectile, new Vector3(transform.position.x , transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
     //    arrow.GetComponent<WeaponProjectile>().setRotation(new Vector3(0, this.gameObject.eulerAngles.y, 0));
     //    arrow.GetComponent<WeaponProjectile>().speed = 0.1;
     //    arrow.GetComponent<WeaponProjectile>().range = 10;
     //    arrow.GetComponent<WeaponProjectile>().setDamage(10);
     //    arrow.GetComponent<WeaponProjectile>().launchAngle = 360 - mainCamera.transform.eulerAngles.x;
    }

    public float getMoveSpeed(){  
        return moveSpeed;
    }

    public void setMoveSpeed(float speed){
        moveSpeed = speed;
    }
    
    public float getAttackSpeed(){ 
        return attackSpeed;
    }

    public void setAttackSpeed(float speed){
        attackSpeed = speed;
    }

    public float getHitpoints(){
        return hitpoints;
    }

    public void setHitpoints(float hp){
        hitpoints = hp;
    }
}
