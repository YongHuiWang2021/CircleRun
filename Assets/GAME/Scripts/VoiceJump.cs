using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class VoiceJump : MonoBehaviour {

	public static VoiceJump _instance;

	[Header("JUMP")]
	public Slider   jumpSlider;
	[Range(1, 10)] public int multiply;
	public float 	jumpForce = 160f; //Force in Y Direction when jumping.
	public float    maxSpeedJ = 4f;  //Max Velocity when Jumping and Moving Forward..
	[Header("WALK")]
	public float 	walkForce = 4.4f; //Force in X Direction when walking.
	public float    startWalk = -60f;//Start Wakling DB
	public float 	maxSpeed  = 4f;  //Max Velocity when walking

	[Header("Dead Anim")]
	public SpriteRenderer eye = null;
	public Sprite   deadEyeSpr = null;
	private Sprite  eyeSprite  = null;

	[Header("SPLASH")]
	public GameObject splashPreFab;
	public ParticleSystem particleMelodie;
	private ParticleSystem.EmissionModule particleEM;


	private bool isGrounded = false;
	private Rigidbody2D rgb2D;
	private Animator	anim;
	private Vector2 startPos;
	private Collider2D	col2d;
	private int	showAdsCounter = 3;
	private float dbValJumpRange;

	[HideInInspector]
	public  bool isDead { get; set; }



	void Awake() {
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		
		isDead = true;
		rgb2D = GetComponent<Rigidbody2D> ();
		anim  = GetComponent<Animator> ();
		col2d = GetComponent<Collider2D> ();

		//Save for later..
		eyeSprite = eye.sprite;
		startPos  = transform.position; //Used to reset the Player to the Starting Position..
		particleEM = particleMelodie.emission;

		//Adds a listener to the main slider and invokes a method when the value changes.
		jumpSlider.onValueChanged.AddListener(delegate {JumpSliderValueChanged(); });
		dbValJumpRange = jumpSlider.value;
	}


	void FixedUpdate()
	{
		if (isDead)
			return;

		float dbVal = AudioVisualizer._instance.dbValue;


		//Jump only if Grounded..
		if ((dbVal >= dbValJumpRange && dbVal <= jumpSlider.maxValue) && isGrounded) { 
			rgb2D.AddForce (transform.up * (jumpForce * multiply));
		}
			
		//Walk..
		if (dbVal >= startWalk && dbVal <= -9f) { 
			//PLAY PARTCILE..
			particleEM.enabled = true;

			//WALK
			anim.SetFloat ("walk", 0.2f);
			rgb2D.AddForce (transform.right * walkForce);

		} else {
			
			//PLAY PARTCILE..
			particleEM.enabled = false;

			//Stop
			anim.SetFloat ("walk", 0f);
		}


		//Limit Speed to MaxSpeed
		if(rgb2D.velocity.magnitude > maxSpeed && isGrounded)
		{
			rgb2D.velocity = Vector2.ClampMagnitude (rgb2D.velocity, maxSpeed);
		}
		//Limit Speed to MaxSpeedJ when jumping..
		else if(rgb2D.velocity.magnitude > maxSpeedJ && !isGrounded)
		{
			rgb2D.velocity = Vector2.ClampMagnitude (rgb2D.velocity, maxSpeedJ);
		}


		//Debug.Log ("Velocity: " + rgb2D.velocity.magnitude );
	}

	//USed from outside ... Start Panel..
	public void RestartGame(){
		
		StartCoroutine (ResetPlayerFirstTime ());
	}

	//Used as value changed for the slider to set the range..
	public void JumpSliderValueChanged()
	{
		dbValJumpRange = jumpSlider.value;
	}

	IEnumerator ResetPlayerFirstTime()
	{
		yield return new WaitForSeconds(.5f);

		col2d.isTrigger = false;
		rgb2D.velocity = new Vector2 (0f, 0f);
		rgb2D.angularVelocity = 0f;
		rgb2D.isKinematic  = true; 
		transform.position = startPos;
		eye.sprite = eyeSprite;

		yield return new WaitForSeconds(.5f);
		rgb2D.isKinematic = false;

		GameManager._instance.Reset (); 

		yield return new WaitForSeconds(.5f);
		isDead = false;

		yield return null;
		Debug.Log ("VoiceJump.cs -> ResetPlayer");
	}

	IEnumerator ResetPlayer()
	{
		//Show ADs..
		showAdsCounter--;
		if (showAdsCounter <= 0) {
			showAdsCounter = Random.Range (2, 5); 
			GameManager._instance.ShowAd ();
		}


		yield return new WaitForSeconds(1f);

		col2d.isTrigger = false;
		rgb2D.velocity = new Vector2 (0f, 0f);
		rgb2D.angularVelocity = 0f;
		rgb2D.isKinematic  = true; 
		transform.position = startPos;
		eye.sprite = eyeSprite;

		yield return new WaitForSeconds(2f);
		rgb2D.isKinematic = false;

		GameManager._instance.Reset (); 

		yield return new WaitForSeconds(1f);
		isDead = false;

		yield return null;
		Debug.Log ("VoiceJump.cs -> ResetPlayer");
	}

	void OnCollisionEnter2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Ground") {
			isGrounded = true;
			anim.SetTrigger ("ground");
		}

	}


	void OnCollisionExit2D(Collision2D coll) {
		
		if (coll.gameObject.tag == "Ground") {
			isGrounded = false;
			anim.SetTrigger ("jump");
		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Enemy") {

			AudioManager._instance.PlayGameOver ();

			//PLAY PARTCILE..
			particleEM.enabled = false;

			GameManager._instance.GameOver = true;
			isDead = true;

			//PLayer is dead..
			eye.sprite = deadEyeSpr; // X - Sprite for the Eye..
			SimpleCameraShake._instance.StartShake();

			//Let the Player fall down..
			StartCoroutine (DeathPause ());
		}

		if (other.tag == "Water") {

			AudioManager._instance.PlayWaterSplash (); 

			//PLAY PARTCILE..
			particleEM.enabled = false;

			//Water Splash..
			GameObject splash = Instantiate (splashPreFab, transform.position, splashPreFab.transform.rotation);
			Destroy (splash, 2f);

			if( !isDead ) {
				GameManager._instance.GameOver = true;
				isDead = true;

				col2d.isTrigger = true;

				//PLayer is dead..
				eye.sprite = deadEyeSpr; // X - Sprite for the Eye..
				SimpleCameraShake._instance.StartShake();

				StartCoroutine (ResetPlayer ()); //REset the Player again..
			}
		}

		if (other.tag == "Score") {
			//AudioManager._instance.PlayRecrod ();
			GameManager._instance.score++;
		}
	}

	IEnumerator DeathPause(){

		rgb2D.isKinematic  = true; 
		rgb2D.velocity     = Vector2.zero;
		yield return new WaitForSeconds(1f);
		rgb2D.isKinematic = false;
		col2d.isTrigger = true;
		StartCoroutine (ResetPlayer ()); //REset the Player again..
		yield return null;

	}

}
