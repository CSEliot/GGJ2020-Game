using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 18;

    private Rigidbody rig;
    public Transform body;
    public Animator legsAnim;
    public Animator armsAnim;
    public GameObject mopObject;

    private float hAxis;
    private float vAxis;

    private bool move;

    float joyAngle;

    bool isMine;

    // Start is called before the first frame update
    void Start()
    {
        isMine = GetComponent<PhotonView>().IsMine;
        if(!isMine){
            move = false;
        }else{
            move = true;
        }
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(move){
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");
        }
    }

    public void ArmsPickUpAnimation(){
        armsAnim.SetTrigger("Hold");
    }

    public void ArmsMoppingMotion(){
        armsAnim.SetTrigger("Mopping");
    }

    public void SwitchToMop(){
        armsAnim.SetBool("Mop", true);
        mopObject.SetActive(true);
    }

    public void SwitchBackFromMop(){
        armsAnim.SetBool("Mop", false);
        mopObject.SetActive(false);
    }

    public void Ceelbrate(){
        armsAnim.SetBool("Mop", false);
    }

    void FixedUpdate(){
        if(move){
            rig.isKinematic = true; 

            Vector3 movement = new Vector3(hAxis, 0.0f, vAxis) * Time.deltaTime;

            if(hAxis > 0.06f || vAxis > 0.06f || hAxis < -0.06f || vAxis < -0.06f){
                legsAnim.SetBool("Moving",true);
            }else{
                legsAnim.SetBool("Moving",false);
            }
            
            if(movement != Vector3.zero){
                rig.transform.position += transform.forward * vAxis * Time.deltaTime * speed;
                rig.transform.position += transform.right * hAxis * Time.deltaTime * speed;
            }

            //body.rotation = Quaternion.LookRotation(movement);
            
            Vector3 lookDirection = new Vector3(hAxis, 0, vAxis);
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        
            float step = 100 * Time.deltaTime;
            if(movement != Vector3.zero){
                body.transform.rotation = Quaternion.RotateTowards(lookRotation, body.transform.rotation, step);
            }else{
                body.transform.rotation = new Quaternion(0,180,0,0);
            }

            rig.isKinematic = false;

        }
    }

    public void disablePlayerMovement(){
        move = false;
    }

    public void enablePlayerMovement(){
        move = true;
    }

    [PunRPC]
    public void GrabBarrel() {
        
    }

}