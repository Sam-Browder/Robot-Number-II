using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICharacter {
	
	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float currentSpeed;
	public float jumpPower = 200f;
	public float climbPower = 5;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool canClimb;
	
	public int direction = 1;
	
	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	private CharacterAbility playerAbility;
	private CharacterDefense playerDefense;

	private ArrayList effects = new ArrayList();
	
	
	// Use this for initialization
	void Start () {
		this.canClimb = false;
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		this.playerAbility = gameObject.GetComponentInChildren<CharacterAbility> ();
		this.playerDefense = gameObject.GetComponentInChildren<CharacterDefense> ();
		IAbility projectile = new BasicAttack ("Projectile",10f,10f,10f,10f);
		IEffect cripple = new CrippleEffect (1f,20f,0f);
		projectile.AddEffect (cripple);
		this.playerAbility.SetAbility(projectile,0);
		this.playerAbility.SetAbility(projectile,1);
		this.currentSpeed = speed;
	}
	
	// Update is called once per frame
	//public Transform testProj;
	void Update () {
		Grounded ();
		Rope ();

		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x));
		
		if (this.playerDefense.GetHealth() <= 0.0) {
			Die ();
		}
		
		UpdateSprite ();
		if (Input.GetButtonDown ("Jump")){
			Jump();		
		}
		if (Input.GetButtonDown ("Fire1")) {
			//this.projspawner.ShootProjOne();
			this.playerAbility.ExecuteAbility(0);
		}
		if (Input.GetButtonDown ("Fire2")) {
			//this.projspawner.ShootProjTwo();
			this.playerAbility.ExecuteAbility(1);
		}

		EffectExpiration ();

	}
	
	// do all physics in here
	void FixedUpdate()
	{
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.80f;
		//Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Ground"), false);// && ((Input.GetAxis("Vertical") > 0.1f) || (Input.GetAxis("Vertical") < -0.1f)) );
		//fake friction
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}
		
		//moves player
		float h = Input.GetAxis ("Horizontal");
		rb2d.AddForce ((Vector2.right * this.currentSpeed) * h);
		
		//limits the speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
		

		Climb ();

	}
	
	
	void Jump(){
		if (grounded){
			rb2d.AddForce(Vector2.up * jumpPower);	
		}else{
			
			if (canDoubleJump){
				canDoubleJump = false;
				rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
				rb2d.AddForce(Vector2.up * jumpPower);
			}			
		}
	}
	
	void Climb(){
		if (canClimb) {
			if(this.grounded){
				rb2d.velocity = new Vector2 (rb2d.velocity.x, this.climbPower * Input.GetAxis ("Vertical"));
			}else{
				rb2d.velocity = new Vector2 (0, this.climbPower * Input.GetAxis ("Vertical"));
			}
		}
	}
	
	void UpdateSprite(){
		if (Input.GetAxis ("Horizontal") < -0.1f) {
			direction = -1;
			transform.localScale = new Vector3(-1,1,1);
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3(1,1,1);
			direction = 1;
		}
	}
	void Respawn(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void Die(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void SetCanClimb(bool canClmb){
		this.canClimb = canClmb;
	}
	
	public void SetGrounded(bool grd){
		this.grounded = grd;
	}
	
	public int GetDirection(){
		return this.direction;
	}
	
	public string GetTag(){
		return this.tag.ToString();
	}
	
	public Vector3 GetPosition(){
		return this.transform.position;
	}
	
	public bool GetCanClimb(){
		return this.canClimb;
	}

	public void SetGravity(float grav) {
			rb2d.gravityScale = grav;
	}

	void Grounded(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .3f, currentPos.y - .5f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .3f, currentPos.y-.5f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		bool tempGround = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
		bool tempEnemy = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Enemy"));

		if (tempEnemy || tempGround) {
			this.grounded = true;
			this.canDoubleJump = true;
		} else {
			this.grounded = false;
		}

	}

	void Rope(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .3f, currentPos.y + .2f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .3f, currentPos.y + .2f, currentPos.z);
		Debug.DrawLine (startPos, endPos, Color.green);
		bool touchingRope = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Rope"));

		if (touchingRope) {
			this.SetCanClimb (true);
			this.SetGravity (0);
		} else {
			this.SetCanClimb (false);
			this.SetGravity (1);
		}
	}

	public void ApplyAbility(IAbility ability){
		ability.ApplyAbility (this);
	}

	public void AddEffect(IEffect effect) {
		this.effects.Add (effect);
		CalculateSpeed ();
	}
	
	public IDefenseBehavior GetDefense(){
		return this.playerDefense;
	}
	
	public void CalculateSpeed(){
		float flatValue = 0f;
		float percentage = 0f;
		for (int i = 0; i < this.effects.Count; i++) {
			IEffect effect = (IEffect) this.effects [i];
			if (effect.GetType() == typeof(CrippleEffect)) {
				CrippleEffect ce = (CrippleEffect) effect;
				flatValue += ce.GetFlatValue();
				percentage += ce.GetPercentage();
			}
		}
		this.currentSpeed = (this.speed - flatValue) * (1 - percentage);
		
		if (currentSpeed < 0)
			this.currentSpeed = 0;
		
		print ("Current speed is " + this.currentSpeed);
	}

	public void EffectExpiration(){
		for (int i = 0; i < this.effects.Count; i++) {
			IEffect effect = (IEffect)this.effects [i];
			if (effect.IsExpired ()) {
				this.effects.RemoveAt (i);
				print (i + " removed");
				CalculateSpeed ();
			}
		}
	}



}