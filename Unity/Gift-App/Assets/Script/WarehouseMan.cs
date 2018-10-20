using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseMan : MonoBehaviour
{
    void Start()
    {
        var pref = new UserPreference("bongteng16", "くわ", "sometoken1234", "{\"meat\":5, \"fish\":3}");
        pref.Save();
        var pref2 = new UserPreference("bongteng", "わわ", "anothertoken456", "{}");
        pref.Save();

        pref2.Print();
    }

    void Update()
    {

    }
}
