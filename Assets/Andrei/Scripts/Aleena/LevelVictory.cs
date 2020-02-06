using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
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

    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    void Awake(){
        finishedAlready = false;
       if(isPlayer1) ObjectPlayer = gameManager.player1.GetComponent<PlayerObjectList>().pipePickup.gameObject;
       if(isPlayer2) ObjectPlayer = gameManager.player2.GetComponent<PlayerObjectList>().pipePickup.gameObject;
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
            CBUG.Do("Level Victory! From: " + player);
            gameManager.GetComponent<PhotonView>().RPC("PlayerMoveToNextFloor", RpcTarget.All,  player);
            //gameManager.PlayerMoveToNextFloor(player);
        }
    }
}
