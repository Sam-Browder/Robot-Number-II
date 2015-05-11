using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	private GameObject enemy;
	private RectTransform trans;
	private float midX;
	private float midY;
	
	// Use this for initialization
	void Start () {
		this.enemy = GameObject.FindGameObjectWithTag ("Enemy");
		this.trans = (RectTransform)this.transform.parent;

	}
	
	// Update is called once per frame
	void Update () {
		midX = this.trans.position.x + this.trans.rect.width/2;
		midY = this.trans.position.y;
		Image image = GetComponent<Image> ();
		Vector3 cpos = Camera.main.WorldToViewportPoint (this.enemy.transform.position);
		this.transform.position = new Vector3 (cpos.x * midX, cpos.y * midY);
		image.fillAmount = this.enemy.gameObject.GetComponentInChildren<ICharacter>().GetDefense ().GetShield() / 50f;
	}
}
