using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlaceTriggerFires : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Plane"){
            return;
        }     
        Debug.Log("PlaceTriggerFires.OnTriggerEnter");
        this.SendMessageUpwards("PlaceObjectArea", this.name); 
    }
}
