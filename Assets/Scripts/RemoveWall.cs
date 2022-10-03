using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWall : MonoBehaviour
{
    public List<GameObject> removeobjects;

    //destroy the wall when colliding with these objects
    private string[] collidedObjectNames = {"HallwayGround", "Ground"};
    public void Start()
    {
       
    }

    public void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        foreach(string name in collidedObjectNames) {
            if(collider.gameObject.name.Equals(name)) {
                foreach(GameObject item in removeobjects) {
                    Destroy(item);
                }
            }
        }
    }
}
