using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickMovement : MonoBehaviour
{

    public Transform player;
    public Vector3 playerOffset;
    public Vector3 weaponHoldPosition;
    private Animator animator;
    public GameObject pivot;
    public float forwardTilt;
    private float angleRad;

    // Start is called before the first frame update
    void Start()
    {
        animator = pivot.GetComponent<Animator> ();
    }

// Update is called once per frame
void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.enabled = true;
            animator.Play("StickBash");
        }else if(Input.GetMouseButtonUp(0))
        {
            animator.enabled = false;
        }
        angleRad = Mathf.Deg2Rad * (90 - player.transform.eulerAngles.y);
        transform.eulerAngles = new Vector3(forwardTilt, player.transform.eulerAngles.y, 0.0f);
        transform.position = player.transform.position + new Vector3((playerOffset.x) * Mathf.Cos(angleRad) - playerOffset.z * Mathf.Sin(angleRad), playerOffset.y, (playerOffset.x) * Mathf.Sin(angleRad) + playerOffset.z * Mathf.Cos(angleRad)) + weaponHoldPosition;

    }
}
