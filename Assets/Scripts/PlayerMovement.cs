using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public float runSpeed = 30f;

	private CharacterController2D characterController;
	private PlayerController playerController;

	float horizontalMove = 0f;
	bool jump = false;
	bool action = false;

	void Start()
	{
		characterController = GetComponent<CharacterController2D>();
		playerController = GetComponent<PlayerController>();
	}

	void Update () 
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Action"))
		{
			action = true;
		}
	}

	void FixedUpdate ()
	{
		// Move our character
		characterController.Move(horizontalMove * Time.fixedDeltaTime, jump);

		if (action) {
			playerController.doAction();
		}

		jump = false;
		action = false;
	}
}