using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFlagController : MonoBehaviour
{
    [SerializeField] private GameObject exclamationMark;
    private bool isActive = false;

    private void Start()
    {
        exclamationMark.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                exclamationMark.SetActive(false);
                Debug.Log("pueden reespawnear");
                GameObject.Find("SpawnManager").GetComponent<SpawnMangerController>().Respawn();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            exclamationMark.SetActive(true);
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            exclamationMark.SetActive(false);
            isActive = false;
        }
    }
}
