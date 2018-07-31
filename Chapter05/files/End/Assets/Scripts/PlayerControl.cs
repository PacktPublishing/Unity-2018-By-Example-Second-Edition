//--------------------------------
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
//--------------------------------
public class PlayerControl : MonoBehaviour
{
	//--------------------------------
	public enum FACEDIRECTION {FACELEFT = -1, FACERIGHT = 1};
	//Which direction is the player facing - left or right?
	public FACEDIRECTION Facing = FACEDIRECTION.FACERIGHT;
	//Which objects are tagged as ground
	public LayerMask GroundLayer;
	//Reference to rigidbody
	private Rigidbody2D ThisBody = null;
	//Reference to transform
	private Transform ThisTransform = null;
	//Reference to feet collider
	public CircleCollider2D FeetCollider = null;
	//Are we touching the ground?
	public bool isGrounded = false;
	//What are the main input axes
	public string HorzAxis = "Horizontal";
	public string JumpButton = "Jump";
	//Speed variables
	public float MaxSpeed = 50f;
	public float JumpPower = 600;
	public float JumpTimeOut = 1f;
	//Can we jump right now?
	private bool CanJump = true;
	//Can we control player?
	public bool CanControl = true;
	public static PlayerControl PlayerInstance = null;
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

		//Set static instance
		PlayerInstance = this;
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
		Destroy(PlayerControl.PlayerInstance.gameObject);
	}
	//--------------------------------
	//Resets player back to defaults
	public static void Reset()
	{
		Health = 100f;
	}
	//--------------------------------
}
//--------------------------------