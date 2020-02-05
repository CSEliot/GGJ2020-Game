using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KiteLion.Debugging;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Photon.Pun;

public class GameManager : MonoBehaviour{

    [Header("ANIMATION")]
    public new Animator camera;
    public Animator dude;
    public Animator elevator;
    public Animator elevatorWalls;
    public Animator rooms;
    public Animator camera2;
    public Animator dude2;
    public Animator elevator2;
    public Animator elevatorWalls2;
    public Animator rooms2;

    [Header("DATA")]
    public PhotonArenaManager PM;
    public int player1Floor = 0;
    public int player2Floor = 0;
    public int playerWonIndex = 0;
    [HideInInspector]
    public bool isGameOver = false;
    bool isGameStarted = false;
    public Transform playerPosition;
    public float countdownLength = 2;
    public float playerTime = 0;
    public float opponentTime = 0;

    [Header("UI")]
    public Canvas hud;
    public Canvas menu;
    public Animator startMenu;
    public GameObject playAgainMenu;
    public GameObject startHUD;
    public GameObject waitingForHUD;
    public TMP_Text winTextPlayer1;
    public TMP_Text winTextPlayer2;
    public Animator tower1Meter;
    public Animator tower2Meter;
    public TMP_Text playerTimeText;
    public TMP_Text playerTimeHUD;
    public TMP_Text opponentTimeText;
    public TMP_Text countdownText;
    public GameObject countdownMenu;

    public int cameraDepth = 0;
    public Camera player1Camera;
    public Camera player2Camera;
    public RenderTexture player1Render;

    public Transform camera1Position;
    public Transform camera2Position;

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

    [Header("MULTIPLAYER")]
    private bool isSpawned;
    private bool isOtherPlayerGot;
    int playerID;
    int countdownStartTime = 0;


    [Header("ROOMS")]
    public GameObject[] roomObjects; // 0 = lobby 
    public GameObject[] roomObjects2; // 0 = lobby 

    /// <summary>
    /// IN MILLISECONDS
    /// </summary>
    public int gameStartTime;

    void Start(){
        PM = PhotonArenaManager.Instance;

        menu.enabled = true;
        playAgainMenu.SetActive(false);
        waitingForHUD.SetActive(false);
        startHUD.SetActive(true);
        hud.enabled = false;
        roomObjects[0].SetActive(true);
        roomObjects[1].SetActive(false);
        roomObjects[2].SetActive(false);
        roomObjects[3].SetActive(false);
        roomObjects[4].SetActive(false);
        roomObjects2[0].SetActive(true);
        roomObjects2[1].SetActive(false);
        roomObjects2[2].SetActive(false);
        roomObjects2[3].SetActive(false);
        roomObjects2[4].SetActive(false);

        PlayerPrefs.SetInt("CBUG_ON", 0);

        //foreach (GameObject obj in GameObject.fi) {

        //}
    }

    public void CloseMenu(){
        StartCoroutine(DisableMenu());
    }

    public void ExitGame() {
        Application.Quit();
    }

    IEnumerator DisableMenu(){
        startMenu.SetTrigger("Animate");
        yield return new WaitForSeconds(1);
        startMenu.enabled = false;
        startHUD.SetActive(false);
    }

    public void LoadLobby(){
        PM.ConnectAndJoinRoom("Player", null);
        waitingForHUD.SetActive(true);
    }

    public void StartMatch(){ // when both players are in, the first mini game starts
        hud.enabled = true;
        StartCoroutine(LeaveLobby());
        StartCoroutine(LeaveLobby2());
        StartCoroutine(CountdownClose());
    }

    IEnumerator CountdownClose(){
        yield return new WaitForSeconds(1);
        countdownMenu.SetActive(false);
    }

    public void StartAnimations(){ // TEMPORARY
        //StartCoroutine(MockAnimations());
        hud.enabled = true;
    }

