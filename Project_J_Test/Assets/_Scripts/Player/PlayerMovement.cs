using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 15;
    public float speed = 20;
	public float maxSpeed;
    float input;

    Vector3 moveDirX;
	int defaultLayer;
	int hideLayer;

    Rigidbody2D playerRigidbody;

	void Awake()
	{
		if (DataController.Instance.PlayerData.playerPosition1 != new Vector3(0, 0, 0))
			transform.position = DataController.Instance.PlayerData.playerPosition1;
	}
	void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
		defaultLayer = LayerMask.NameToLayer("Player");
		hideLayer = LayerMask.NameToLayer("HidePlayer");
    }

    void Update()
    {

	}

	void FixedUpdate()
	{
		input = Input.GetAxis("Horizontal");
		//if (Input.GetAxis("Horizontal") != 0)
		//{
		//	moveDirX = new Vector3(input, 0, 0).normalized;
		//	playerRigidbody.AddForce(Vector2.right * moveDirX, ForceMode2D.Impulse);
		//}
		
		moveDirX = new Vector3(input, 0, 0).normalized;
		//transform.position += moveDirX * speed * Time.deltaTime;
		playerRigidbody.AddForce(moveDirX, ForceMode2D.Impulse);
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			if (playerRigidbody.velocity.x > maxSpeed)
				playerRigidbody.velocity = new Vector2(maxSpeed, playerRigidbody.velocity.y);
			else if (playerRigidbody.velocity.x < maxSpeed * (-1))
				playerRigidbody.velocity = new Vector2(maxSpeed * (-1), playerRigidbody.velocity.y);
		}
		else
			playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);

		if (Input.GetKeyDown(KeyCode.Space))
			playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Monster"))
        {
                GameManager.GM.hpGauge -= 0.34f;
        }

        if (other.CompareTag("HelpObject"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Dust"))
		{
			gameObject.layer = hideLayer;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("HelpObject"))
			transform.GetChild(0).gameObject.SetActive(false);

		if (other.CompareTag("Dust"))
			gameObject.layer = defaultLayer;
	}
}
