using System.Collections;
using UnityEngine;

public class PuertaChronos : MonoBehaviour
{
    public GameObject llaveFinal;

    private bool doorChronos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanvasController.Instance.InteractTextOn();
        }

        if (other.tag == "Player" && tag == "doorChronos")
        {
            doorChronos = true;
        }
    }

    private void Update()
    {
        if(doorChronos == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.parent.GetComponent<Animator>().Play("Abrirpuerta");
                llaveFinal.SetActive(true);

                CanvasController.Instance.InteractTextOff();

                transform.parent.GetChild(3).gameObject.SetActive(false);
                transform.parent.GetChild(4).gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanvasController.Instance.InteractTextOff();
        }

        if (other.tag == "Player" && tag == "doorChronos")
        {
            doorChronos = false;
        }
    }

    public IEnumerator resetText()
    {
        yield return new WaitForSeconds(1.5f);

        CanvasController.Instance.helpText.text = "";
        CanvasController.Instance.helpText.gameObject.SetActive(false);
    }
}
