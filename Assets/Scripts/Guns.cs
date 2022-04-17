using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public GameObject[] guns;

    [HideInInspector]
    public bool gun1;

    [HideInInspector]
    public bool gun2;

    [HideInInspector]
    public bool gun3;

    public bool unlockedGun2;
    public bool unlockedGun3;

    public static Guns Instance;

    private void Awake()
    {
        Instance = this;
        gun1 = true;

        Invoke("escogerArmaInicio", 2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (GameObject child in guns)
            {
                child.SetActive(false);
            }

            guns[0].SetActive(true);

            gun1 = true;
            gun2 = false;
            gun3 = false;

            CanvasController.Instance.PickWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && unlockedGun2 == true)
        {
            if (CanvasController.Instance.waitingtWeapon2 == false)
            {
                CanvasController.Instance.helpText.text = "";
                CanvasController.Instance.helpText.gameObject.SetActive(false);

                CanvasController.Instance.waitingtWeapon2 = true;
            }

            foreach (GameObject child in guns)
            {
                child.SetActive(false);
            }

            guns[1].SetActive(true);

            gun1 = false;
            gun2 = true;
            gun3 = false;

            CanvasController.Instance.PickWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && unlockedGun3 == true)
        {
            if (CanvasController.Instance.waitingtWeapon3 == false)
            {
                CanvasController.Instance.helpText.text = "";
                CanvasController.Instance.helpText.gameObject.SetActive(false);

                CanvasController.Instance.waitingtWeapon3 = true;
            }

            foreach (GameObject child in guns)
            {
                child.SetActive(false);
            }

            guns[2].SetActive(true);

            gun1 = false;
            gun2 = false;
            gun3 = true;

            CanvasController.Instance.PickWeapon(2);
        }
    }

    public void escogerArmaInicio()
    {
        foreach (GameObject child in guns)
        {
            child.SetActive(false);
        }

        guns[0].SetActive(true);

        gun1 = true;
        gun2 = false;
        gun3 = false;

        CanvasController.Instance.PickWeapon(0);
    }
}
