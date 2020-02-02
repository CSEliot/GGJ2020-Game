using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSuccess : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectPickup;
    
    [SerializeField]
    private GameObject ObjectPlayer;

    public GameManager gameManager;

    void Awake(){
        ObjectPlayer = gameManager.player1.GetComponent<PlayerObjectList>().pipePickup.gameObject;
    }


    public void PickUpObject(string objectPickedUp) {
        ObjectPickup.SetActive(false);
        ObjectPlayer.SetActive(true);
    }

}

