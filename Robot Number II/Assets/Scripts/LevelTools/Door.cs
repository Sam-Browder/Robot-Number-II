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
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Player"), LayerMask.NameToLayer ("Door"), enemy == null);
		Physics2D.IgnoreLayerCollision (LayerMask.NameToLayer ("Enemy"), LayerMask.NameToLayer ("Door"), enemy == null);

		this.anim.SetBool ("enemyKilled", enemy == null);
	}

}
