﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Swab : VRTK_InteractableObject
{
    // private uint 
    // private VRTK_ControllerReference controller;

    public PetriDish petriDish;
    private RaycastHit swabTouching;
    public int swabTipSize = 5;
    public float tipHeight = 1.0f;


    public Color swabColor = Color.green;
    

    private bool lastTouch;
    private Quaternion lastAngle;

    // Start is called before the first frame update
    void Start()
    {
        // controller = VRTK_ControllerReference.GetControllerReference();
        // petriDish = GameObject.Find("PetriDish").GetComponent<PetriDish>();

        
    }

    // Update is called once per frame
    override protected void Update()
    {
  
        // float tipHeight = transform.Find("Tip").transform.localScale.y;
        Vector3 tip = transform.Find("Tip").transform.position;
        // tipHeight should be last parameter...
        if (Physics.Raycast(tip, transform.up, out swabTouching, tipHeight)) 
        {
            if (!swabTouching.collider.gameObject.CompareTag("PetriDish"))
            {
                
                return; // we want to break out of this if it's not touching petri dish
            }

            // VRTK_ControllerHaptics.TriggerHapticPulse(controller, 0.1f); // haptic feedback
            
            
            petriDish = swabTouching.collider.gameObject.GetComponent<PetriDish>();
            petriDish.setSwabSize(swabTipSize);
            petriDish.SetColor(swabColor);
            petriDish.SetTouchPosition(swabTouching.textureCoord.x, swabTouching.textureCoord.y);
            petriDish.ToggleSwab(true);

            if (!lastTouch)
            {
                lastTouch = true;
                lastAngle = transform.rotation;
            }
        }

        else
        {
            if (petriDish != null)
            {
                petriDish.ToggleSwab(false);
                lastTouch = false;
            }
            
        }
        

        if (lastTouch)
        {
            transform.rotation = lastAngle;
        }
    }
}