using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f, turnSmoothing = 0.1f, gravity = -9.81f, jumpHeight = 5f;
    float xInput, zInput;
    Vector3 move, currentVelocity;

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        move = transform.right * xInput + transform.forward * zInput;

        controller.Move(move * speed * Time.deltaTime);

        currentVelocity.y += gravity * Time.deltaTime;

        if(controller.isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = -2f;
        }
        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            currentVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        controller.Move(currentVelocity * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Bullet")
        {
            GameObject.Find("GUI").GetComponent<GUIManager>().OnDeath();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GUIManager.canPause = false;

            if(GunFire.controlling)
            {
                Destroy(GameObject.FindWithTag("Bullet"));
            }

            GameObject.Find("Player Camera").transform.parent = null;
            GameObject.Find("Player Camera").GetComponent<PlayerViewCamera>().enabled = false;

            Destroy(gameObject);

        }
    }

}
