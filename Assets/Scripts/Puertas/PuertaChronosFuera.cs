using System.Collections;
using UnityEngine;

public class PuertaChronosFuera : MonoBehaviour
{
    private bool entro;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CanvasController.Instance.InteractTextOn();
        }

        if (other.tag == "Player" && tag == "doorChronosFuera")
        {
            entro = true;
        }
    }

    private void Update()
    {
        if (entro == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CanvasController.Instance.helpText.text = "La puerta está bloqueada desde el interior";
                CanvasController.Instance.helpText.gameObject.SetActive(true);

                StopAllCoroutines();
                StartCoroutine(resetText());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CanvasController.Instance.InteractTextOff();
        }

        if (other.tag == "Player" && tag == "doorChronosFuera")
        {
            entro = false;
        }
    }

    public IEnumerator resetText()
    {
        yield return new WaitForSeconds(1.5f);

        CanvasController.Instance.helpText.text = "";
        CanvasController.Instance.helpText.gameObject.SetActive(false);
    }
}
