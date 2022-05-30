using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private Animator _amiAnimation;

    private void Start()
    {
        _amiAnimation = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
        if (collider.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("abriendo");
                _amiAnimation.SetTrigger("Open");
            }
        }
    }
}
