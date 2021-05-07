using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	//General
	[Header("General")]
	private Animator animator;
	private Rigidbody2D rigid;
	private SpriteRenderer spriteRenderer;
    private static GameManager manager;
    [SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private GameObject hitbox;

	//Basic Movement
	[Header("Basic Movement")]
	public float hMove = 1;
	public float runSpeed = 1.25f;
    public bool canJump = true;//disable jumping 

	//Jump
	[Header("Jump")]
	public float jumpforce = 7;
	private bool jump = false;
	private float jumpTimeCounter;
	[SerializeField] private float jumpTime;
	private bool isJumping;
	private bool isGrounded = false;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	private bool canDoubleJump;
	[SerializeField] private Transform jumpFX;

	//Wall jump
	[Header("Wall Jump")]
	public Transform frontCheck;
	private bool isTouchingWall;
	private bool isSliding;
	public float slideSpeed;
	public float checkRadius;
	private bool canWallJump;
	private bool isWallJumping;
	public float xWallForce;
	public float yWallForce;
	public float wallJumpTime;
	[SerializeField] private Transform wallJumpFX;

	//Crouch
	private bool crouch = false;
	//private bool isCrouching = false;
	private float crouchSpeed = 0.75f;

	//Dash
	[Header("Dash")]
	[SerializeField] public float dashDistance = 15f;
	//[SerializeField] public float dashForce;
	private bool isDashing = false;
	public int direction = -1;
	[SerializeField] private bool canDash;

	public RoomManager roomManager;

    //DropDown
    CircleCollider2D feetCollider;

	//Interact
	[Header("Interact")]
	[SerializeField]
    [Tooltip("Mask to hit interactable items")]
    LayerMask interactableMask;
    BoxCollider2D bodyCollider;

	//Unlocker
	[Header("Progression Tracker")]
	public ProgressionTracker tracker;

    // Start is called before the first frame update
    void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
        manager = GameManager.instance;
        feetCollider = GetComponentInChildren<CircleCollider2D>();
        bodyCollider = GetComponentInChildren<BoxCollider2D>();
    }


	private void Update()
	{
		Jump();
		WallJump();
		OnLand();
		Crouch();
		OnCrouch();
		Dash();

        if (Input.GetKey("s")) //change to getbuttondown
            TestTile();
        if (Input.GetButtonDown("Interact")) 
            IsInteractable();       
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

		if (isGrounded == true)
		{
			canDoubleJump = true;
			animator.SetBool("IsJumping", false);
			animator.SetBool("HasLanded", true);
		}

		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded == true) // Do one jump if grounded
			{
				BaseJump();
				animator.SetBool("IsJumping", true);
				animator.SetBool("HasLanded", false);
			}
			else
			{
				if (canDoubleJump == true && isSliding == false && tracker.unlockDoubleJump == true) // Do a second jump if not grounded
				{
					BaseJump();
					animator.SetBool("IsJumping", true);
					animator.SetBool("HasLanded", false);
					canDoubleJump = false;
					Instantiate(jumpFX, transform.position, Quaternion.identity);
				}
			}
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
				jumpTimeCounter -= Time.deltaTime;
			}
			else
			{
				isJumping = false;
			}
		}
	}

	void WallJump()
	{
		if(tracker.unlockWallJump == true)
		{
			isTouchingWall = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);

			if (isTouchingWall == true && isGrounded == false && hMove != 0)
			{
				isSliding = true;
			}
			else
			{
				isSliding = false;
			}

			if (isSliding == true)
			{
				rigid.velocity = new Vector2(rigid.velocity.x, Mathf.Clamp(rigid.velocity.y, -slideSpeed, float.MaxValue));
			}

			if (Input.GetButtonDown("Jump") && isSliding == true && canWallJump == true)
			{
				isWallJumping = true;
				canWallJump = false;
				Invoke("WallJumpReset", wallJumpTime);
				Instantiate(wallJumpFX, transform.position, Quaternion.identity);
			}

			if (isWallJumping == true)
			{
				rigid.velocity = new Vector2(rigid.velocity.x, yWallForce);
			}
		}
	}

	void WallJumpReset()
	{
		isWallJumping = false;
	}

	void BaseJump()
	{
		isJumping = true;
		jumpTimeCounter = jumpTime;
		rigid.velocity = Vector2.up * jumpforce;
	}

	public void JumpRefresh()
	{
		canDoubleJump = true;
		canWallJump = true;
		BaseJump();
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
			canWallJump = true;
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
		if (tracker.unlockDash == true)
		{
			if (canDash == true)
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

	public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
	{

		float timer = 0;
		while (knockDur > timer)
		{

			timer += Time.deltaTime;

			rigid.AddForce(new Vector3(direction * -75, knockbackDir.y * knockbackPwr, transform.position.z));

		}
		yield return 0;
	}

	private void DropDown()
	{
		rigid.velocity = Vector2.down * jumpforce * 1.5f;
		this.canJump = false;
		animator.SetBool("IsJumping", true);
		animator.SetBool("HasLanded", false);
	}

    //tests the tile for specialty
    void TestTile() {                
        RaycastHit2D hit = Physics2D.Raycast(feetCollider.bounds.center, Vector2.down, feetCollider.bounds.extents.y + 0.1f, groundLayer);

        /***** Debugging ****/
        Debug.DrawRay(feetCollider.bounds.center, Vector2.down * (feetCollider.bounds.extents.y + 0.1f), Color.green);
        /***** Debugging ****/

        if (hit.collider != null) {
            try {
                SpecialTile st = hit.collider.GetComponent<SpecialTile>();
                st.ActivateSpecial();
                DropDown();
            }catch {
                
            }
        }
    }

    //interaction
    void IsInteractable() {
        if (manager.isPaused) return;
        RaycastHit2D hit;
        if (hMove < 0) { //player looking left
            hit = Physics2D.Raycast(bodyCollider.bounds.center, Vector2.left, 5f, interactableMask);
            Debug.DrawRay(bodyCollider.bounds.center, Vector2.left * 5f, Color.red);
        } else { //player is looking right
            hit = Physics2D.Raycast(bodyCollider.bounds.center, Vector2.right, 5f, interactableMask);
            Debug.DrawRay(bodyCollider.bounds.center, Vector2.right * 5f, Color.red);
        }
        Interactable obj;
        if (hit.collider != null) {
            try {
                obj = hit.collider.GetComponent<Interactable>();
                Debug.Log(obj.CanInteract);
                if (obj.CanInteract)
                    obj.OnInteract();
                else
                    Debug.Log("Interactable but cant interact right now");
            }
            catch {
                Debug.Log("Not interactable");
                return;
            }
           
        }
    }
}
