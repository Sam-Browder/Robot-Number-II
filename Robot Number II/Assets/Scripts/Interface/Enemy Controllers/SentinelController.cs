using UnityEngine;
using System.Collections;

public class SentinelController : MonoBehaviour, ICharacter {

	//floats
	private float maxHealth = 50f;
	public float maxSpeed = 3;
	public float speed = 50f;
	public float currentSpeed;
	public float jumpPower = 200f;
	public int numOfJumps = 2;
	private int cashValue = 1;
	public float minDistFromPlayer = 2f;
	private Vector2 unitDirectionVector;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool canClimb;
	public bool facingObsticle;
	public bool noGround;
	public bool isHittingFloor;
	public bool isHittingCeiling;
	public bool playerInLineOfSight;
	public bool playerInLineOfSightUp;
	public bool playerInLineOfSightCenter;
	public bool playerInLineOfSightDown;

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
		this.player =(ICharacter) FindObjectOfType (typeof(Player));
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		//this.projspawner = gameObject.GetComponentInChildren<ProjSpawner> ();
		this.enemyAbility = gameObject.GetComponentInChildren<CharacterAbility> ();
		this.enemyDefense = gameObject.GetComponentInChildren<EnemyDefense> ();
		IAbility projectile = new BasicAttack ("Lazer",10f,10f,10f,10f, 0f);
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
		
		//Grounded ();
		//FacingObstacle ();
		//NoGround ();
		//StandinOnPlayer ();
		//StandingUnderPlayer ();
		LineOfSightUp ();
		LineOfSightCenter ();
		LineOfSightDown ();
		hittingCeiling ();
		hittingFloor ();
		//CanMakeJump();
		TooCloseToPlayer ();
		//checkIsStuck();
		
		
		if (this.playerInLineOfSightCenter || this.playerInLineOfSightDown || this.playerInLineOfSightUp) {
			this.playerInLineOfSight = true;
		} else {
			this.playerInLineOfSight = false;
		}
		
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
		if (this.playerSpotted && this.shouldMove) {
			rb2d.AddForce ((this.unitDirectionVector * this.speed));

		}
		
		
		//limits the speed
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		}
		
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}

		if (rb2d.velocity.y > maxSpeed) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, maxSpeed);
		}
		
		if (rb2d.velocity.y < -maxSpeed) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, -maxSpeed);
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
		//this.rb2d = gameObject.GetComponent<Rigidbody2D> ();
		//this.transform.position = this.blueNode.transform.position;
		float y1 = this.transform.position.y;
		float x1 = this.transform.position.x;
		float y2 = this.player.GetPosition ().y;
		float x2 = this.player.GetPosition().x;
		Vector2 directionVector = new Vector2 (x2 - x1, y2 - y1);
		float directionVecotrMagnitude = directionVector.magnitude;
		this.unitDirectionVector = new Vector2 (directionVector.x / directionVecotrMagnitude, directionVector.y / directionVecotrMagnitude);

		
		if (this.transform.position.x < this.player.GetPosition ().x) {
			this.directionToPlayer = 1;
		} else {
			this.directionToPlayer = -1;
		}
		
		if (this.distanceToPlayer < this.visionDistance+1) {
			this.playerSpotted=true;
			this.spottedTime = this.time;
		}else if(this.spottedTime < this.time - 4f){
			this.playerSpotted=false;
		}
		
		
		
		if (this.playerSpotted) {
			this.direction = this.directionToPlayer;
			this.isPatroling=false;
		} else {
			Patrol();
		}
		
		
		if (this.playerInLineOfSight){
			if (attackTimer > this.attackSpeed) {
				attackTimer = 0;
				this.enemyAbility.ExecuteAbility(0);
			}
		}

		if (this.tooCloseToPlayer) {
			this.shouldMove = false;
		} else {
			this.shouldMove = true;
		}

		if (this.isHittingCeiling && this.transform.position.y < this.player.GetPosition().y) {
			this.unitDirectionVector = new Vector2(this.direction, 0f);
		}

		if (this.isHittingFloor) {
			this.unitDirectionVector = new Vector2(this.direction, .5f);
		}

		//if (this.noGround) {
		//	this.shouldMove = false;
		//} else {
		//	this.shouldMove = true;
		//}
		
		
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
	
	
	
	
	

	
	
	void hittingFloor(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .5f, currentPos.y - .8f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .5f, currentPos.y-.8f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.isHittingFloor = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	void hittingCeiling(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x - .5f, currentPos.y + .4f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x + .5f, currentPos.y+.4f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.isHittingCeiling = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	void LineOfSightUp(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x, currentPos.y-.25f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x+visionDistance * this.direction, currentPos.y+.25f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.playerInLineOfSightUp = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
	}
	void LineOfSightCenter(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x, currentPos.y-.25f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x+visionDistance * this.direction, currentPos.y-.25f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.playerInLineOfSightCenter = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
	}
	void LineOfSightDown(){
		Vector3 currentPos = this.transform.position;
		Vector3 startPos = new Vector3 (currentPos.x, currentPos.y-.25f, currentPos.z);
		Vector3 endPos = new Vector3 (currentPos.x+visionDistance * this.direction, currentPos.y-.75f, currentPos.z);
		Debug.DrawLine(startPos, endPos,Color.green);
		this.playerInLineOfSightDown = Physics2D.Linecast (startPos, endPos, 1 << LayerMask.NameToLayer ("Player"));
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