using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendeeNodeScript : MonoBehaviour {
    string userId;
    string userName;

	public void Init(string userName)
    {
        transform.Find("Text").GetComponent<Text>().text = userName;
        this.userName = userName;
    }

    public void InitID(string userId)
    {
        this.userId = userId;
    }

    public void OnButton()
    {
        ChooseGiftManager.SetTargetId(userId);
    }
}
