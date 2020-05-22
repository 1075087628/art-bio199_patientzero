﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {
    public Image image;

    public float Value {
        get {
            return image.fillAmount;
        }

        set {
            image.fillAmount = value;
        }
    }

    public bool Visible {
        get {
            return gameObject.activeInHierarchy;
        }

        set {
            gameObject.SetActive(value);
        }
    }

    void Start() {
        Value = 0;
    }

    void Update() {
        transform.rotation = Camera.main.transform.rotation;
    }
}