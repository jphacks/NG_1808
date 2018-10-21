using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HistoryView : MonoBehaviour
{
    [SerializeField]
    Calendar calender;
    List<HistoryNode> nodes;
    // Use this for initialization
    void Start()
    {
        this.nodes = GetComponentsInChildren<HistoryNode>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickupNodes()
    {
        int month = this.calender.targetDate.Month;
        int day = this.calender.targetDate.Day;
        foreach (var n in this.nodes)
        {
            n.gameObject.SetActive(n.IsSelected(month, day));
        }
    }
}
