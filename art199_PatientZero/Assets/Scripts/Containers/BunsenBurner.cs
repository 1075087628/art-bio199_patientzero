﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsenBurner : MonoBehaviour {
    ParticleSystem ps;
    Collider col;
    Light lgt;

    public bool turnedOn = false;

    void Start() {
        ps = GetComponent<ParticleSystem>();
        col = GetComponentInChildren<Collider>();
        lgt = GetComponentInChildren<Light>();

        if (turnedOn) {
            TurnOn();
        } else {
            TurnOff();
        }
    }

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        Toggle();
    //    }
    //}

    public void Toggle() {
        if (turnedOn) {
            TurnOff();
        } else {
            TurnOn();
        }
    }

    public void TurnOn() {
        turnedOn = true;
        ps.Play();
        col.enabled = true;
        lgt.enabled = true;
    }

    public void TurnOff() {
        turnedOn = false;
        ps.Stop();
        col.enabled = false;
        lgt.enabled = false;
    }
}
