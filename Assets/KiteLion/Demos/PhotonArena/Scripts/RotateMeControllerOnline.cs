using KiteLion.Debugging;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMeControllerOnline : MonoBehaviour
{

    public float RotateSpeed = 0.5f;
    public float ForwardSpeed = 3;

    float _sideInput = 0;
    float _forwardInput = 0;

    bool _isMine = false;

    PhotonArenaManager _PM;

    // Use this for initialization
    void Start()
    {
        CBUG.Do("Online Character Spawned!");
        _PM = PhotonArenaManager.Instance;

        if(GetComponent<PhotonView>().IsMine == false)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine == false) {
            return;
        }

        _sideInput = Input.GetAxis("Horizontal");
        _forwardInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (GetComponent<PhotonView>().IsMine == false) {
            return;
        }

        GetComponent<Rigidbody>().isKinematic = true;
        Vector3 previousRotation = new Vector3(0, GetComponent<Rigidbody>().rotation.eulerAngles.y);
        Vector3 newRotation = new Vector3(0, previousRotation.y + _sideInput * RotateSpeed);

        Vector3 previousVelocity = new Vector3(0, 0, GetComponent<Rigidbody>().velocity.z);
        Vector3 newVelocity = new Vector3(0, 0, previousVelocity.z + _forwardInput * ForwardSpeed);

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(newRotation);
        GetComponent<Rigidbody>().velocity = transform.forward * _forwardInput;
        GetComponent<Rigidbody>().isKinematic = false;

    }
}
