using UnityEngine;

public class LlavePuerta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PuertaLlave.Instance.canBeOpened = true;
            Destroy(gameObject);
        }
    }
}
