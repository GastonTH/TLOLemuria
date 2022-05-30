using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFlagController : MonoBehaviour
{
    [SerializeField] private GameObject exclamationMark;

    private void Start()
    {
        exclamationMark.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            exclamationMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            exclamationMark.SetActive(false);
        }
    }
}
