using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Door : MonoBehaviour
{

	public EnemyController enemy;
	private Animator anim;
	
	// Use this for initialization
	void Start ()
	{
		this.anim = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if(enemy==null){
			Physics2D.IgnoreCollision (this.gameObject.GetComponent<BoxCollider2D>(), GameObject.Find("Player").GetComponent<BoxCollider2D>());

			this.anim.SetBool ("enemyKilled", enemy == null);
		}
	}
}