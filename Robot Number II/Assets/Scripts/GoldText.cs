﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldText : MonoBehaviour {
	
	private Text txt;
	private TestMenu global;
	
	// Use this for initialization
	void Start () {
		this.global = GameObject.FindObjectOfType<TestMenu> ();
		this.txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = ""+this.global.money;
	}
}

