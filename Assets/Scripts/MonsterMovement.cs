using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public Transform player;

    // at 0 so it isn't annoying
    public float speed = 0f;
    private float rotateDeg = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Monster that follows Player.");
    }

    // Update is called once per frame
    void Update()
    {
      float angle = Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x);
      transform.localPosition = new Vector3(transform.position.x + (speed * Mathf.Cos(angle)), 
                                            transform.position.y,
                                            transform.position.z + (speed * Mathf.Sin(angle)));
      float rotateBody = rotateDeg;
      if(Mathf.Abs(transform.rotation.y - angle * Mathf.Deg2Rad) < rotateDeg) {
        rotateBody = Mathf.Abs(transform.rotation.y - angle * Mathf.Deg2Rad);
      }
      if(transform.rotation.y > angle * Mathf.Deg2Rad) rotateBody *= -1f;

      transform.Rotate(transform.rotation.x, rotateBody, transform.rotation.z);
       
    }
}
