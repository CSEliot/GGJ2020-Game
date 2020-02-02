using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellHopGuy : MonoBehaviour
{
    public int player;
    public GameManager gameManager;
    public Animator anim;
    public GameObject deadGuy;
    public GameObject body;

    bool hasFinished = false;

    void Awake(){
        hasFinished = false;
    }

    void OnTriggerEnter(Collider collision){
        if(collision.tag == "BellHop"){
            hasFinished = true;
            deadGuy.SetActive(true);
            body.SetActive(false);
            anim.enabled = false;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(1);
        gameManager.PlayerMoveToNextFloor(player);
    }
}
