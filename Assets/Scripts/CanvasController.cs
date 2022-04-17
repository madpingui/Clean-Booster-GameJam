using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController Instance;

    public Image[] guns;

    [HideInInspector]
    public int garbageNmber;

    public int mission;
    public LookAnimation[] objectsChrono;
    public PickObject[] pickObjects;

    private int indexChoosed;

    public TextMeshProUGUI garbageText;

    public TextMeshProUGUI helpText;

    [HideInInspector]
    public bool firstShrink;
    [HideInInspector]
    public bool waitingtShrink;

    [HideInInspector]
    public bool waitingtWeapon2;
    [HideInInspector]
    public bool waitingtWeapon3;

    public TextMeshProUGUI interactText;

    void Awake()
    {
        Instance = this;
        foreach (LookAnimation obj in objectsChrono)
        {
            obj.TurnOffShader();
        }
        foreach (PickObject obj in pickObjects)
        {
            obj.TurnOffShader();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            helpText.gameObject.SetActive(true);
            helpText.text = "You can use your second weapon to pick up items";
            Guns.Instance.unlockedGun2 = true;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            helpText.gameObject.SetActive(true);
            helpText.text = "You can use your third weapon to go back in time";
            Guns.Instance.unlockedGun3 = true;

        }
    }

    public void PickWeapon(int index)
    {
        indexChoosed = index;

        guns[0].color = Color.white;
        if (!Guns.Instance.unlockedGun2)
        {
            guns[1].color = Color.gray;
        }
        else
        {
            guns[1].color = Color.white;
        }
        if (!Guns.Instance.unlockedGun3)
        {
            guns[2].color = Color.gray;
        }
        else
        {
            guns[2].color = Color.white;
        }

        guns[index].color = Color.green;
    }

    public void PickUpGarbage()
    {
        garbageNmber++;

        if (mission == 0)
        {
            garbageText.text = "Escombros recolectados " + garbageNmber.ToString() + "/26";
            if (garbageNmber == 26)
            {
                mission++;
                garbageText.text = "Escombros recolectados " + garbageNmber.ToString() + "/41";
            }
        }
        else if (mission == 1)
        {
            garbageText.text = "Escombros recolectados " + garbageNmber.ToString() + "/41";
            if (garbageNmber == 41)
            {
                mission++;
                garbageText.text = "Escombros recolectados " + garbageNmber.ToString() + "/49";
            }
        }
        else if (mission == 2)
        {
            garbageText.text = "Escombros recolectados " + garbageNmber.ToString() + "/49";
            if (garbageNmber == 49)
            {
                mission++;
                garbageText.text = "Obtén la llave sobre la repisa de la cocina";
            }
        }

        if (garbageNmber == 26)
        {
            helpText.gameObject.SetActive(true);
            helpText.text = "Puedes hacerte muy pequeño presionando Z, te puede ayudar a pasar por lugares algo apretados y solo así podrás saltar con BARRA ESPACIADORA";
            firstShrink = true;
        }

        if (garbageNmber == 41)
        {
            helpText.gameObject.SetActive(true);
            helpText.text = "Ahora puedes usar tu segundo Gadget con la tecla 2 , con este puedes coger los objetos que brillan ROJO";
            Guns.Instance.unlockedGun2 = true;
            PickWeapon(indexChoosed);
            foreach (PickObject obj in pickObjects)
            {
                obj.TurnOnShader();
            }
        }

        if (garbageNmber == 49)
        {
            helpText.gameObject.SetActive(true);
            helpText.text = "Usa tu tercer Gadget con la tecla 3, si le apuntas a objetos que brillan VERDE y matienes presionado click, volverán a como estaban ";
            Guns.Instance.unlockedGun3 = true;
            PickWeapon(indexChoosed);
            foreach (LookAnimation obj in objectsChrono)
            {
                obj.TurnOnShader();
            }
        }
    }

    public void InteractTextOn()
    {
        interactText.gameObject.SetActive(true);
    }
    public void InteractTextOff()
    {
        interactText.gameObject.SetActive(false);
    }
}