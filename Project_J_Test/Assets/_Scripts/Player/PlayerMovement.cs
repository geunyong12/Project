using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 15;
    public float speed = 20;
    float input;

    Vector3 moveDirX;

    Rigidbody2D playerRigidbody;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = Input.GetAxis("Horizontal");
		if (Input.GetAxis("Horizontal") != 0)
        {
			moveDirX = new Vector3(input, 0, 0).normalized;
			transform.position += moveDirX * speed * Time.deltaTime;
		}
		if (Input.GetKeyDown(KeyCode.Space))
			playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}
}
