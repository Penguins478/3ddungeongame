using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInterfaces : MonoBehaviour
{
    public GameObject popUpBox;
    //public Animator animator;
    public TMP_Text popUpText;

    public void PopUp(string text){
        popUpBox.SetActive(false);
        popUpText.text = text;
        //animator.SetTrigger("pop");
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("x")){
            popUpBox.SetActive(true);
        }else{
            popUpBox.SetActive(false);
        }
    }
}
 