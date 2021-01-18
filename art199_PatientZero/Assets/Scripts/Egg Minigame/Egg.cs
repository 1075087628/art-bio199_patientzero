﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
	//code for when the egg enters the flashlight light and changes meshes
	public Material inFlashlight;
	public Material outFlashlight;

    void Start()
    {
    	// getting the original mesh of the egg so that when the egg exits the light it reverts 
    	// back to normal
        outFlashlight = this.gameObject.GetComponent<Renderer>().material;
    }

	void OnCollisionEnter (Collision collision) {
		if(collision.gameObject.tag == "Flashlight"){
			this.gameObject.GetComponent<Renderer>().material= inFlashlight;
		}
		if(collision.gameObject.activeSelf == false){
			this.gameObject.GetComponent<Renderer>().material = outFlashlight;
		}
	}

	void OnCollisionExit (Collision collision){
		this.gameObject.GetComponent<Renderer>().material = outFlashlight;
	}
}