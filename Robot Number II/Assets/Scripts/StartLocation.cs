using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartLocation : MonoBehaviour
{
	
	private bool wasTouched = false;
	
	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			this.wasTouched = true;
		}
	}

	public bool isTouched() {
		return this.wasTouched;
	}

}
