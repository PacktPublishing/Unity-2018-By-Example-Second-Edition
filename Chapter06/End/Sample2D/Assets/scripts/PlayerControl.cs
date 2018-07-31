//--------------------------------
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
//--------------------------------
public class PlayerControl : MonoBehaviour
{
	//--------------------------------
	public enum FACEDIRECTION {FACELEFT = -1, FACERIGHT = 1};
	public FACEDIRECTION Facing = FACEDIRECTION.FACERIGHT;
	public LayerMask GroundLayer;
	private Rigidbody2D ThisBody = null;
	private Transform ThisTransform = null;
	public CircleCollider2D FeetCollider = null;
	public bool isGrounded = false;
	public string HorzAxis = "Horizontal";
	public string JumpButton = "Jump";
	public float MaxSpeed = 50f;
	public float JumpPower = 600;
	public float JumpTimeOut = 1f;
	private bool CanJump = true;
	private Animator ThisAnimator = null;
	private int MotionVal = Animator.StringToHash("Motion");
	public bool CanControl = true;
	public static PlayerControl PlayerInstance = null;
	public GameObject DeathParticles = null;
	//--------------------------------
	public static float Health
	{
		get
		{
			return _Health;
		}

		set
		{
			_Health = value;

			//If we are dead, then end game
			if(_Health <= 0)
			{
				Die();
			}
		}
	}

	[SerializeField]
	private static float _Health = 100f;
	//--------------------------------
	// Use this for initialization
	void Awake ()
	{
		//Get transform and rigid body
		ThisBody = GetComponent<Rigidbody2D>();
		ThisTransform = GetComponent<Transform>();

		//Get Animator
		ThisAnimator = GetComponent<Animator>();

		//Set static instance
		PlayerInstance = this;
	}
	//--------------------------------
	void Start()
	{
		//Level begins. Set starting position
		ThisTransform.position = SceneChanger.LastTarget;
	}
	//--------------------------------
	//Returns bool - is player on ground?
	private bool GetGrounded()
	{
		//Check ground
		Vector2 CircleCenter = new Vector2(ThisTransform.position.x, ThisTransform.position.y) + FeetCollider.offset;
		Collider2D[] HitColliders = Physics2D.OverlapCircleAll(CircleCenter, FeetCollider.radius, GroundLayer);
		if(HitColliders.Length > 0) return true;
		return false;
	}
	//--------------------------------
	//Flips character direction
	private void FlipDirection()
	{
		Facing = (FACEDIRECTION) ((int)Facing * -1f);
		Vector3 LocalScale = ThisTransform.localScale;
		LocalScale.x *= -1f;
		ThisTransform.localScale = LocalScale;
	}
	//--------------------------------
	//Engage jump
	private void Jump()
	{
		//If we are grounded, then jump
		if(!isGrounded || !CanJump)return;

		//Jump
		ThisBody.AddForce(Vector2.up * JumpPower);
		CanJump = false;
		Invoke ("ActivateJump", JumpTimeOut);
	}
	//--------------------------------
	//Activates can jump variable after jump timeout
	//Prevents double-jumps
	private void ActivateJump()
	{
		CanJump = true;
	}
	//--------------------------------
	// Update is called once per frame
	void FixedUpdate ()
	{
		//If we cannot control character, then exit
		if(!CanControl || Health <= 0f)
		{
			//Update motion Animation
			ThisAnimator.SetFloat(MotionVal, 0f, 0.1f, Time.deltaTime);
			return;
		}

		//Update grounded status
		isGrounded = GetGrounded();
		float Horz = CrossPlatformInputManager.GetAxis(HorzAxis);
		ThisBody.AddForce(Vector2.right * Horz * MaxSpeed);

		if(CrossPlatformInputManager.GetButton(JumpButton))
			Jump();
		
		//Clamp velocity
		ThisBody.velocity = new Vector2(Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed), 
		                                Mathf.Clamp(ThisBody.velocity.y, -Mathf.Infinity, JumpPower));
	
		//Flip direction if required
		if((Horz < 0f && Facing != FACEDIRECTION.FACELEFT) || (Horz > 0f && Facing != FACEDIRECTION.FACERIGHT))
			FlipDirection();

		//Update motion Animation
		ThisAnimator.SetFloat(MotionVal, Mathf.Abs(Horz), 0.1f, Time.deltaTime);
	}
	//--------------------------------
	void OnDestroy()
	{
		PlayerInstance = null;
	}
	//--------------------------------
	//Function to kill player
	static void Die()
	{
		//Spawn particle system for death
		if(PlayerControl.PlayerInstance.DeathParticles != null)
			Instantiate(PlayerControl.PlayerInstance.DeathParticles, PlayerControl.PlayerInstance.ThisTransform.position, PlayerControl.PlayerInstance.ThisTransform.rotation);

		Destroy(PlayerControl.PlayerInstance.gameObject);
	}
	//--------------------------------
	//Resets player back to defaults
	public static void Reset()
	{
		Health = 100f;
		//Set to default position
		SceneChanger.LastTarget = new Vector3(1.55f,-1.63f,0f);
	}
	//--------------------------------
}
//--------------------------------