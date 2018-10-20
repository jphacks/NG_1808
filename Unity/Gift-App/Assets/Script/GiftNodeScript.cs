using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftNodeScript : MonoBehaviour {
    string giftName;

    public void Init(string giftName)
    {
        transform.Find("Text").GetComponent<Text>().text = giftName;
        this.giftName = giftName;
    }
}
