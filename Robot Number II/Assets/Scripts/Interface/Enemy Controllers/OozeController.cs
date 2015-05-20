using UnityEngine;
using System.Collections;

public class OozeController : MonoBehaviour, ICharacter {
	//floats
	private float maxHealth = 150f;
	public float maxSpeed = 3;
	public float speed = 50f;
	public float currentSpeed;
	public float jumpPower = 200f;
	public int numOfJumps = 2;
	private int cashValue = 1;
	public float minDistFromPlayer = 2f;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool canClimb;
	public bool facingObsticle;
	public bool noGround;
	public bool standingOnPlayer;
	public bool standingUnderPlayer;
	public bool playerInLineOfSight;
	public bool canMakeJump;
	public bool shouldMove = true;
	public bool tooCloseToPlayer;
	public bool isPatroling;
	public bool facingLeft;
	public bool facingRight;
	public bool playerSpotted;
	public bool isStuck;
	
	//control case in AI method
	public int controlCase;
	
	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	
	//Game values
	private float time = 0.0f;
	private float attackTimer = 0.0f;
	public float attackSpeed;
	public float jumpTime;
	public float spottedTime;
	
	public float visionDistance;
	
	public int direction = 1;
	private ICharacter player;
	private float distanceToPlayer;
	private int directionToPlayer;
	//private ProjSpawner projspawner;
	private CharacterAbility enemyAbility;
	private EnemyDefense enemyDefense;
	private FittingMenu fit = null;
	
	private ArrayList effects = new ArrayList();
	
	
	
	// Use this for initialization
	void Start () {
		//player =(ICharacter) GameObject.FindGameObjectWithTag("Player");
		player =(ICharacter) FindObjectOfType (typeof(Player));
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		//this.projspawner = gameObject.GetComponentInChildren<ProjSpawner> ();
		this.enemyAbility = gameObject.GetComponentInChildren<CharacterAbility> ();
		this.enemyDefense = gameObject.GetComponentInChildren<EnemyDefense> ();
		IAbility projectile = new MeleeAttack (10f,10f,10f,10f, 0f);
		//IEffect cripple = new CrippleEffect (1f,49f,0f);
		//IEffect stun = new StunEffect (2f);
		//projectile.AddEffect (cripple);
		//projectile.AddEffect (stun);
		this.enemyAbility.SetAbility(projectile,0);
		this.enemyAbility.SetAbility(projectile,1);
		this.currentSpeed = speed;
		this.canClimb = false;
	}
	
	
	void Update () {
		
		
		float xDistToPlayer = Mathf.Abs (this.transform.position.x - this.player.GetPosition ().x);
		float yDistToPlayer = Mathf.Abs (this.transform.position.x - this.player.GetPosition ().x);
		this.distanceToPlayer = Mathf.Sqrt(Mathf.Pow(2, xDistToPlayer) + Mathf.Pow(2, yDistToPlayer));
		
		
		//anim.SetBool("Grounded", grounded);
		//anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));
		
		time += Time.deltaTime;
		attackTimer += Time.deltaTime;
		
		Grounded ();
		FacingObstacle ();
		NoGround ();
		StandinOnPlayer ();
		StandingUnderPlayer ();
		LineOfSight ();
		CanMakeJump();
		TooCloseToPlayer ();
		checkIsStuck();

		
		
		
		if (this.enemyDefense.GetHealth() <= 0.0) {
			Die();
		}
		
		EffectExpiration ();
		
		AI ();
		UpdateSprite ();		
	}
	
	// do all physics in here
	void FixedUpdate()
	{	
		transform.rotation = Quaternion.AngleAxis(30, Vector3.up);
		
		Vector3 easeVelocity = rb2d.velocity;
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.80f;
		
		//fake friction
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}
		
		
		//moves player
		float h = direction;
		if (this.shouldMove) {
			rb2d.AddForce ((Vector2.right * this.currentSpeed) * h);
		}
		
		
		//limits the speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}
	
	void AI(){
		// these are the control bool's made by the line casts
		
		//public bool noGroundLeft;
		//public bool noGroundRight;
		//public bool standingOnPlayer;
		//public bool standingUnderPlayer;
		//public bool playerInLineOfSight;
		//public bool canMakeJump;
		//public bool facingObsticle;
		
		
		// these are control bool's made by if logic
		//public bool shouldMove;		
		//public bool tooCloseToPlayer;
		//public bool isPatroling;
		
		//determines the direction to the player

		if (this.transform.position.x < this.player.GetPosition ().x) {
			this.directionToPlayer = 1;
		} else {
			this.directionToPlayer = -1;
		}
		
		if (this.playerInLineOfSight || this.standingOnPlayer || this.standingUnderPlayer) {
			this.playerSpotted=true;
			this.spottedTime = this.time;
		}else if(this.spottedTime < this.time - 4f){
			this.playerSpotted=false;
		}
		

				
		if (this.playerSpotted) {
			if (this.standingOnPlayer || this.standingUnderPlayer){
				this.direction = this.direction;
			}else{
				this.direction = this.directionToPlayer;
			}
			this.isPatroling=false;
		} else {
			Patrol();
		}

		if (!this.playerInLineOfSight){
			if (attackTimer > this.attackSpeed) {
				attackTimer = 0;
				this.enemyAbility.ExecuteAbility(0);

			}
		}
		
		if (this.noGround) {
			this.shouldMove = false;
		} else {
			this.shouldMove = true;
		}

		
	}
	
