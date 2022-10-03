using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateStartingWeapon : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public GameObject startingWeaponPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject weapon = Instantiate(startingWeaponPrefab, new Vector3(player.position.x , player.position.y, player.position.z + 1), Quaternion.identity) as GameObject;
        weapon.GetComponent<MagicStaffWeapon>().player = this.player;
        weapon.GetComponent<MagicStaffWeapon>().mainCamera = this.mainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
