using System.Collections;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    private Transform playerCam;
    bool beingCarried = false;
    private bool touched = false;

    private bool startCarry;

    private Vector3 initialTransf;

    private void Awake()
    {
        initialTransf = transform.position;
        playerCam = Camera.main.transform;
    }

    void Update()
    {
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
                startCarry = false;
                ClawPower.Instance.clawObject.transform.localPosition = new Vector3(-3f, 0f, -3f);
                Invoke("StopCarry", 0.5f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                startCarry = false;
                ClawPower.Instance.clawObject.transform.localPosition = new Vector3(-3f, 0f, -3f);
                Invoke("StopCarry", 0.5f);
            }
        }
    }

    public void PickedUp()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = playerCam;
        beingCarried = true;
        ClawPower.Instance.clawObject.transform.position = transform.position;
        ClawPower.Instance.ClankGraps();
        Invoke("StartCarry", 0.3f);
    }

    public void StartCarry()
    {
        startCarry = true;
    }

    public void StopCarry()
    {
        PickGun.Instance.carrying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Untagged")
        {
            if (startCarry)
            {
                if (beingCarried)
                {
                    touched = true;
                }
            }
        }

        if (other.tag == "Void")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialTransf;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Untagged")
        {
            if (startCarry)
            {
                if (beingCarried)
                {
                    touched = true;
                }
            }
        }
    }

    public void TurnOnShader()
    {
        MaterialPropertyBlock _propBlock = new MaterialPropertyBlock();

        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer _renderer = transform.GetChild(i).GetComponent<Renderer>();
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_GlowingValue", 1f);
            _renderer.SetPropertyBlock(_propBlock);
        }
    }
    public void TurnOffShader()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer _renderer = transform.GetChild(i).GetComponent<Renderer>();
            MaterialPropertyBlock _propBlock = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetFloat("_GlowingValue", 0f);
            _renderer.SetPropertyBlock(_propBlock);
        }
    }
}