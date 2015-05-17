using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chest : MonoBehaviour
{
	
	private bool wasTouched;
	private Animator anim;
	private TestMenu testMenu;

	public int goldAmount;
	
	// Use this for initialization
	void Start ()
	{
		this.wasTouched = false;
		anim = gameObject.GetComponent<Animator> ();
		this.testMenu = GameObject.FindObjectOfType<TestMenu> ();
	}

	// Update is called once per frame
	void Update ()
	{
		anim.SetBool ("wasTouched", this.wasTouched);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			this.wasTouched = true;
			this.testMenu.money += goldAmount;

		}
	}
}
