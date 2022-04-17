using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPower : MonoBehaviour
{
    // Start is called before the first frame update
    public static ClawPower Instance;
    public AudioClip clankGrap;
    public GameObject clawGun, clawObject;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClankGraps()
    {
        GetComponent<AudioSource>().PlayOneShot(clankGrap);
    }
}
