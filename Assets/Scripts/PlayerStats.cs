using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hitpoints = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getHitpoints(){
    	return hitpoints;
    }

    public void setHitpoints(float hp){
    	hitpoints = hp;
    }
}
