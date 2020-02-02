using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellHopMiniGame : MonoBehaviour
{
    public Animator cart;
    public Animator enemy;

    public bool canPushCart = false;

    void Start(){
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart(){
        yield return new WaitForSeconds(1);
        canPushCart = true;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canPushCart == true){
            cart.SetTrigger("Animate");
        }
    }
}
