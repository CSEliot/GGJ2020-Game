using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVictory : MonoBehaviour
{
    [SerializeField]
    private int player;

    [SerializeField]
        private GameObject ObjectPickup;
    
    [SerializeField]
        private GameObject ObjectPlayer;
    
    [SerializeField]
        private GameObject ObjectPlace;
    
    public GameManager gameManager;
    public bool finishedAlready = false;

    void Awake(){
        finishedAlready = false;
        ObjectPlayer = gameManager.player1.GetComponent<PlayerObjectList>().pipePickup.gameObject;
    }

    void Start()
    {
        ObjectPickup.SetActive(true);
        ObjectPlayer.SetActive(false);
        ObjectPlace.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    void Update(){
        if(ObjectPlace.GetComponentInChildren<MeshRenderer>().enabled && !finishedAlready){
            finishedAlready = true;
            gameManager.PlayerMoveToNextFloor(player);
        }
    }
}
