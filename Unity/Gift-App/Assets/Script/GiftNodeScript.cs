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

    public void OnButton()
    {
        if(giftName.Contains(" "))
        {
            giftName.Replace(" ", "|");
        }
        string encodeGift = WWW.EscapeURL(giftName);
        Application.OpenURL("https://www.amazon.co.jp/s/ref=sr_st_review-rank?sort=review-rank&keywords=" + encodeGift);
    }
}
