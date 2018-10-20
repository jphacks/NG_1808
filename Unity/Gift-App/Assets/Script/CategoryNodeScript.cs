using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryNodeScript : MonoBehaviour {
    string categoryName;

	public void Init(string categoryName)
    {
        transform.Find("Text").GetComponent<Text>().text = categoryName;
        this.categoryName = categoryName;
    }

    public void OnButton()
    {
        List<string> giftList = new List<string> { "Yシャツ", "Tシャツ", "タートルネック", "ヒートテック", "フード付き" };
        GameObject.Find("GiftManageObject").GetComponent<ChooseGiftManager>().MoveToGiftListPage(giftList);
    }
}
