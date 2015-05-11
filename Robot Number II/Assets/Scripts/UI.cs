using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SkillTimer[] timers = gameObject.GetComponentsInChildren<SkillTimer>();
		timers [0].SetIndex (0);
		timers [1].SetIndex (1);
		timers [2].SetIndex (2);
		timers [3].SetIndex (3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
