using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateMonster : MonoBehaviour
{

	public GameObject player;
	public GameObject slimePrefab;
    public GameObject skeletonPrefab;
    public GameObject golemPrefab;
    public GameObject batPrefab;
    public GameObject spiritPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // slime
        GameObject monster = Instantiate(slimePrefab, new Vector3(3, 1, -10), Quaternion.identity) as GameObject;
        monster.GetComponent<Slime>().player = this.player;

        // skeleton
        GameObject monster2 = Instantiate(skeletonPrefab, new Vector3(5, 1, -15), Quaternion.identity) as GameObject;
        monster2.GetComponent<Skeleton>().player = this.player;

        // golem
        GameObject monster3 = Instantiate(golemPrefab, new Vector3(10, 1, -15), Quaternion.identity) as GameObject;
        monster3.GetComponent<Golem>().player = this.player;

        // bat
        GameObject monster4 = Instantiate(batPrefab, new Vector3(7, 2, -15), Quaternion.identity) as GameObject;
        monster4.GetComponent<Bat>().player = this.player;

        // spirit
        GameObject monster5 = Instantiate(spiritPrefab, new Vector3(8, 1, -10), Quaternion.identity) as GameObject;
        monster5.GetComponent<Spirit>().player = this.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
