using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGun : MonoBehaviour
{
    RaycastHit HitInfo;

    public static PickGun Instance;

    public bool carrying;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out HitInfo, 5.0f, LayerMask.GetMask("Pickable")))
        {
            if (HitInfo.collider.tag == "Pickable")
            {
                if (Input.GetMouseButtonDown(0) && carrying == false)
                {
                    HitInfo.collider.GetComponent<PickObject>().PickedUp();
                    carrying = true;
                }
            }
        }
    }
}
