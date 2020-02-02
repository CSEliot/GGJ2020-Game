using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectSucess : MonoBehaviour
{
    [SerializeField]
        private GameObject ObjectPickup;
    [SerializeField]
        private GameObject ObjectPlayer;
    [SerializeField]
        private GameObject ObjectPlace;

        public GameManager gameManager;

        public GameObject flashingIcon1;
        public GameObject successFlash;

    bool pickIt = false;
    bool place = false;

    void Awake(){
        ObjectPlayer = gameManager.player1.GetComponent<PlayerObjectList>().pipePickup.gameObject;
    }


    public void PlaceObjectArea(string objName){
        place = true;
        suceed();
    }

    public void PickUpObject(string objectPickedUp) {
        ObjectPickup.SetActive(false);
        ObjectPlayer.SetActive(true);
        pickIt = true;
        suceed();
    }

    void suceed (){
        if(pickIt && place){    
            ObjectPlace.GetComponentInChildren<MeshRenderer>().enabled = true;
            ObjectPlayer.SetActive(false);
            flashingIcon1.SetActive(false);
            successFlash.SetActive(true);
        }
    }

}
