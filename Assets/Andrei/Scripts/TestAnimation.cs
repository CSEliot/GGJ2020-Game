using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimation : MonoBehaviour{

    [Header("ANIMATION")]
    public Animator camera;
    public Animator dude;
    public Animator elevator;
    public Animator rooms;

    [Header("UI")]
    public Canvas menu;
    public Animator tower1Meter;
    public Animator tower2Meter;

    public GameObject[] roomObjects; // 0 = lobby 

    void Start(){
        menu.enabled = true;
        roomObjects[0].SetActive(true);
        roomObjects[1].SetActive(false);
    }

    public void StartAnimations(){
        StartCoroutine(MockAnimations());
    }

    IEnumerator MockAnimations(){
        yield return new WaitForSeconds(1f);

        // Enter Elevator from Lobby
        elevator.SetTrigger("Animate");
        dude.SetBool("LobbyExit",true);
        yield return new WaitForSeconds(0.5f);

        // closing door and fading out
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.25f);
        rooms.SetTrigger("Animate"); // room leave
        tower1Meter.SetTrigger("Animate");


        // Switch to ROOM 1 from LOBBY
        yield return new WaitForSeconds(1.5f);
        roomObjects[0].SetActive(false);
        roomObjects[1].SetActive(true);

        //roomObjects[2].GetComponent<MiniGame1>().Play(); // example for calling minigame


        rooms.SetTrigger("Animate"); // room enter
        elevator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");


        yield return new WaitForSeconds(2.5f);


        // End of Mini Game, Enter Elevator
        
        StartCoroutine(EndMiniGameGoToNextFloor());
    }

    IEnumerator EndMiniGameGoToNextFloor(){
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Fade");
        rooms.SetTrigger("Animate"); // room leave
        tower1Meter.SetTrigger("Animate");
    }
}
