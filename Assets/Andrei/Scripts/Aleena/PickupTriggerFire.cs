using UnityEngine;

public class PickupTriggerFire : MonoBehaviour
{
    bool inTrigger = false;
    public GameObject prompt;

    void Start(){
        prompt.SetActive(false);
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.name == "Player1(Clone)" || col.gameObject.name == "Player2(Clone)"){
            prompt.SetActive(true);
            inTrigger = true;
        }
    }

    void OnTriggerStay(Collider col){
        if(col.gameObject.name == "Player1(Clone)" || col.gameObject.name == "Player2(Clone)"){
            inTrigger = true;
        }   
    }

    void Update(){
        if ((Input.GetButtonDown("Fire1")) && inTrigger) {
            this.SendMessageUpwards("PickUpObject", this.name);
            InputNet.Player1Fired = false;
        }
    }

    void OnTriggerExit(Collider col){
        prompt.SetActive(false);
        inTrigger = false;
    }
}
