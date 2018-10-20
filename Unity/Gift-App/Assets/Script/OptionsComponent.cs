﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsComponent : MonoBehaviour {
    [SerializeField]
    Text buttonText;
    [SerializeField]
    QuestionManager.CategoryChoice myChoice;

    public void SetButtonText(string ID)
    {
        buttonText.text = ID;
    }

    public void OnButton()
    {
        QuestionManager.instance.GetChoiceButton(myChoice);
    }
	
}