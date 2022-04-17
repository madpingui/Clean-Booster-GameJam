using UnityEngine;

public class Garbage : MonoBehaviour
{
    private bool sucked;

    private Camera cam;

    private float t;
    public float timeToReachTarget;

    private float distance;


    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Guns.Instance.gun1 != true)
        {
            t = 0;
            sucked = false;
            GetComponent<Rigidbody>().isKinematic = false;
            return;
        }

        if (cam.transform.parent.localScale.x < transform.localScale.x * 2)
        {
            t = 0;
            sucked = false;
            GetComponent<Rigidbody>().isKinematic = false;
            return;
        }

        if (sucked == true && Input.GetMouseButton(0))
        {


            t += Time.deltaTime / timeToReachTarget;
            transform.position = Vector3.Lerp(transform.position, cam.transform.position, t);

            distance = Vector3.Distance(gameObject.transform.position, cam.transform.position);

            if (distance < 2)
            {
                Destroy(gameObject);
                CanvasController.Instance.PickUpGarbage();
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vacuum")
        {
            t = 0;
            sucked = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vacuum")
        {
            t = 0;
            sucked = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
