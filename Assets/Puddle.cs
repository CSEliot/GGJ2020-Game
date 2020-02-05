using UnityEngine;

public class Puddle : MonoBehaviour
{
    bool inTrigger = false;
    public GameObject prompt;
    public int clicksToDelete = 7;
    public Transform scaleMesh;
    public GameObject checkMark;
    public CleanUpMiniGame gameController;

    float cleanTimer = 0;

    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    public GameObject player1;
    public GameObject player2;

    void Start(){
        prompt.SetActive(true);
    }

    void OnTriggerStay(Collider col){
        if(col.gameObject.name == "Player1(Clone)" || col.gameObject.name == "Player2(Clone)"){
            inTrigger = true;
        }   
    }

    void Update(){
        if(isPlayer1){ 
            player1 = GameObject.Find("Player1(Clone)");
            player1.GetComponent<PlayerMovement>().SwitchToMop();
        }else if(isPlayer2){ 
            player2 = GameObject.Find("Player2(Clone)");
            player2.GetComponent<PlayerMovement>().SwitchToMop();
        }

        if ((Input.GetButtonDown("Fire1")) && inTrigger) {
            if (isPlayer1 && player1 == null)
            {
                return;
            }
            if (isPlayer2 && player2 == null)
            {
                return;
            }
            clicksToDelete--;
            if(isPlayer1) player1.GetComponent<PlayerMovement>().ArmsMoppingMotion();
            else if(isPlayer2) player2.GetComponent<PlayerMovement>().ArmsMoppingMotion();

            if(clicksToDelete == 6){
                scaleMesh.localScale = new Vector3(.8f,.8f,.8f);
            }else if(clicksToDelete == 4){
                scaleMesh.localScale = new Vector3(.5f,.5f,.5f);
            }else if(clicksToDelete == 2){
                scaleMesh.localScale = new Vector3(.25f,.25f,.25f);
            }

            if(clicksToDelete == 0){
                gameController.currentObjectivesCompleted++;
                checkMark.transform.parent = gameController.transform;
                checkMark.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col){
        inTrigger = false;
    }
}