	public Rigidbody2D GetRB2D(){
		return this.rb2d;
	}
	
	void UpdateSprite(){
		if (direction < -0.1f) {
			direction = -1;
			transform.localScale = new Vector3(-1,1,1);
		}
		if (direction > 0.1f) {
			transform.localScale = new Vector3(1,1,1);
			direction = 1;
		}
	}
	
	void Respawn(){
		GameObject newEnemy = (GameObject) Instantiate(Resources.Load("Enemy"));
		newEnemy.transform.position = new Vector3(2.4f,1.4f,0.0f);
		Destroy (gameObject);
		
	}
	
	public void Die(){
		GameObject globalData = GameObject.FindGameObjectWithTag ("Global");
		if(globalData != null) {
			globalData.gameObject.GetComponent<TestMenu> ().money += this.cashValue;
		}
		
		/*EnemyHealth[] health = GameObject.FindObjectsOfType<EnemyHealth> ();
		EnemyHealthBG[] healthBG = GameObject.FindObjectsOfType<EnemyHealthBG> ();
		for (int i = 0; i < health.Length; i++) {
			if (health [i].gameObject == this.gameObject) {
				Destroy (health [i]);
				Destroy (healthBG [i]);
			}
		}*/
		
		Destroy (gameObject);
	}
	
	public void SetGrounded(bool grd){
		this.grounded = grd;
	}
	
	public void SetCanClimb(bool canClimb){
		this.canClimb = canClimb;
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
	
	public bool GetCanClimb() {
		return false;
	}
	
	public void SetGravity(float grav) {
		rb2d.gravityScale = grav;
	}
	
	void Jump(){
		
		
		if (this.grounded){
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower);	
			this.canDoubleJump = true;
			this.jumpTime= this.time;
			
		}else if (this.canDoubleJump && this.jumpTime < this.time-.3){
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower);
			this.canDoubleJump = false;
			this.jumpTime = this.time;
		}
	}
	void Patrol(){
		if (time > 2) {
			time = 0;
			this.direction = this.direction * -1;
			this.isPatroling = true;
		}
	}
	
	
	
	
	
	void FacingObstacle(){
		Vector3 currentPos = this.transform.position;
		Vector3 endPos = new Vector3 (currentPos.x + 1*this.direction, currentPos.y-.3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.facingObsticle = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	
	void NoGround(){
		Vector3 currentPos = new Vector3(this.transform.position.x+.5f*this.direction,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x, currentPos.y-1f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.noGround= !Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	void CanMakeJump(){
		Vector3 currentPos = new Vector3(this.transform.position.x+.5f *this.direction,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (this.transform.position.x + 4.5f * this.direction, currentPos.y-1f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		if (this.grounded) {
			this.canMakeJump = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
		}
	}
	
	
	void StandinOnPlayer(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .5f, currentPos.y - .6f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .5f, currentPos.y-.6f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.standingOnPlayer = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
	}
	
	void StandingUnderPlayer(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .5f, currentPos.y + .6f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .5f, currentPos.y+.6f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.standingUnderPlayer = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
	}
	
	void LineOfSight(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x, currentPos.y +.25f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x+visionDistance * this.direction, currentPos.y+.25f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.playerInLineOfSight = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
	}
	
	void TooCloseToPlayer(){
		if (this.distanceToPlayer < this.minDistFromPlayer){
			this.tooCloseToPlayer = true;
		}else {
			this.tooCloseToPlayer = false;
		}
	}
	
	void Grounded(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .2f, currentPos.y - .5f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .2f, currentPos.y-.5f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		bool tempGround = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
		bool tempEnemy = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Enemy"));
		bool tempPlayer = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
		
		if (tempEnemy || tempGround || tempPlayer) {
			this.grounded = true;
			this.canDoubleJump = true;
		} else {
			this.grounded = false;
		}
		
	}
	
	void checkIsStuck(){
		if (this.shouldMove && Mathf.Abs(this.rb2d.velocity.x) < .01) {
			this.isStuck = true;
		} else {
			this.isStuck = false;
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
		return this.enemyDefense;
	}
	
	public void CheckStatus(){
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
				CheckStatus ();
			}
		}
	}
	
	public FittingMenu GetFitting(){
		return this.fit;
	}
	
	public CharacterAbility GetAbilities(){
		return this.enemyAbility;
	}

	public float GetMaxHealth (){
		return this.maxHealth;
	}
	
	
	
}



