using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 18;

    private Rigidbody rig;

    private float hAxis;
    private float vAxis;

    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(move){
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");
        }
    }

    void FixedUpdate(){
        if(move){
            //Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;
            //rig.MovePosition(transform.position + movement);
            rig.transform.position += transform.forward * vAxis * Time.deltaTime * speed;
            rig.transform.position += transform.right * hAxis * Time.deltaTime * speed;
        }
    }

    public void disablePlayerMovement(){
        move = false;
    }

    public void enablePlayerMovement(){
        move = true;
    }

}