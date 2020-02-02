using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{

    [Header("ANIMATION")]
    public Animator camera;
    public Animator dude;
    public Animator elevator;
    public Animator rooms;
    public Animator camera2;
    public Animator dude2;
    public Animator elevator2;
    public Animator rooms2;

    [Header("DATA")]
    public int player1Floor = 0;
    public int player2Floor = 0;
    [HideInInspector]
    public bool isGameOver = false;
    public Transform playerPosition;

    [Header("UI")]
    public Canvas hud;
    public Canvas menu;
    public Animator tower1Meter;
    public Animator tower2Meter;

    [Header("PLAYERS")]
    public Transform player1PreAnimated;
    public Transform player2PreAnimated;
    public Transform player1Position;
    public Transform player2Position;
    public Transform player1Prefab;
    public Transform player2Prefab;
    [Space]
    //[HideInInspector]
    public Transform player1;
    //[HideInInspector]
    public Transform player2;


    [Header("ROOMS")]
    public GameObject[] roomObjects; // 0 = lobby 
    public GameObject[] roomObjects2; // 0 = lobby 

    void Start(){
        menu.enabled = true;
        roomObjects[0].SetActive(true);
        roomObjects[1].SetActive(false);
    }

    public void StartMatch(){ // when both players are in, the first mini game starts
        // SPAWN PLAYER HERE?

        hud.enabled = true;
        StartCoroutine(LeaveLobby());
    }

    public void StartAnimations(){ // TEMPORARY
        StartCoroutine(MockAnimations());
        hud.enabled = true;
    }

    public void Update(){
        if(isGameOver){
            if(player1Floor == 4){
                Player1Win();
            }else if(player2Floor == 4){
                Player2Win();
            }
        }
    }

    void Player1Win(){

    }

    void Player2Win(){

    }

    // Increase Floor for Player
    public void PlayerMoveToNextFloor(int playerNumber){ // 0 = Player 1, 1 - Player 2
        if(playerNumber == 0){
            StartCoroutine(EndMiniGamePlayer1());
        }else if(playerNumber == 1){
            StartCoroutine(EndMiniGamePlayer2());
        }
    }

    IEnumerator LeaveLobby(){
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
        tower1Meter.SetTrigger("Animate");
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room leave

        yield return new WaitForSeconds(1.5f);

        player1 = Instantiate(player1Prefab, new Vector3(1000,1000,1000), player1Position.transform.rotation);

        roomObjects[0].SetActive(false);
        roomObjects[1].SetActive(true);
        rooms.SetTrigger("Animate"); // room enter
        elevator.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");
        player1Floor++;
        yield return new WaitForSeconds(0.2f);

        player1PreAnimated.gameObject.SetActive(false);
        player1.position = player1Position.position;
    }

    IEnumerator EndMiniGamePlayer1(){
        player1PreAnimated.gameObject.SetActive(true);
        player1.position = new Vector3(1000,1000,1000);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room leave
        tower1Meter.SetTrigger("Animate");
        player1Floor++;
        yield return new WaitForSeconds(0.2f);

        if(player1Floor == 4){ // 3 is max, so if we go one more we won
            isGameOver = true;
            StartCoroutine(LoadRoofPlayer1());
        }else{
            StartCoroutine(LoadNextRoomPlayer1());
        }
    }

    IEnumerator EndMiniGamePlayer2(){
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        dude2.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Fade");
        camera2.SetTrigger("Animate");
        rooms2.SetTrigger("Animate"); // room leave
        tower2Meter.SetTrigger("Animate");
        player2Floor++;
        yield return new WaitForSeconds(0.2f);

        if(player2Floor == 4){ // 3 is max, so if we go one more we won
            isGameOver = true;
        }else{
            StartCoroutine(LoadNextRoomPlayer2());
        }
    }

    IEnumerator LoadNextRoomPlayer1(){
        yield return new WaitForSeconds(1.5f);
        if(player1Floor == 1){
            roomObjects[0].SetActive(false);
            roomObjects[1].SetActive(true);
        }else if(player1Floor == 2){
            roomObjects[1].SetActive(false);
            roomObjects[2].SetActive(true);
        }else if(player1Floor == 3){
            roomObjects[2].SetActive(false);
            roomObjects[3].SetActive(true);
        }
        rooms.SetTrigger("Animate"); // room enter
        camera.SetTrigger("Animate");
        elevator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");
    }

    IEnumerator LoadRoofPlayer1(){
        yield return new WaitForSeconds(1.5f);
        roomObjects[4].SetActive(false);
        roomObjects[5].SetActive(true);
        rooms.SetTrigger("Animate"); // room enter
        camera.SetTrigger("Animate");
        elevator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");
    }

    IEnumerator LoadNextRoomPlayer2(){
        yield return new WaitForSeconds(1.5f);
        if(player2Floor == 1){
            roomObjects2[0].SetActive(false);
            roomObjects2[1].SetActive(true);
        }else if(player2Floor == 2){
            roomObjects2[1].SetActive(false);
            roomObjects2[2].SetActive(true);
        }else if(player2Floor == 3){
            roomObjects2[2].SetActive(false);
            roomObjects2[3].SetActive(true);
        }
        rooms2.SetTrigger("Animate"); // room enter
        camera2.SetTrigger("Animate");
        elevator2.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude2.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator2.SetTrigger("Animate");
    }


    IEnumerator MockAnimations(){ // TEMP STUFF
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
        tower1Meter.SetTrigger("Animate");
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room leave

        // Switch to ROOM 1 from LOBBY  ////////////////////////////////////////////
        yield return new WaitForSeconds(1.5f);
        roomObjects[0].SetActive(false);
        roomObjects[1].SetActive(true);
        rooms.SetTrigger("Animate"); // room enter
        elevator.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");


        yield return new WaitForSeconds(2.5f);


        // End of Mini Game, Enter Elevator
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room leave
        tower1Meter.SetTrigger("Animate");
        player1Floor++;

        // Switch to ROOM 2 from ROOM 1  ////////////////////////////////////////////
        yield return new WaitForSeconds(1.5f);
        roomObjects[1].SetActive(false);
        roomObjects[2].SetActive(true);
        camera.SetTrigger("Animate");
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
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room leave
        tower1Meter.SetTrigger("Animate");
        player1Floor++;

        // Switch to ROOM 3 from ROOM 2  ////////////////////////////////////////////
        yield return new WaitForSeconds(1.5f);
        roomObjects[2].SetActive(false);
        roomObjects[3].SetActive(true);
        camera.SetTrigger("Animate");
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
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room leave
        tower1Meter.SetTrigger("Animate");
        player1Floor++;

        // Switch to ROOM 4 from ROOM 3  ////////////////////////////////////////////
        yield return new WaitForSeconds(1.5f);
        roomObjects[3].SetActive(false);
        roomObjects[4].SetActive(true);
        camera.SetTrigger("Animate");
        rooms.SetTrigger("Animate"); // room enter
        elevator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");
    }
}
