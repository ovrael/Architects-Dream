using System;
using System.Collections;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Player")]
	[SerializeField] Player player;

	[Header("Running")]
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	
	[Header("Jumping")]
	[SerializeField] private bool m_AirControl = true;                          // Whether or not a player can steer while jumping;
	private float m_JumpForce;					// Amount of force added when the player jumps.
	[SerializeField] private float jumpTime;
	private float fallMultiplier;

	[Header("GroundCheck")]
	[SerializeField] private Transform groundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private float groundedRadius = .75f; // Radius of the overlap circle to determine if grounded
	[SerializeField] private float changeDistance;
														 
	[Header("Particle System")]
	[SerializeField] ParticleSystem jumpParticle;
	[SerializeField] ParticleSystem landParticle;
	[SerializeField] ParticleSystem runningDustParticle;
	[SerializeField] ParticleSystem sprintingDustParticle;

	[Header("Slopes")]
	[SerializeField] float maxKat;
	[SerializeField] float czyMozeChodzicPoPochylni;
	[SerializeField] float dystansMierzeniaPochylni;
	[SerializeField] CircleCollider2D koliderDoPochylni;
	[SerializeField] Transform sprite;
	[SerializeField] Animator animacja;
	[SerializeField] PhysicsMaterial2D brakTarcia;                      // Material bez zadnego tarcia
	[SerializeField] PhysicsMaterial2D maxTarcie;                       // Material z duzym tarciem

	[Header("Sounds")]
	[SerializeField] AudioClip walkSound;
	[SerializeField] AudioClip jumpSound;
	[SerializeField] AudioClip landSound;

	private Vector2 prostopadlosc;
	private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;
	private AudioSource audioSource;
	
	float rozmiarKolidera;                  // Przechowuje rozmiar kolidera do mierzenia k¹ta podloza
	float katDolnegoPochylenia;             // Kat pochylenia powierzchni pod bohaterem
	float katDolnegoPochyleniaStary;        // Kat pochylenia (stary)
	float katBocznegoPochylenia;
	float jumpTimeCounter;
	float tmpGravityScale;
	float predkoscSprintu;

	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private bool isGrounded;
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool isJumping = false;
	private bool isSprinting;
	private bool czyNaPochylni;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void SprawdzaniePochylni()
	{
		Vector2 sprawdzPozycje = transform.position - new Vector3(0.0f, rozmiarKolidera / 2);
		PionoweSprawdzaniePochylni(sprawdzPozycje);
		PoziomeSprawdzaniePochylni(sprawdzPozycje);
	}

	private void PoziomeSprawdzaniePochylni(Vector2 sprawdzPozycje)
	{
		RaycastHit2D hitPrzod = Physics2D.Raycast(sprawdzPozycje, transform.right, dystansMierzeniaPochylni, m_WhatIsGround);
		RaycastHit2D hitTyl = Physics2D.Raycast(sprawdzPozycje, -transform.right, dystansMierzeniaPochylni, m_WhatIsGround);

		if (hitPrzod)
		{
			katBocznegoPochylenia = -Vector2.Angle(hitPrzod.normal, Vector2.up);
			if(Mathf.Abs(katBocznegoPochylenia) < 80)
				czyNaPochylni = true;
		}
		else if (hitTyl)
		{
			czyNaPochylni = true;
			katBocznegoPochylenia = Vector2.Angle(hitTyl.normal, Vector2.up);
			if (Mathf.Abs(katBocznegoPochylenia) < 80)
				czyNaPochylni = true;
		}
		else
		{
			if(Math.Abs(katDolnegoPochylenia) <= 1)
				czyNaPochylni = false;
			katBocznegoPochylenia = 0.0f;
		}

	}

	private void PionoweSprawdzaniePochylni(Vector2 sprawdzPozycje)
	{
		RaycastHit2D hit = Physics2D.Raycast(sprawdzPozycje, Vector2.down, dystansMierzeniaPochylni, m_WhatIsGround);
		if (hit)
		{
			prostopadlosc = Vector2.Perpendicular(hit.normal).normalized;
			katDolnegoPochylenia = Vector2.Angle(hit.normal, Vector2.up);

			if (Math.Abs(katDolnegoPochylenia) >= 1)
			{
				czyNaPochylni = true;
			}
			else
				czyNaPochylni = false;

			katDolnegoPochyleniaStary = katDolnegoPochylenia;

			Debug.DrawRay(hit.point, prostopadlosc, Color.red);
			Debug.DrawRay(hit.point, hit.normal, Color.green);
		}

	}

	private void ZmianaMaterialu(float ruch)
	{
		//Debug.Log("Dolny: " + katDolnegoPochylenia + "\tBoczny: " + katBocznegoPochylenia + "\tCzy na pochylni: " + czyNaPochylni);
		if(!isGrounded)
			m_Rigidbody2D.sharedMaterial = brakTarcia;
		else
		{
			if (czyNaPochylni && ruch == 0.0f)
				m_Rigidbody2D.sharedMaterial = maxTarcie;
			else
				m_Rigidbody2D.sharedMaterial = brakTarcia;
		}
	}

	private void ObrocPodKat()
	{
		if(Math.Abs(katBocznegoPochylenia) <= 44)
			sprite.transform.rotation = Quaternion.Euler(0f, 0f, -katBocznegoPochylenia);
		else if(Math.Abs(katBocznegoPochylenia) >= 75 && (Math.Abs(katDolnegoPochylenia) <= 1))
			sprite.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		else if(katBocznegoPochylenia > 44)
			sprite.transform.rotation = Quaternion.Euler(0f, 0f, -40f);
		else if(katBocznegoPochylenia < -44)
			sprite.transform.rotation = Quaternion.Euler(0f, 0f, 40f);
	}

	private void Awake()
	{
		predkoscSprintu = player.SprintModifier;
		fallMultiplier = player.FallMultiplier;
		m_JumpForce = player.JumpPower;

		audioSource = GetComponent<AudioSource>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		rozmiarKolidera = koliderDoPochylni.radius;
		tmpGravityScale = m_Rigidbody2D.gravityScale;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void Update()
	{
		predkoscSprintu = player.SprintModifier;
		fallMultiplier = player.FallMultiplier;
		m_JumpForce = player.JumpPower;
	}

	private void FixedUpdate()
	{
		if(isGrounded)
		{
			animacja.SetBool("czySpada", false);
			animacja.SetBool("czyWyladowal", false);
		}

		EvenBetterGroundCheck();
		SprawdzaniePochylni();
		ObrocPodKat();
		ParticleSystemManagement();
		BetterFalling();

		//Debug.Log("Boczny: " + katBocznegoPochylenia + "\tDolny: " + katDolnegoPochylenia);

	}

	private void ParticleSystemManagement()
	{
		if (isGrounded && isSprinting && (m_Rigidbody2D.velocity.x > 1 || m_Rigidbody2D.velocity.x < -1))
			sprintingDustParticle.Play();
		else if (isGrounded && !isSprinting && (m_Rigidbody2D.velocity.x > 1 || m_Rigidbody2D.velocity.x < -1))
			runningDustParticle.Play();
	}

	private void BetterFalling()
	{
		if (m_Rigidbody2D.velocity.y >= -1.4f) //is falling
		{
			if(!isGrounded)
				animacja.SetBool("czySkacze", true);
			animacja.SetBool("czySpada", false);
			m_Rigidbody2D.gravityScale = tmpGravityScale;
		}
		else
		{
			animacja.SetBool("czyWyladowal", false);
			animacja.SetBool("czySkacze", false);
			animacja.SetBool("czySpada", true);
			m_Rigidbody2D.sharedMaterial = brakTarcia;
			m_Rigidbody2D.gravityScale = fallMultiplier;
		}
	}

	//Nieuzywane GroundChecki
	//private void GroundCheck()
	//{
	//	bool wasGrounded = isGrounded;
	//	isGrounded = false;

	//	// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
	//	// This can be done using layers instead but Sample Assets will not overwrite your project settings.
	//	Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, m_WhatIsGround);
	//	for (int i = 0; i < colliders.Length; i++)
	//	{
	//		if (colliders[i].gameObject != gameObject)
	//		{
	//			isGrounded = true;
	//			if (!wasGrounded)
	//			{
	//				OnLandEvent.Invoke();
	//				Debug.Log("UZIEMIONY");
	//				isJumping = false;
	//				animacja.SetBool("czyUziemiony", true);
	//				animacja.SetBool("czyWyladowal", true);
	//			}
	//		}
	//		else
	//			animacja.SetBool("czyUziemiony", false);
	//	}
	//}

	//private void BetterGroundCheck()
	//{
	//	float extraHeightText = 0.5f;
	//	RaycastHit2D raycastHit = Physics2D.BoxCast(groundCheckCollider.bounds.center, groundCheckCollider.bounds.size, 0f, Vector2.down, extraHeightText, m_WhatIsGround);
	//	isGrounded = raycastHit.collider != null;
	//	if(isGrounded)
	//	{
	//		OnLandEvent.Invoke();
	//		Debug.Log("UZIEMIONY");
	//		isJumping = false;
	//		animacja.SetBool("czyUziemiony", true);
	//		animacja.SetBool("czyWyladowal", true);
	//	}
	//	else
	//	{
	//		animacja.SetBool("czyUziemiony", false);
	//	}
	//}
	
	private void EvenBetterGroundCheck()
	{
		bool wasGrouned = isGrounded;
		isGrounded = false;
		
		if(Physics2D.OverlapCircle(groundCheck.position, groundedRadius, m_WhatIsGround))
		{
			isGrounded = true;
			if(!wasGrouned)
			{
				PlaySound(landSound, 1f);

				OnLandEvent.Invoke();
				animacja.SetBool("czyWyladowal", true);
				landParticle.Play();
				isJumping = false;
			}
		}


		if (isGrounded)
		{
			OnLandEvent.Invoke();
			isJumping = false;
			animacja.SetBool("czyUziemiony", true);
			animacja.SetBool("czyWyladowal", true);
		}
		else
		{
			animacja.SetBool("czyUziemiony", false);
		}

	}

	public void LewoPrawo(float move, bool czySprintuje)
	{
		ZmianaMaterialu(move);

		float randomPitch;
		if (czySprintuje)
		{
			randomPitch = UnityEngine.Random.Range(0.75f, 0.8f);
			move *= predkoscSprintu;
		}
		else
		{
			randomPitch = UnityEngine.Random.Range(0.4f, 0.5f);
		}

		if (isGrounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector3();

			if (czyNaPochylni && !isJumping)
			{
				if(isGrounded)
				{
					targetVelocity = new Vector2(-move * 10f * Time.deltaTime * prostopadlosc.x, -move * 10f * Time.deltaTime * prostopadlosc.y);
				}
				else
					targetVelocity = new Vector2(move * 10f * Time.fixedDeltaTime, m_Rigidbody2D.velocity.y);
			}
			else if(!czyNaPochylni)
			{
				//if(!audioSource.isPlaying && Mathf.Abs(move) > 0)
				//	audioSource.Play();
				targetVelocity = new Vector2(move * 10f * Time.fixedDeltaTime, m_Rigidbody2D.velocity.y);
			}
			else if (czyNaPochylni && isJumping)
			{
				targetVelocity = new Vector2(move * 10f * Time.fixedDeltaTime, m_Rigidbody2D.velocity.y);
			}

			if (Mathf.Abs(move) > 0 && isGrounded && !audioSource.isPlaying)
				PlaySound(walkSound, randomPitch);

			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
				Flip();
			else if (move< 0 && m_FacingRight)
				Flip();
		}
	}

	public void Skok(bool skok, bool dalejSkacze, ref bool przerwano)
	{
		if (isGrounded)
			przerwano = false;

		if (isGrounded && skok)
		{
			PlaySound(jumpSound, 1f);
			StartCoroutine("ChangeGroundDetectionPoint");
			jumpParticle.Play();
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
			sprite.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			isGrounded = false;
			isJumping = true;
			jumpTimeCounter = jumpTime;
			animacja.SetBool("czyWyladowal", false);
		}

		if (isJumping && dalejSkacze && !przerwano)
		{ 
			if (jumpTimeCounter > 0)
			{
				m_Rigidbody2D.AddForce(new Vector2(0f, 10 * jumpTimeCounter), ForceMode2D.Impulse);
				jumpTimeCounter -= Time.deltaTime;
			}
			else
				isJumping = false;
		}
	}

	IEnumerator ChangeGroundDetectionPoint()
	{
		groundCheck.position = new Vector3(groundCheck.position.x, groundCheck.position.y + changeDistance);
		yield return new WaitForSeconds(0.1f);
		groundCheck.position = new Vector3(groundCheck.position.x, groundCheck.position.y - changeDistance);
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = sprite.localScale;
		theScale.x *= -1;
		runningDustParticle.transform.localScale = theScale;
		sprite.localScale = theScale;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
	}

	private void PlaySound(AudioClip clip, float pitchValue)
	{
		audioSource.clip = clip;
		audioSource.pitch = pitchValue;
		audioSource.Play();
	}

}
