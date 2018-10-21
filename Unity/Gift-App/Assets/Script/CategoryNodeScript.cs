using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryNodeScript : MonoBehaviour {
    string categoryName;

    [SerializeField]
    CategoryDatabase categoryDatabase;

	public void Init(string categoryName)
    {
        transform.Find("Text").GetComponent<Text>().text = categoryName;
        this.categoryName = categoryName;
    }

    public void OnButton()
    {
        List<string> giftList = categoryDatabase.GetGiftList(categoryName);
        GameObject.Find("GiftManageObject").GetComponent<ChooseGiftManager>().MoveToGiftListPage(giftList);
    }
}
