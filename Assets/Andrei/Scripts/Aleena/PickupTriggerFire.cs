using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTriggerFire : MonoBehaviour
{
    bool inTrigger = false;
    public GameObject prompt;

    void OnTriggerEnter(Collider col){
        Debug.Log("EnterFired Pickup, " + col.gameObject.name);
        prompt.SetActive(false);
    }
    void OnTriggerStay(Collider col){
        Debug.Log("PickupTriggerFires.OnTriggerStay, " + col.gameObject.name);
        if(col.gameObject.name == "Player1(Clone)" || col.gameObject.name == "Player2(Clone)"){
            inTrigger = true;
        }   
    }

    void Update(){
        if (Input.GetButtonDown("Fire1") && inTrigger) {
            this.SendMessageUpwards("PickUpObject", this.name);
        }
    }

    void OnTriggerExit(Collider col){
        Debug.Log("EnterFired Pickup, " + col.gameObject.name);
        inTrigger = false;
        prompt.SetActive(false);
    }
}
