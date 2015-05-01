using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, ICharacter {
	
	//floats
	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 200f;
	public int numOfJumps = 2;
	
	//booleans
	public bool grounded;
	public bool canDoubleJump;
	public bool canClimb;
	public bool obstacleOnLeft;
	public bool obstacleOnRight;
	public bool noGroundLeft;
	public bool noGroundRight;
	public bool standingOnPlayer;
	public bool standingUnderPlayer;
	public bool playerInLineOfSight;
	public bool shouldMove;
	public bool canJumpLeft;
	public bool canJumpRight;
	
	public int direction = 1;
	
	//refrences
	private Rigidbody2D rb2d;
	private Animator anim;
	
	//Game values
	private float health =100;
	private float time = 0.0f;
	private float attackTimer = 0.0f;
	public float attackSpeed;
	public float jumpTime;
	
	public float visionDistance;
	
	private ICharacter player;
	private float distanceToPlayer;
	private float directionToPlayer;
	//private ProjSpawner projspawner;
	private CharacterAttack enemyAttack;
	
	
	
	// Use this for initialization
	void Start () {
		//player =(ICharacter) GameObject.FindGameObjectWithTag("Player");
		player =(ICharacter) FindObjectOfType (typeof(Player));
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		//this.projspawner = gameObject.GetComponentInChildren<ProjSpawner> ();
		this.enemyAttack = gameObject.GetComponentInChildren<CharacterAttack> ();
		IAttack projectile = new ProjectileAttack ();
		IAttack lazer = new LazerAttack ();
		this.enemyAttack.SetPrimaryAttack (projectile);
		this.enemyAttack.SetSecondaryAttack (lazer);
		this.canClimb = false;
	}
	
	
	void Update () {

		this.distanceToPlayer = Mathf.Abs( this.transform.position.x - this.player.GetPosition ().x);

		anim.SetBool("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs(rb2d.velocity.x));

		time += Time.deltaTime;
		attackTimer += Time.deltaTime;
		
		ObstacleOnLeft ();
		ObstacleOnRight ();
		NoGroundLeft ();
		NoGroundRight ();
		StandinOnPlayer ();
		StandingUnderPlayer ();
		LineOfSight ();
		CanJumpLeft ();
		CanJumpRight ();
				
		
		if (health <= 0.0) {
			Die();
		}
		
		AI ();
		UpdateSprite ();		
	}
	
	// do all physics in here
	void FixedUpdate()
	{
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
			rb2d.AddForce ((Vector2.right * speed) * h);
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

		//determines the direction to the player
		if (this.transform.position.x < this.player.GetPosition ().x) {
			this.directionToPlayer = 1;
		} else {
			this.directionToPlayer = -1;
		}

		//walk back and forth if you are to far from the player
		if (this.distanceToPlayer > this.visionDistance) {
			if (time > 2) {
				time = 0;
				this.direction = this.direction * -1;
			}
		} else {
			this.direction = (int)this.directionToPlayer;
		}

		if (this.distanceToPlayer < .8 && !this.standingOnPlayer && !this.standingUnderPlayer) {
			this.shouldMove = false;
		} else {
			this.shouldMove = true;
		}


		if(this.standingOnPlayer){
			this.direction = this.direction;
		}

		if (this.standingUnderPlayer) {
			this.direction = this.direction;
		}
			

		if (this.direction < 0 && this.obstacleOnLeft ||this.direction > 0 && this.obstacleOnRight) {
			if(this.grounded){
				this.numOfJumps = 1;
			}
			this.Jump ();
		}
		if (this.direction<0 && this.noGroundLeft && this.canJumpLeft || this.direction>0 && this.noGroundRight && this.canJumpRight){
			if(this.grounded){
				this.numOfJumps = 1;
			}
			this.Jump ();
		}


		
		if (this.playerInLineOfSight){
			if (attackTimer > this.attackSpeed) {
				attackTimer = 0;
				this.enemyAttack.DoAttack();
			}
		}
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
		Destroy (gameObject);
	}

	public void ApplyDefense(IAttack attack) {
		//this.playerDefense.doDefense (attack, this);
	}
	
	
	public void ApplyDamage(float damage){
		this.health = this.health - damage;
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
		if (this.numOfJumps>0 && this.jumpTime < this.time-.3){
			rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
			rb2d.AddForce(Vector2.up * jumpPower);	
			this.numOfJumps= this.numOfJumps-1;
			this.jumpTime= this.time;
			
		}			
	}
	
	
	
	void ObstacleOnLeft(){
		Vector3 currentPos = this.transform.position;
		Vector3 endPos = new Vector3 (currentPos.x - 1, currentPos.y-.3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.obstacleOnLeft = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	void ObstacleOnRight(){
		Vector3 currentPos = this.transform.position;
		Vector3 endPos = new Vector3 (currentPos.x + 1, currentPos.y-.3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.obstacleOnRight = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	
	void NoGroundLeft(){
		Vector3 currentPos = new Vector3(this.transform.position.x-.5f,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x, currentPos.y-3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.noGroundLeft = !Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	void NoGroundRight(){
		Vector3 currentPos = new Vector3(this.transform.position.x+.5f,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x, currentPos.y-3f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.noGroundRight = !Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}

	void CanJumpLeft(){
		Vector3 currentPos = new Vector3(this.transform.position.x-.5f,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x - 4, currentPos.y-1f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.canJumpLeft = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
	}
	
	void CanJumpRight(){
		Vector3 currentPos = new Vector3(this.transform.position.x+.5f,this.transform.position.y, this.transform.position.z);
		Vector3 endPos = new Vector3 (currentPos.x+4, currentPos.y-1f, currentPos.z);
		Debug.DrawLine(currentPos, endPos,Color.green);
		this.canJumpRight = Physics2D.Linecast (currentPos, endPos, 1 << LayerMask.NameToLayer ("Ground"));
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

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}