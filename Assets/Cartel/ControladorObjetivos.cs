using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorObjetivos : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark; // esto es el sprite de el signo de exclamacion
    [SerializeField] private GameObject dialoguePanel; //panel de dialogo
    [SerializeField] private TMP_Text dialogueText; //el texto
    [SerializeField, TextArea (4,6)] private string[] dialogueLines; // cuantas lineas de dialogo

    private float typingTime = 0.05f; //tiempo de aparicion de las letras

    private bool isPlayerInRange; //si el jugador esta cerca
    private bool didDialogueStart; //si empezo el dialogo
    private int lineIndex; //el index pe

    private void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Interaccion")) //<----- este axys lo podrian cambiar a otra tecla
        {
            if (!didDialogueStart)                                     //si el jugador oprime el boton (del axis , eneste caso Fire1) se prende start Dialogue
            {
                StartDialogue();
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive (true);  //aque si prenden cosas como el panel y se arranca el dialogo , tambbien se apaga la exclamacion
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(Showline());

    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(Showline());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1;
        }
    }

    private IEnumerator Showline()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])    //contorla la aparicion de las letras
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime); //aqui coge el tiempo del TP 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
             isPlayerInRange = false;
             dialogueMark.SetActive(false);
        
    }
}
