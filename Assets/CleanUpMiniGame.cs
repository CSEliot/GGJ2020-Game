using UnityEngine;
using Photon.Pun;

public class CleanUpMiniGame : MonoBehaviour
{
    public int player;

    public int objectives = 4;
    public int currentObjectivesCompleted = 0;

    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    public GameManager gameManager;
    bool finishedAlready = false;

    public GameObject player1;
    public GameObject player2;

    void Update()
    {
        if(isPlayer1){ 
            player1 = GameObject.Find("Player1(Clone)");
            player1.GetComponent<PlayerMovement>().SwitchToMop();
        }else if(isPlayer2){ 
            player2 = GameObject.Find("Player2(Clone)");
            player2.GetComponent<PlayerMovement>().SwitchToMop();
        }

        if(currentObjectivesCompleted == objectives && !finishedAlready){
            finishedAlready = true;
            if(isPlayer1){ 
                player1 = GameObject.Find("Player1(Clone)");
                player1.GetComponent<PlayerMovement>().SwitchBackFromMop();
            }
            if(isPlayer2){ 
                player2 = GameObject.Find("Player2(Clone)");
                player2.GetComponent<PlayerMovement>().SwitchBackFromMop();
            }
            Debug.Log("Calling RPC!");
            gameManager.GetComponent<PhotonView>().RPC("PlayerMoveToNextFloor", RpcTarget.All,  player);
        }
    }
}
