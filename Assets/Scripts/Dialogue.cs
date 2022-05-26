using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private bool isReading;
    private int lineIndex;
    private bool isStartReading;
    private float letterForSecond = 0.05f;
    
    [SerializeField, TextArea(4, 6)] public string[] sentences;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject exclamationMark;

    // Update is called once per frame
    void Update()
    {
        // el jugador tiene que estar cerca del objeto y ademas darle a la tecla de accion (E) para que lea el dialogo
        if (isReading && Input.GetKeyDown(KeyCode.E))
        {
            // si no ha empezado a leer el dialogo, empieza a leerlo
            if (!isStartReading)
            {
                //Debug.Log("start reading");
                DialogSecuence();
            }
            //ha terminado de escribir toda la linea
            else if (dialogueText.text == sentences[lineIndex])
            {
                NextLine();
            }
            // si quiero que todas las lineas se escriban a la vez
            else
            {
                //paramos de primero todas las corrutinas
                StopAllCoroutines();
                //escribimos todas las lineas
                dialogueText.text = sentences[lineIndex];
            }
        }
    }

    private void DialogSecuence()
    {
        isStartReading = true;
        panel.SetActive(true);
        exclamationMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(WriteLine());
    }

    private void NextLine()
    {
        Debug.Log("indice de linea: " + lineIndex);
        lineIndex++;
        if (lineIndex < sentences.Length)
        {
            StartCoroutine(WriteLine());
        }
        else
        {
            isStartReading = false;
            panel.SetActive(false);
            exclamationMark.SetActive(true);
        }
    }

    private IEnumerator WriteLine()
    {
        // primero el texto estara vacio
        dialogueText.text = "";
        foreach (char l in sentences[lineIndex])
        {
            dialogueText.text += l;
            yield return new WaitForSeconds(letterForSecond);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            isReading = true;
            Debug.Log("Reading");
            exclamationMark.SetActive(isReading);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isReading = false;
        Debug.Log("Not Reading");
        exclamationMark.SetActive(isReading);
        isStartReading = false;
        panel.SetActive(false);
        dialogueText.text = "";
    }
}
