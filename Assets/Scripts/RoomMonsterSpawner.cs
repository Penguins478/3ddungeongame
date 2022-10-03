using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMonsterSpawner : MonoBehaviour
{
    public List<GameObject> roomTypes;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player") {
            int roomNum = Random.Range(0, roomTypes.Count - 1);          
            GameObject monsters = Instantiate(roomTypes[roomNum], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity) as GameObject;
            for(int i = 0; i < monsters.transform.childCount; i++) {
                // monsters.transform.GetChild(i).GetComponent<BasicMonster>().player = player;
            }

        }
    }
}
