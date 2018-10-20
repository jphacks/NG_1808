using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.MiniJSON;
using System.Linq;

public class ChooseGiftManager : MonoBehaviour {
    [SerializeField]
    int HigherRankingCount = 5;

    [SerializeField]
    GameObject CategoryViewContent;

    [SerializeField]
    GameObject CategoryNodePrefab;

    [SerializeField]
    GameObject GiftViewContent;

    [SerializeField]
    GameObject GiftNodePrefab;

    [SerializeField]
    GameObject GiftPanel;

    static string targetUserId;

    public bool GiftPageFlag = false;

	// Use this for initialization
	void Start () {
        UserPreference.GetPreferencefromID(targetUserId, userPreference =>
        {
            Dictionary<string, object> _likeDic = Json.Deserialize(userPreference.json) as Dictionary<string, object>;
            Dictionary<string,int> likeDic =_likeDic.ToDictionary(c => c.Key, c => (int)(long)c.Value);
            List<string> sortLikeList = likeDic.OrderByDescending(c => c.Value).Select(c => c.Key).ToList();
            for (int i = 0; i < HigherRankingCount; i++)
            {
                GameObject CategoryNodeInstance = Instantiate(CategoryNodePrefab, CategoryViewContent.transform);
                CategoryNodeInstance.SendMessage("Init", sortLikeList[i]);
            }
        });
	}

    public void MoveToGiftListPage(List<string> giftList)
    {
        List<GameObject> childObjects = GiftViewContent.GetComponentsInChildren<GiftNodeScript>().Select(c => c.gameObject).ToList();
        childObjects.ForEach(c => Destroy(c));
        for (int i = 0; i < giftList.Count; i++)
        {
            GameObject GiftNodeInstance = Instantiate(GiftNodePrefab, GiftViewContent.transform);
            GiftNodeInstance.SendMessage("Init", giftList[i]);
        }

        GiftPanel.transform.localPosition -= new Vector3(200, 0, 0);
        GiftPageFlag = true;

    }

    public void MoveToCategoryListPage()
    {
        if (GiftPageFlag)
        {
            GiftPanel.transform.localPosition += new Vector3(200, 0, 0);
            GiftPageFlag = false;
        }
    }

    public static void SetTargetId(string userId)
    {
        targetUserId = userId;
    }

}
