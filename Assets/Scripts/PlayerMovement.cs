using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    RaycastHit HitInfo;

    public CharacterController controller;
    public GameObject tornado, chronoGun;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public float groundDistance;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out HitInfo, groundDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Guns.Instance.gun1 && Input.GetMouseButtonUp(0))
        {
            tornado.GetComponent<Animator>().SetTrigger("Off");
            tornado.GetComponent<AudioSource>().Stop();
        }
        if (Guns.Instance.gun1 && Input.GetMouseButtonDown(0))
        {
            tornado.GetComponent<Animator>().SetTrigger("On");

            tornado.GetComponent<AudioSource>().Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 0.2f);
    }
}