    public void QuitGame(){

        foreach (UnityEngine.Object Obj in DontDestroyThis.List) {
            Destroy(Obj);
        }
        //SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Update(){
        
        if (PM.CurrentServerUserDepth == PhotonArenaManager.ServerDepthLevel.InRoom && isSpawned == false) {
            playerID = PM.GetLocalPlayerID();

            if(playerID == 1){
                player1 = PM.SpawnPlayer(new Vector3(1000,1000,1000), player1Position.transform.rotation, "Player1").transform;
                player1.rotation = Quaternion.Euler(new Vector3(0,-45,0));
                player1.tag = "PlayerSelf";
                //camera.transform.position = camera1Position.position;
                //camera2.transform.position = camera2Position.position;
                player1Camera.targetTexture = null;
                player2Camera.targetTexture = player1Render;
            }else if(playerID == 2){
                player2 = PM.SpawnPlayer(new Vector3(1000,1000,1000), player2Position.transform.rotation, "Player2").transform;
                player2.rotation = Quaternion.Euler(new Vector3(0,-45,0));
                player2.tag = "PlayerSelf";
                //camera.transform.position = camera2Position.position;
                //camera2.transform.position = camera1Position.position;
                player1Camera.targetTexture = player1Render;
                player2Camera.targetTexture = null;
            }

            CBUG.Do(playerID + " - Player ID");
            isSpawned = true;
        }

        if(player1 != null && player2 != null && PM.CurrentServerUserDepth == PhotonArenaManager.ServerDepthLevel.InRoom){
            waitingForHUD.SetActive(false);
            if(!isGameStarted){
                countdownMenu.SetActive(true);
            }
        }
        
        if (PM.CurrentServerUserDepth == PhotonArenaManager.ServerDepthLevel.InRoom && isOtherPlayerGot == false) {
            if(playerID == 1) {
                GameObject OtherPlayer = GameObject.FindWithTag("PlayerOther");
                if(OtherPlayer != null){
                    player2 = OtherPlayer.transform;
                }
            } else if(playerID == 2) {
                GameObject OtherPlayer = GameObject.FindWithTag("PlayerOther");
                if(OtherPlayer != null){
                    player1 = OtherPlayer.transform;
                }
            }
        }

        if(player1 != null && player2 != null && isGameStarted == false){
            //countdown start time = Getclockinseconds
            //if (countdown start time )
            if(countdownStartTime == 0) {
                countdownStartTime = PM.GetClock();
                //CBUG.Do("Countdown Started" + countdownStartTime);
                //CBUG.Do("Countdown Length" + countdownLength);
            }
            
            //CBUG.Do(countdownStartTime - PM.GetClock() + ": Countdown start time - GetClockInSeconds");

            if(PM.GetClock() - countdownStartTime >= countdownLength ) {
                isGameStarted = true;
                gameStartTime = PM.GetClock();
                PM.GetRoom().IsVisible = false;
                PM.GetRoom().IsOpen = false;
                StartMatch();
                countdownText.text = "GO!";
                CBUG.Do("Start game!");
            }  
        }

        if(isGameStarted == true && !isGameOver){
            playerTime = (PM.GetClock() - gameStartTime)/1000f;
            playerTimeHUD.text = Convert.ToInt32(playerTime).ToString();

            if(PM.CurrentServerUserDepth == PhotonArenaManager.ServerDepthLevel.Offline || PM.GetRoom().PlayerCount < 2) {
                QuitGame();
            }
        }
    }

    [PunRPC]
    void GetPlayerWinTime(){
        if(playerID == 1) {

            if(PM.GetData("player1WinTime") != null) {
                playerTimeText.text = "YOUR TIME: " + (float)PM.GetData("player1WinTime");
            } else {
                playerTimeText.text = "YOU'RE NOT FINISHED YET!";
            }
            if(PM.GetData("player2WinTime") != null) {
                opponentTimeText.text = "OPPONENT TIME: " + PM.GetData("player2WinTime");
            } else {
                opponentTimeText.text = "OPPONENT NOT FINISHED YET!";
            }
        } else {
            if(PM.GetData("player2WinTime") != null) {
                playerTimeText.text = "YOUR TIME: " + (float)PM.GetData("player2WinTime");
            } else {
                playerTimeText.text = "YOU'RE NOT FINISHED YET!";
            }
            if(PM.GetData("player1WinTime") != null) {
                opponentTimeText.text = "OPPONENT TIME: " + PM.GetData("player1WinTime");
            } else {
                opponentTimeText.text = "OPPONENT NOT FINISHED YET!";
            }
        }

    }

