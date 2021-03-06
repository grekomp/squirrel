﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	// Status
	public bool grounded;
	public bool sliding;

	// Jumping
	public int jumpCharges = 0;
	bool isJumping = false;
	bool jumpReleased = false;
	float minGroundNormalY = 0.5f;
	float minWallNormalX = 0.9f;
	float defaultGravityScale;

	// Wall sliding
	float slidingDirection;
	public float slidingSpeed = 1.0f;
	public float slideReleaseDelay = 0.1f;
	public float slideReleaseBuildup;

	// Presets
	public PlayerPreset currentPreset;
	public PlayerPreset defaultPreset;
	public PlayerPreset coffeePreset;

	// Coffee mode
	public float coffeeModeDuration = 5.0f;
	float coffeeModeDurationRemaining;

	//Dying
	public bool dead;
	public float deathDelay = 2.0f;
	public float deathKnockBack = 15.0f;

	// Misc
	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	Animator animator;
	ContactPoint2D[] contacts = new ContactPoint2D[16];
	int numContacts;
	bool levelEnding = false;

	// Sounds
	public AudioClip[] jumpSounds;
	public AudioClip[] deathSounds;
	public AudioClip[] doubleJumpSounds;
	public AudioClip[] endLevelSounds;
	AudioSource audioSource;

	void Start () {
		currentPreset = defaultPreset;
		jumpCharges = currentPreset.jumpCharges;

		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();

		defaultGravityScale = rb.gravityScale;

		audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate()
	{
		if (!GameController.paused)
		{
			if (!dead)
			{

				// Coffee mode countdown
				if (coffeeModeDurationRemaining > 0)
				{
					coffeeModeDurationRemaining -= Time.fixedDeltaTime;

					if (coffeeModeDurationRemaining <= 0)
					{
						TriggerNormalMode();
					}
				}
			}

			// Reset grounded status
			grounded = false;

			// Check if player is grounded
			numContacts = rb.GetContacts(contacts);

			for (int i = 0; i < numContacts; i++)
			{
				if (contacts[i].normal.y > minGroundNormalY)
				{
					grounded = true;
					ResetJumpCharges();
				}
			}


			Moving();
			Jumping();
			WallSliding();

			// Update animation
			animator.SetBool("grounded", grounded);
			animator.SetBool("sliding", sliding);
			animator.SetBool("movingX", Mathf.Abs(rb.velocity.x) > 0.01f);
			animator.SetFloat("velocityY", rb.velocity.y);
		}
	}

	// Horizontal movement
	void Moving()
	{
		// Read input
		float moveX = Input.GetAxisRaw("Horizontal");

		// If there is input, increase velocity
		if (moveX != 0.0f && !dead)
		{
			// Increasing velocity
			rb.velocity += new Vector2(moveX * currentPreset.moveSpeed * Time.fixedDeltaTime * currentPreset.acceleration, 0.0f);

			// Cap velocity to moveSpeed
			if (rb.velocity.x > currentPreset.moveSpeed || rb.velocity.x < -currentPreset.moveSpeed)
			{
				rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -currentPreset.moveSpeed, currentPreset.moveSpeed), rb.velocity.y);
			}
		} else if (grounded)
		{
			// If there is no input, slow down and stop
			if (Mathf.Abs(rb.velocity.x) < 0.5f)
			{
				rb.velocity = new Vector2(0.0f, rb.velocity.y);
			} else if (rb.velocity.x > 0)
			{
				rb.velocity = new Vector2(rb.velocity.x - currentPreset.moveSpeed * Time.fixedDeltaTime * currentPreset.breaking, rb.velocity.y);

				if (rb.velocity.x < 0) rb.velocity = new Vector2(0.0f, rb.velocity.y); 
			} else if (rb.velocity.x < 0)
			{
				rb.velocity = new Vector2(rb.velocity.x + currentPreset.moveSpeed * Time.fixedDeltaTime * currentPreset.breaking, rb.velocity.y);
				if (rb.velocity.x > 0) rb.velocity = new Vector2(0.0f, rb.velocity.y);
			}

		} else
		{
			// If there is no input, slow down and stop
			if (Mathf.Abs(rb.velocity.x) < 0.5f)
			{
				rb.velocity = new Vector2(0.0f, rb.velocity.y);
			}
			else if (rb.velocity.x > 0)
			{
				rb.velocity = new Vector2(rb.velocity.x - currentPreset.moveSpeed * Time.fixedDeltaTime * currentPreset.airBreaking, rb.velocity.y);
				if (rb.velocity.x < 0) rb.velocity = new Vector2(0.0f, rb.velocity.y);
			}
			else if (rb.velocity.x < 0)
			{
				rb.velocity = new Vector2(rb.velocity.x + currentPreset.moveSpeed * Time.fixedDeltaTime * currentPreset.airBreaking, rb.velocity.y);
				if (rb.velocity.x > 0) rb.velocity = new Vector2(0.0f, rb.velocity.y);
			}
		}

		// Flip sprite if moving right
		if (Mathf.Abs(rb.velocity.x) > 0.5f)
		{
			spriteRenderer.flipX = rb.velocity.x > 0;
		}

	}

	// Jumping from ground
	void Jumping()
	{
		// Read input
		bool jump = Input.GetAxisRaw("Jump") > 0;

		// 
		if (!jump)
		{
			jumpReleased = true;
		}

		// If jump is pressed and player is on ground, add velocity
		if (!isJumping && !sliding && !dead && jump && jumpReleased && (jumpCharges > 0 || grounded))
		{
			rb.velocity = new Vector2(rb.velocity.x, currentPreset.jumpHeight);
			isJumping = true;
			if (!grounded)
			{
				jumpCharges--;
				PlayDoubleJumpSound();
			}
			else
			{
				PlayJumpSound();
			}

			if (jumpCharges < 0) jumpCharges = 0;
			jumpReleased = false;

			// Update animation
			animator.SetTrigger("jump");
		}
		else
		{
			if (jump && isJumping && !dead)
			{
				if (rb.velocity.y > 0)
				{
					rb.gravityScale = currentPreset.jumpingGravityScale;
				}
				else
				{
					rb.gravityScale = defaultGravityScale;
					isJumping = false;
				}
			}
			else
			{
				rb.gravityScale = defaultGravityScale;
				isJumping = false;
			}
		}
	}

	// Wall Sliding and wall jumping
	void WallSliding()
	{
		// If player is on the ground, stop sliding
		if (grounded || dead)
		{
			sliding = false;
		}
		else
		{
			// Check if player is touching a wall
			for (int i = 0; i < numContacts; i++)
			{
				if (Mathf.Abs(contacts[i].normal.x) > minWallNormalX)
				{
					sliding = true;
					slidingDirection = contacts[i].normal.x;
				}
			}

			if (numContacts == 0)
			{
				sliding = false;
			}

			if (sliding)
			{
				// If player is moving downwards
				if (rb.velocity.y < 0)
				{
					// Slowly slide down the wall
					rb.velocity = new Vector2(-(float)slidingDirection, -slidingSpeed);

					// Stop sliding after hitting the jump button
					if (Input.GetAxisRaw("Jump") > 0 && jumpReleased)
					{
						rb.velocity = new Vector2(slidingDirection * currentPreset.moveSpeed, currentPreset.jumpHeight);
						isJumping = true;
						jumpReleased = false;
						sliding = false;

						// Update animation
						animator.SetTrigger("jump");

						PlayJumpSound();
					}

					// Delay releasing the wall after pressing horizontal movement buttons, to give time for jumping
					if (Input.GetAxisRaw("Horizontal") > 0 && slidingDirection > 0)
					{
						slideReleaseBuildup += Time.fixedDeltaTime;
					}
					else if (Input.GetAxisRaw("Horizontal") < 0 && slidingDirection < 0)
					{
						slideReleaseBuildup += Time.fixedDeltaTime;
					}
					else
					{
						slideReleaseBuildup = 0.0f;
					}

					if (slideReleaseBuildup > slideReleaseDelay)
					{
						sliding = false;
						rb.velocity = new Vector2(slidingDirection, rb.velocity.y);
					}
				}
			}
		}
	}

	// Triggering coffee mode
	public void TriggerCoffeeMode()
	{
		currentPreset = coffeePreset;
		ResetJumpCharges();
		GameController.instance.CoffeeMode(true);

		coffeeModeDurationRemaining = coffeeModeDuration;
	}

	// Triggering normal mode
	public void TriggerNormalMode()
	{
		currentPreset = defaultPreset;
		ResetJumpCharges();
		GameController.instance.CoffeeMode(false);

		coffeeModeDurationRemaining = 0.0f;
	}

	void ResetJumpCharges()
	{
		jumpCharges = currentPreset.jumpCharges;
	}

	public void Death(Vector2 knockbackDirection)
	{
		if (!dead)
		{
			PlayDeathSound();
			GameController.instance.deathCount++;
		}
		dead = true;
		animator.SetBool("dead", dead);

		Vector2 knockback = knockbackDirection.normalized;
		if (knockback.x > 0)
		{
			knockback = new Vector2(deathKnockBack, deathKnockBack);
		} else
		{
			knockback = new Vector2(-deathKnockBack, deathKnockBack);
		}

		rb.velocity = knockback;

		Invoke("RestartLevel", deathDelay);
	}

	public void EndLevel()
	{
		if (!dead && !levelEnding)
		{
			levelEnding = true;
			PlayEndLevelSound();
			GameController.instance.EndLevel();
		}
	}

	void RestartLevel()
	{
		GameObject.Find("Level Controller").GetComponent<LevelController>().RestartLevel();
	}

	void PlayJumpSound()
	{
		audioSource.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
		audioSource.Play();
	}

	void PlayDoubleJumpSound()
	{
		audioSource.clip = doubleJumpSounds[Random.Range(0, doubleJumpSounds.Length)];
		audioSource.Play();
	}

	void PlayEndLevelSound()
	{
		audioSource.clip = endLevelSounds[Random.Range(0, endLevelSounds.Length)];
		audioSource.Play();
	}

	void PlayDeathSound()
	{
		audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
		audioSource.Play();
	}
}