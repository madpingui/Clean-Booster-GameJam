using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerShrink : MonoBehaviour
{
    public PostProcessVolume volume;
    private DepthOfField _depthOfField;
    private ChromaticAberration _chromaticA;
    RaycastHit HitInfo;

    private bool isSmall;
    private bool Changing;

    private float t;
    public float timeToReachTarget;

    public PlayerMovement movement;

    public CharacterController controller;

    private Camera cam;

    public Transform playerBody;

    public Vector3 little;
    public Vector3 big;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Changing == false && CanvasController.Instance.firstShrink == true)
        {
            if (isSmall == true)
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.up, out HitInfo, 1.7f))
                {
                    return;
                }
            }

            int child = transform.GetChild(0).childCount;

            if (child > 3)
            {
                transform.GetChild(0).GetChild(3).parent = null;
            }

            if (CanvasController.Instance.waitingtShrink == false)
            {
                CanvasController.Instance.helpText.text = "";
                CanvasController.Instance.helpText.gameObject.SetActive(false);

                CanvasController.Instance.waitingtShrink = true;
            }

            isSmall = !isSmall;
            Changing = true;
            t = 0;

            movement.enabled = false;
        }

        if (isSmall == true && Changing)
        {
            t += Time.deltaTime / timeToReachTarget;

            playerBody.localScale = Vector3.Lerp(big, little, t);

            volume.profile.TryGetSettings(out _depthOfField);
            volume.profile.TryGetSettings(out _chromaticA);
            //_depthOfField.focusDistance.value = Mathf.Lerp(1.5f, 0.15f, t);
            //_depthOfField.aperture.value = Mathf.Lerp(1, 4, t);
            //_depthOfField.focalLength.value = Mathf.Lerp(15, 9, t);
            _chromaticA.intensity.value = Mathf.Lerp(0, 0.4f, t);

            cam.fieldOfView = Mathf.Lerp(60, 80, t);

            if (t >= 1)
            {
                Changing = false;
            }

            movement.enabled = true;
            movement.speed = 1.8f;
            movement.jumpHeight = 0.5f;
        }

        else if (isSmall == false && Changing)
        {
            t += Time.deltaTime / timeToReachTarget;

            playerBody.localScale = Vector3.Lerp(little, big, t);

            volume.profile.TryGetSettings(out _depthOfField);
            volume.profile.TryGetSettings(out _chromaticA);
            //_depthOfField.focusDistance.value = Mathf.Lerp(0.15f, 1.5f, t);
            //_depthOfField.aperture.value = Mathf.Lerp(4, 1, t);
            //_depthOfField.focalLength.value = Mathf.Lerp(9, 15, t);
            _chromaticA.intensity.value = Mathf.Lerp(0.4f, 0, t);

            cam.fieldOfView = Mathf.Lerp(80, 60, t);

            if (t >= 1)
            {
                Changing = false;
            }

            movement.enabled = true;
            movement.speed = 4;
            movement.jumpHeight = 0f;
        }
    }
}