using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryNode : MonoBehaviour
{
    [SerializeField]
    static GameObject NodePrefab;
    [SerializeField]
    int Month;
    [SerializeField]
    int Day;
    [SerializeField]
    string Summary;
    [SerializeField]
    string person;

    void Start()
    {
        this.GetComponentInChildren<Text>().text = string.Format("{0}月{1}日\n{2}", this.Month, this.Day, this.Summary);
    }

    public bool IsSelected(int month, int day)
    {
        bool isSameMonth = this.Month == month;
        bool isSameDay = this.Day == day;
        return isSameMonth && isSameDay;
    }
}
