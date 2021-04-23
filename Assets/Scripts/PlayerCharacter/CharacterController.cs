using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	//General
	private Animator animator;
	private Rigidbody2D rigid;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private GameObject hitbox;

	//Basic Movement
	public float hMove = 1;
	public float runSpeed = 1.25f;
    public bool canJump = true;//disable jumping 

	//Jump
	private bool jump = false;
	[SerializeField] public float jumpforce = 500;
	private float jumpTimeCounter;
	[SerializeField] private float jumpTime;
	private bool isJumping;
	private bool isGrounded = false;

	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	//Crouch
	bool crouch = false;
	private bool isCrouching = false;
	private float crouchSpeed = 0.75f;

	//Dash
	[SerializeField] private float dashDistance = 15f;
	[SerializeField] private float dashForce;
	private bool isDashing = false;
	public int direction = -1;
	[SerializeField] private bool canDash;
	

	// Start is called before the first frame update
	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		Jump();
		OnLand();
		Crouch();
		OnCrouch();
		Dash();
	}

	void FixedUpdate()
	{
		Walk();
		GravityHandler();
	}

	public void Walk()
	{
		if (!crouch) //Normal WalkSpeed
		{
			hMove = Input.GetAxisRaw("Horizontal") * runSpeed;
			animator.SetFloat("Speed", Mathf.Abs(hMove));

			if (!isDashing)
			{
				rigid.velocity = new Vector2(hMove * runSpeed, rigid.velocity.y);
			}

		}
		else//Crouch speed
		{
			hMove = Input.GetAxisRaw("Horizontal") * (runSpeed * crouchSpeed);
			animator.SetFloat("Speed", Mathf.Abs(hMove));
			rigid.velocity = new Vector2(hMove * runSpeed, rigid.velocity.y);
		}
		if (hMove > 0) //Flip sprite depending on the direction the player is moving
		{
			//Moving right
			spriteRenderer.flipX = true;
			direction = 1;
		}
		if (hMove < 0)
		{
			//Moving left
			spriteRenderer.flipX = false;
			direction = -1;
		}
	}

	void Jump()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);

		if (isGrounded == true && Input.GetButtonDown("Jump"))
		{
			isJumping = true;
			jumpTimeCounter = jumpTime;
			rigid.velocity = Vector2.up * jumpforce;
			animator.SetBool("IsJumping", true);
			animator.SetBool("HasLanded", false);
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			isJumping = false;
		}
		if (Input.GetButton("Jump") && isJumping == true)
		{
			if (jumpTimeCounter > 0 && isJumping == true)
			{
				rigid.velocity = Vector2.up * jumpforce;
				//rigid.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
				jumpTimeCounter -= Time.deltaTime;
			}
			else
			{
				isJumping = false;
			}
		}
	}

	void GravityHandler()
	{
		if(rigid.velocity.y < 0)
		{
			rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if(rigid.velocity.y > 0 && !Input.GetButtonDown("Jump"))
		{
			rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	public void OnLand()
	{
		if (isGrounded && isJumping == false)
		{
            this.canJump = true;
            animator.SetBool("IsJumping", false);
			animator.SetBool("HasLanded", true);
		}
	}

	void Crouch()
	{
		if (Input.GetButton("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
	}

	public void OnCrouch()
	{
		if (crouch)
		{
			animator.SetBool("IsCrouching", true);
		}
		else
		{
			animator.SetBool("IsCrouching", false);
		}
	}

	public void Dash()
	{
		if(canDash == true)
		{
			if (Input.GetButtonDown("Dash") && isDashing == false)
			{
				StartCoroutine(DashCo());
				canDash = false;
				hitbox.SetActive(false);
			}
		}
		if (isGrounded)
		{
			canDash = true;
		}
	}

	IEnumerator DashCo()
	{
		isDashing = true;
		rigid.velocity = new Vector2(rigid.velocity.x, 0f);
		if(hMove == 0)
		{
			rigid.AddForce(new Vector2(dashDistance * direction * 2.5f, 0f), ForceMode2D.Impulse);
		}
		else
		{
			rigid.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
		}
		
		float gravity = rigid.gravityScale;
		rigid.gravityScale = 0;

		yield return new WaitForSeconds(0.4f);

		hitbox.SetActive(true);
		isDashing = false;
		rigid.gravityScale = 1;
	}

	public void DropDown()
	{
		rigid.velocity = Vector2.down * jumpforce * 1.5f;
		this.canJump = false;
		animator.SetBool("IsJumping", true);
		animator.SetBool("HasLanded", false);
	}
}
