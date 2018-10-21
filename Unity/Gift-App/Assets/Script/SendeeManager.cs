using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendeeManager : MonoBehaviour {
    [SerializeField]
    GameObject SendeeNodePrefab;

    [SerializeField]
    GameObject SendeeViewContent;

	// Use this for initialization
	void Start () {
        FacebookAPI.GetFriendsID(IDs =>
        {
            foreach (var ID in IDs)
            {
                UserPreference.GetPreferencefromID(ID, userPreference =>
                {
                    string friendsName = userPreference.name;
                    GameObject SendeeNodeInstance = Instantiate(SendeeNodePrefab, SendeeViewContent.transform);
                    SendeeNodeInstance.SendMessage("Init", friendsName);
                    SendeeNodeInstance.SendMessage("InitID", ID);
                });
            }
        });
	}
	
}
