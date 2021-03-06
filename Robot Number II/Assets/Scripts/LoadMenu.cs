﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadMenu : MonoBehaviour {

	private GameObject itemPanel;
	private GameObject shopPanel;

	// Use this for initialization
	void Start () {
		this.itemPanel = GameObject.Find ("ItemPanel");
		this.itemPanel.SetActive (false);

		this.shopPanel = GameObject.Find ("ShopPanel");
		this.shopPanel.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenItemPanel(){
		this.itemPanel.SetActive (true);
	}

	public void CloseItemPanel(){
		this.itemPanel.SetActive (false);
	}

	public void OpenShopPanel(){
		this.shopPanel.SetActive (true);
	}

	public void CloseShopPanel(){
		this.shopPanel.SetActive (false);
	}
}
