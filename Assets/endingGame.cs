using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class endingGame : MonoBehaviour
{
    public static endingGame Instance;

    public Animator[] BookShelfs;
    public GameObject canvasEndGame;
    RaycastHit HitInfo;

    bool final = false;

    [HideInInspector]
    public bool puertaFinal;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && final)
        {
            ExitGame();
        }
        if (Guns.Instance.gun3 != true)
        {
            return;
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out HitInfo, 5.0f, LayerMask.GetMask("Final")))
        {
            if (HitInfo.collider.tag == "Final")
            {
                if (Input.GetMouseButton(0) && !final && puertaFinal)
                {
                    AnimationChrono.animationChrono.anim.SetTrigger("On");
                    EndingGame();
                    final = true;
                }
            }
        }
    }


    public void EndingGame()
    {
        for (int i = 0; i < BookShelfs.Length; i++)
        {
            BookShelfs[i].SetFloat("Speed", 1f);
        }
        canvasEndGame.SetActive(true);
        canvasEndGame.GetComponent<Animator>().SetTrigger("On");

    }
    public void ExitGame()
    {
        Application.Quit();
    }



}
