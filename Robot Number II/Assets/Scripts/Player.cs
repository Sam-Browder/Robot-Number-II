using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ICharacter {
	
	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float currentSpeed;
	public float jumpPower = 200f;
	public float climbPower = 5;
	public float defaultGcd = 0.5f;
	public float gcd = 0.5f;
	private float platformVelocity;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool canClimb;
	public bool isStun;
	private bool onPlatform;
	
	public int direction = 1;
	
	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	private CharacterAbility playerAbility;
	private CharacterDefense playerDefense;
	private FittingMenu fit = null;

	private ArrayList effects = new ArrayList();
	
	
	// Use this for initialization
	void Start () {
		this.canClimb = false;
		this.isStun = false;
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		this.playerAbility = gameObject.GetComponentInChildren<CharacterAbility> ();
		this.playerDefense = gameObject.GetComponentInChildren<CharacterDefense> ();

		this.fit = gameObject.GetComponentInChildren<FittingMenu> ();
		TestMenu global = GameObject.FindObjectOfType<TestMenu> ();
		this.fit.Equip (global.activeWeapon);
		this.fit.Equip (global.activeDefense);
		this.fit.Equip (global.activeUtility);
		this.fit.SetAbility ();

		InitializeCharacter ();
	}


	// Update is called once per frame
	//public Transform testProj;
	void Update () {


		if (this.isStun)
			return;

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
		if (Input.GetKeyDown(KeyCode.Q)) {
			this.playerAbility.ExecuteAbility(0);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			this.playerAbility.ExecuteAbility(1);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			this.playerAbility.ExecuteAbility(2);
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			this.playerAbility.ExecuteAbility(3);
		}
		DrawVelocity ();
		EffectExpiration ();

	}
	
	// do all physics in here
	void FixedUpdate()
	{
		//sets the player verticle
		transform.rotation = Quaternion.AngleAxis(30, Vector3.up);

		Vector3 groundedEaseVelocity = rb2d.velocity;
		groundedEaseVelocity.y = rb2d.velocity.y;
		groundedEaseVelocity.z = 0.0f;
		groundedEaseVelocity.x *= 0.80f;

		Vector3 airEaseVelocity = rb2d.velocity;
		airEaseVelocity.y = rb2d.velocity.y;
		airEaseVelocity.z = 0.0f;
		airEaseVelocity.x *= .8f;

		if (this.isStun) {
			groundedEaseVelocity.x = 0f;
			rb2d.velocity = groundedEaseVelocity;
			return;
		}

		//Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Ground"), false);// && ((Input.GetAxis("Vertical") > 0.1f) || (Input.GetAxis("Vertical") < -0.1f)) );
		//fake friction
		if (grounded) {
			rb2d.velocity = groundedEaseVelocity;
		} else {
			rb2d.velocity = airEaseVelocity;
		}
		
		//moves player
		float h = Input.GetAxis ("Horizontal");
		rb2d.AddForce ((Vector2.right * this.currentSpeed) * h);


		if (this.onPlatform) {
			Debug.Log(this.platformVelocity);
			this.rb2d.velocity = new Vector2 (this.rb2d.velocity.x +this.platformVelocity, this.rb2d.velocity.y);//+ this.platformVelocity
		} else {

			if(this.grounded){
			
				//limits the speed
				if (rb2d.velocity.x > maxSpeed) {
					rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
				}
				if (rb2d.velocity.x < -maxSpeed) {
					rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
				}
			}


		}
		

		Climb ();


		//if(this.onPlatform){
		//	
		//	rb2d.velocity = new Vector2(this.rb2d.velocity.x + this.platformVelocity, this.rb2d.velocity.y);
		//}
		//Debug.Log(this.platformVelocity);
		//Debug.Log(this.onPlatform);


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

	public void SuperJump(float force){
		rb2d.AddForce (Vector2.up * force);
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

	public void InitializeCharacter(){
		TestMenu global = GameObject.FindGameObjectWithTag ("Global").gameObject.GetComponent<TestMenu>();
		string[] weaponData;
		double[] armorData;
		float health;
		if (global == null) {
			weaponData = new string[] {"Projectile", "Lazer","BurstJump","SpeedBurst"};
			armorData = new double[] {1.0, 1.0, 1.0, 1.0};
			health = 100f;
		} else {
			weaponData = global.weaponData;
			armorData = global.armorData;
			health = global.health;
		}
		/*IAbility ab;
		for (int i = 0; i < weaponData.Length; i++) {
			switch (weaponData [i]) {
			case "Projectile":
				ab = new BasicAttack ("Projectile", 10f, 10f, 10f, 10f, 0.5f);
				break;
			case "Lazer":
				ab = new BasicAttack ("Lazer", 10f, 10f, 10f, 10f, 0.5f);
				break;
			case "BurstJump":
				ab = new BurstJump (500f, 5f);
				break;
			case "Rush":
				ab = new Rush (4000f, 3f);
				break;
			case "SpeedBurst":
				ab = new SpeedBurst(-2f,5f);
				break;
			case "GrenadeToss":
				ab = new GrenadeToss(30f,30f,30f,30f,6f);
				break;
			case "DeathLazer":
				ab = new DeathLazer(5f,5f,5f,5f,5f);
				break;
			default:
				ab = new BasicAttack ("Projectile", 0f, 0f, 0f, 0f, 0.5f);
				break;
			}
			this.playerAbility.SetAbility (ab, i);
		}*/
		this.playerDefense.SetResistance (armorData [0], armorData [1], armorData [2], armorData [3]);
		this.playerDefense.ResetHealth (health);
		this.currentSpeed = speed;
		this.gcd = Time.time;
	}

	public bool GlobalCooldown(){
		if (Time.time > this.gcd) {
			this.gcd = Time.time + this.defaultGcd;
			return true;
		} else {
			return false;
		}
	}

	
	public void Die(){
		Application.LoadLevel("GameOverScene");
	}
	
	public void SetCanClimb(bool canClmb){
		this.canClimb = canClmb;
	}
	
	public void SetGrounded(bool grd){
		this.grounded = grd;
	}

	public void SetYVelocity(float vertVel){
		rb2d.velocity = new Vector2(rb2d.velocity.x, vertVel);
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

	public Rigidbody2D GetRB2D(){
		return this.rb2d;
	}

	void Grounded(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .2f, currentPos.y - .5f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .2f, currentPos.y-.5f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		bool tempGround = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
		bool tempEnemy = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Enemy"));
		bool tempOnPlatform = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Platform"));

		//this.onPlatform = tempOnPlatform;


		if (tempEnemy || tempGround || tempOnPlatform) {
			this.grounded = true;
			this.canDoubleJump = true;
		} else {
			this.grounded = false;
		}

	}

	void DrawVelocity(){
		Vector3 currentPos = this.transform.position;
		Debug.DrawLine(currentPos, new Vector3(currentPos.x + rb2d.velocity.x,currentPos.y + rb2d.velocity.y,0),Color.red);
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
		CheckStatus ();
	}

	public ArrayList GetEffects(){
		return this.effects;
	}
	
	public IDefenseBehavior GetDefense(){
		return this.playerDefense;
	}
	
	public void CheckStatus(){
		float flatValue = 0f;
		float percentage = 0f;
		bool stun = false;
		float gcd = 0.5f;
		for (int i = 0; i < this.effects.Count; i++) {
			IEffect effect = (IEffect) this.effects [i];
			if (effect.GetType() == typeof(CrippleEffect)) {
				CrippleEffect ce = (CrippleEffect) effect;
				flatValue += ce.GetFlatValue();
				percentage += ce.GetPercentage();
			}
			if (effect.GetType() == typeof(StunEffect)) {
				stun = true;
			}
		}

		this.currentSpeed = (this.speed - flatValue) * (1 - percentage);
		this.isStun = stun;
		this.defaultGcd = gcd;
		
		if (currentSpeed < 0) {
			this.currentSpeed = 0;
		}

	}


	public void EffectExpiration(){
		for (int i = 0; i < this.effects.Count; i++) {
			IEffect effect = (IEffect)this.effects [i];
			if (effect.IsExpired ()) {
				this.effects.RemoveAt (i);
				CheckStatus ();
			}
		}
	}

	public FittingMenu GetFitting(){
		return this.fit;
	}

	public CharacterAbility GetAbilities(){
		return this.playerAbility;
	}

	public void SetPlatformVelocity(float vel){
		this.platformVelocity = vel;
	}

	public void SetOnPlatform(bool bol){
		this.onPlatform = bol;
	}


	public float GetMaxHealth (){
		return 0;
	}



}