    public void Player1Lose(){
        winTextPlayer2.text = "YOU WIN!";
        winTextPlayer1.text = "YOU LOSE";
    }

    public void Player2Lose(){
        winTextPlayer1.text = "YOU WIN!";
        winTextPlayer2.text = "YOU LOSE";
    }

    IEnumerator PlayAgainDelay(){
        yield return new WaitForSeconds(3);
        playAgainMenu.SetActive(true);
        if(playerID == 2) {
            playAgainMenu.GetComponent<Canvas>().worldCamera = player2Camera;
        }
        if (playerID == 1) {
            playAgainMenu.GetComponent<Canvas>().worldCamera = player1Camera;
        }
    }

    // Increase Floor for Player
    [PunRPC]
    public void PlayerMoveToNextFloor(int playerNumber){ // 0 = Player 1, 1 - Player 2
        Debug.Log("END MINIGAME: " + playerNumber);
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
        camera2.SetTrigger("Animate");
        if (PM.IsLocalClient(player1.GetComponent<PhotonView>()))
        {
            MusicManager.ElevatorMusic();
        }
        rooms.SetTrigger("Animate"); // room leave

        yield return new WaitForSeconds(1.5f);

        //player1 = Instantiate(player1Prefab, new Vector3(1000,1000,1000), player1Position.transform.rotation);

        roomObjects[0].SetActive(false);
        roomObjects[1].SetActive(true);
        rooms.SetTrigger("Animate"); // room enter
        if (PM.IsLocalClient(player1.GetComponent<PhotonView>()))
        {
            MusicManager.GameMusic();
        }
        elevator.SetTrigger("Fade");
        camera2.SetTrigger("Animate");
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

    IEnumerator LeaveLobby2(){
        yield return new WaitForSeconds(1f);

        // Enter Elevator from Lobby
        elevator2.SetTrigger("Animate");
        dude2.SetBool("LobbyExit",true);
        yield return new WaitForSeconds(0.5f);
        // closing door and fading out
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Fade");
        yield return new WaitForSeconds(0.25f);
        tower2Meter.SetTrigger("Animate");
        camera.SetTrigger("Animate");
        if (PM.IsLocalClient(player2.GetComponent<PhotonView>()))
        {
            MusicManager.ElevatorMusic();
        }
        rooms2.SetTrigger("Animate"); // room leave

        yield return new WaitForSeconds(1.5f);

        //player1 = Instantiate(player1Prefab, new Vector3(1000,1000,1000), player1Position.transform.rotation);

        roomObjects2[0].SetActive(false);
        roomObjects2[1].SetActive(true);
        rooms2.SetTrigger("Animate"); // room enter
        if (PM.IsLocalClient(player2.GetComponent<PhotonView>()))
        {
            MusicManager.GameMusic();
        }
        elevator2.SetTrigger("Fade");
        camera.SetTrigger("Animate");
        if (PM.IsLocalClient(player2.GetComponent<PhotonView>()))
        {
            MusicManager.ElevatorMusic();
        }
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude2.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator2.SetTrigger("Animate");
        player2Floor++;
        yield return new WaitForSeconds(0.2f);

        player2PreAnimated.gameObject.SetActive(false);
        player2.position = player2Position.position;
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
        if (PM.IsLocalClient(player1.GetComponent<PhotonView>()))
        {
            MusicManager.ElevatorMusic();
        }
        yield return new WaitForSeconds(0.2f);

        if(player1Floor == 4){ // 3 is max, so if we go one more we won
            StartCoroutine(LoadRoofPlayer1());
            if(playerID == 1) {
                isGameOver = true;
                PM.SaveData("isGameOver", true);
                PM.SaveData("player1WinTime", playerTime);
                hud.enabled = false;
            }

            playerTimeText.text = "YOUR TIME: " + playerTime;
            opponentTimeText.text = "OPPONENT TIME: " + opponentTime;
            GetComponent<PhotonView>().RPC("GetPlayerWinTime", RpcTarget.All);
        }else{
            StartCoroutine(LoadNextRoomPlayer1());
        }

        if (player1Floor == 4)
        {
            StartCoroutine(PlayAgainDelay());
        }
    }

    IEnumerator EndMiniGamePlayer2(){
        player2PreAnimated.gameObject.SetActive(true);
        player2.position = new Vector3(1000,1000,1000);
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
        if (PM.IsLocalClient(player2.GetComponent<PhotonView>()))
        {
            MusicManager.ElevatorMusic();
        }
        yield return new WaitForSeconds(0.2f);

        if(player2Floor == 4){ // 3 is max, so if we go one more we won
            StartCoroutine(LoadRoofPlayer2());
            if(playerID == 2)
            {
                isGameOver = true;
                PM.SaveData("isGameOver", true);
                PM.SaveData("player2WinTime", playerTime);
                hud.enabled = false;
            }
            playerTimeText.text = "YOUR TIME: " + playerTime;
            opponentTimeText.text = "OPPONENT TIME: " + opponentTime;
            GetComponent<PhotonView>().RPC("GetPlayerWinTime", RpcTarget.All);
        }else{
            StartCoroutine(LoadNextRoomPlayer2());
        }

        if(player2Floor == 4)
        {
            StartCoroutine(PlayAgainDelay());
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
            StartCoroutine(MiniGame3(0));
            //StartCoroutine(FakeWinMiniGame3(0));
        }
        rooms.SetTrigger("Animate"); // room enter
        camera.SetTrigger("Animate");
        elevator.SetTrigger("Fade");
        if (PM.IsLocalClient(player1.GetComponent<PhotonView>()))
        {
            MusicManager.GameMusic();
        }
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");
    }

    IEnumerator LoadRoofPlayer1(){
        yield return new WaitForSeconds(1.5f);
        roomObjects[3].SetActive(false);
        roomObjects[4].SetActive(true);
        
        rooms.SetTrigger("Animate"); // room enter
        elevatorWalls.SetTrigger("Animate");
        camera.SetTrigger("Animate");
        elevator.SetTrigger("Fade");
        if (PM.IsLocalClient(player1.GetComponent<PhotonView>()))
        {
            MusicManager.GameMusic();
        }
        yield return new WaitForSeconds(0.5f);
        elevator.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator.SetTrigger("Animate");
    }

    IEnumerator LoadRoofPlayer2(){
        yield return new WaitForSeconds(1.5f);
        roomObjects2[3].SetActive(false);
        roomObjects2[4].SetActive(true);
        
        rooms2.SetTrigger("Animate"); // room enter
        elevatorWalls2.SetTrigger("Animate");
        camera2.SetTrigger("Animate");
        elevator2.SetTrigger("Fade");
        if (PM.IsLocalClient(player2.GetComponent<PhotonView>()))
        {
            MusicManager.GameMusic();
        }
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude2.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator2.SetTrigger("Animate");
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
            StartCoroutine(MiniGame3(1));
            //StartCoroutine(FakeWinMiniGame3(1));
        }
        rooms2.SetTrigger("Animate"); // room enter
        camera2.SetTrigger("Animate");
        elevator2.SetTrigger("Fade");
        if (PM.IsLocalClient(player2.GetComponent<PhotonView>()))
        {
            MusicManager.GameMusic();
        }
        yield return new WaitForSeconds(0.5f);
        elevator2.SetTrigger("Animate");
        yield return new WaitForSeconds(0.3f);
        dude2.SetTrigger("EnterExit");
        yield return new WaitForSeconds(0.25f);
        elevator2.SetTrigger("Animate");
    }

    IEnumerator FakeWinMiniGame3(int playerIndex){
        yield return new WaitForSeconds(2);
        Debug.Log("FakeWinMiniGame3( called bad!");
        //PlayerMoveToNextFloor(playerIndex);
    }

    IEnumerator MiniGame3(int playerIndex){
        yield return new WaitForSeconds(1.5f);
        if(playerIndex == 0){
            player1PreAnimated.gameObject.SetActive(false);
            player1.position = player1Position.position;
        }else{
            player2PreAnimated.gameObject.SetActive(false);
            player2.position = player2Position.position;
        }
    }
}
