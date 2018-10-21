using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Sticky : MonoBehaviour
{
    [SerializeField]
    GameObject NodePrefab;
    [SerializeField]
    Calendar Calender;
    [SerializeField]
    GameObject Content;
    List<Anniversary> anniversaries = new List<Anniversary>();

    void Start()
    {
        this.anniversaries.Add(Anniversary.Sample);

        if (this.anniversaries.Count > 0)
        {
            GetComponentInChildren<Image>().color = Color.red;
        }
    }

    public void ShowAnniversary()
    {
        foreach (Transform t in this.Content.transform)
        {
            Destroy(t.gameObject);
        }
        List<GameObject> nodes = this.anniversaries.Select(ConvertToNode).ToList();
        nodes.ForEach(n =>
        {
            n.transform.SetParent(this.Content.transform); n.transform.localScale = Vector3.one;
        });
    }

    private GameObject ConvertToNode(Anniversary anniversary)
    {
        string summary = anniversary.Summary;
        string person = anniversary.Person;
        GameObject newNode = Instantiate(this.NodePrefab);
        string month = this.Calender.targetDate.Month.ToString();
        string day = this.Calender.targetDate.Day.ToString();
        newNode.GetComponentInChildren<Text>().text = string.Format("{0}月{1}日\n{2}", month, day, summary);
        return newNode;
    }

}

public class Anniversary
{
    public string Summary { get; private set; }
    public string Person { get; private set; }

    public static Anniversary Sample
    {
        get
        {
            return new Anniversary("狩人さんの誕生日です。", "hunter3");
        }
    }

    public Anniversary(string summary, string person)
    {
        this.Summary = summary;
        this.Person = person;
    }
}
