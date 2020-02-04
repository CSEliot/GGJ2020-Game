using UnityEngine;

public class PickUpSuccess : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectPickup;
    
    [SerializeField]
    private GameObject ObjectPlayer;

    public GameManager gameManager;
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;

    void Awake(){
       if(isPlayer1) ObjectPlayer = gameManager.player1.GetComponent<PlayerObjectList>().pipePickup.gameObject;
       if(isPlayer2) ObjectPlayer = gameManager.player2.GetComponent<PlayerObjectList>().pipePickup.gameObject;
    }


    public void PickUpObject(string objectPickedUp) {
        ObjectPickup.SetActive(false);
        ObjectPlayer.SetActive(true);
        ObjectPlayer.transform.parent.parent.parent.GetComponent<PlayerMovement>().ArmsPickUpAnimation();
    }
}

