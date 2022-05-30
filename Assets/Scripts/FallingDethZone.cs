using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDethZone : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            int maxVitH = GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.MaxVit;
            Debug.Log("entro el player");
            GameObject.Find("GameManager").GetComponent<GameManager>().ReturnLastPositionFromDead();
            GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.CurrentVit -= (maxVitH * 15) / 100;
            Debug.Log("max = " + maxVitH + "current" + GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.CurrentVit);
            GameObject.Find("GameManager").GetComponent<GameManager>().healthBarController.setHealth(GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.CurrentVit);
            if (GameObject.Find("GameManager").GetComponent<GameManager>().MyHeroe.CurrentVit <= 0)
            {
                GameObject.Find("Player(Clone)").GetComponent<BanditController>().Die();
            }
        }
    }
}
