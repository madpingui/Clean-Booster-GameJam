using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaLlave : MonoBehaviour
{
    public static PuertaLlave Instance;
    public bool canBeOpened;

    private bool llavePuerta;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanvasController.Instance.InteractTextOn();
        }

        if (other.tag == "Player" && tag == "doorLlave")
        {
            llavePuerta = true;
        }
    }

    private void Update()
    {
        if (llavePuerta == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (canBeOpened == false)
                {
                    CanvasController.Instance.helpText.text = "Necesitas una llave para entrar aquí";
                    CanvasController.Instance.helpText.gameObject.SetActive(true);

                    StopAllCoroutines();
                    StartCoroutine(resetText());
                }
                else if (canBeOpened == true)
                {
                    transform.parent.GetComponent<Animator>().Play("Abrirpuerta");
                    endingGame.Instance.puertaFinal = true;

                    CanvasController.Instance.InteractTextOff();

                    transform.GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanvasController.Instance.InteractTextOff();
        }

        if (other.tag == "Player" && tag == "doorLlave")
        {
            llavePuerta = false;
        }
    }

    public IEnumerator resetText()
    {
        yield return new WaitForSeconds(1.5f);

        CanvasController.Instance.helpText.text = "";
        CanvasController.Instance.helpText.gameObject.SetActive(false);
    }
}
