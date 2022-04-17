using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAnimation : MonoBehaviour
{
    RaycastHit HitInfo;

    private Animator animSelected;

    void Update()
    {
        if (Guns.Instance.gun3 != true)
        {
            if (animSelected != null)
            {
                animSelected.SetFloat("Speed", 0);
                animSelected = null;
            }
            return;
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out HitInfo, 5.0f, LayerMask.GetMask("Animate")))
        {
            if (HitInfo.collider.tag == "Animatable")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AnimationChrono.animationChrono.anim.SetTrigger("On");
                    AnimationChrono.animationChrono.GetComponent<AudioSource>().Play();
                }
                if (Input.GetMouseButton(0))
                {

                    animSelected = HitInfo.collider.gameObject.GetComponent<Animator>();
                    HitInfo.collider.gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
                }
            }
        }
        else
        {
            if (animSelected != null)
            {

                animSelected.SetFloat("Speed", 0);
                animSelected = null;
            }
        }

    }
    public void _EndAnimation()
    {
        AnimationChrono.animationChrono.anim.SetTrigger("Off");
        TurnOffShader();
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