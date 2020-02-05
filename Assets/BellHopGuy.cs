using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BellHopGuy : MonoBehaviour
{
    public int player;
    public GameManager gameManager;
    public Animator anim;
    public GameObject deadGuy;
    public GameObject body;
    public GameObject check;

    bool hasFinished = false;

    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    bool _movingToNextLevel = false;

    void Awake(){
        hasFinished = false;
        StartCoroutine(Kill());
    }

    void OnTriggerEnter(Collider collision){
        if(collision.tag == "BellHop"){
            hasFinished = true;
            deadGuy.SetActive(true);
            check.SetActive(true);
            body.SetActive(false);
            anim.enabled = false;
            if(isPlayer1){ 
                Debug.Log("calling");
                gameManager.dude.SetTrigger("Celebrate");
            }else if(isPlayer2){ 
                gameManager.dude2.SetTrigger("Celebrate");
            }
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(1);

        if(_movingToNextLevel) {
            
        } else {
            _movingToNextLevel = true;
            Debug.Log("Delay() called bad!");
            gameManager.GetComponent<PhotonView>().RPC("PlayerMoveToNextFloor", RpcTarget.All, player);
        }
    }

    IEnumerator Kill(){
        yield return new WaitForSeconds(8);
        if(!hasFinished){
            if (_movingToNextLevel) {

            } else {
                _movingToNextLevel = true;
                Debug.Log("Kill() called bad!");
                //gameManager.PlayerMoveToNextFloor(player);
                gameManager.GetComponent<PhotonView>().RPC("PlayerMoveToNextFloor", RpcTarget.All, player);
            }
        }
    }
}
