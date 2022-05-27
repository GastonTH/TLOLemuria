using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDethZone : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("entro el player");
            /*var bandit = other.gameObject.GetComponent<BanditController>();
        
            bandit.transform.position = new Vector3(0, 0, 0);*/
        }
    }
}
