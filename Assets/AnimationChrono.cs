using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChrono : MonoBehaviour
{
    public static AnimationChrono animationChrono;

    [HideInInspector]
    public Animator anim;

    void Start()
    {
        animationChrono = this;
        anim = GetComponent<Animator>();
    }
}
