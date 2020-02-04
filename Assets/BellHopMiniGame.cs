using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellHopMiniGame : MonoBehaviour
{
    public Animator cart;
    public Animator enemy;

    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    public bool canPushCart = false;

    public GameObject player1;
    public GameObject player2;
    public GameObject prompt;

    void Start(){
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart(){
        yield return new WaitForSeconds(1.5f);
        canPushCart = true;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canPushCart == true){
            canPushCart = false;
            Destroy(prompt);
            cart.SetTrigger("Animate");
            if(isPlayer1){ 
                player1 = GameObject.Find("Player1(Clone)");
                player1.GetComponent<PlayerMovement>().ArmsPickUpAnimation();
            }
            if(isPlayer2){ 
                player2 = GameObject.Find("Player2(Clone)");
                player2.GetComponent<PlayerMovement>().ArmsPickUpAnimation();
            }
            StartCoroutine(LowerArm());
        }
    }

    IEnumerator LowerArm(){
        yield return new WaitForSeconds(1);
        if(isPlayer1) GameObject.Find("Player1(Clone)").GetComponent<PlayerMovement>().ArmsPickUpAnimation();
        if(isPlayer2) GameObject.Find("Player2(Clone)").GetComponent<PlayerMovement>().ArmsPickUpAnimation();
    }
}
