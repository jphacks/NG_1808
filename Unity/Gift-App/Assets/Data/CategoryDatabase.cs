using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.MiniJSON;
using System.Linq;

[CreateAssetMenu(menuName ="ScriptableObject/CategoryDatabase")]
public class CategoryDatabase : ScriptableObject {

    public List<CategoryProperty> _categoryList = new List<CategoryProperty>();

    [System.Serializable]
    public class CategoryProperty
    {
        public string categoryName;
        public List<string> tag;
        public Texture2D categoryImage;
        public List<string> giftList;
    }

    public List<string> GetGiftList(string categoryName)
    {
        List<List<string>> giftListList = _categoryList.Where(c => c.categoryName == categoryName).Select(c => c.giftList).ToList();
        return giftListList[0];
    }

    public List<string> categoryList
    {
        get
        {
            return _categoryList.Select(c => c.categoryName).ToList();
        }
    }

    Dictionary<string, int> _categoryDictionary;


    public Dictionary<string, int> categoryDictionary
    {
        get
        {
            if (_categoryDictionary == null)
            {
                if (PlayerPrefs.GetString("LikeJson", "") == "")
                {
                    _categoryDictionary = categoryList.ToDictionary(c => c, c => 0);
                }
                else
                {
                    _categoryDictionary = (Json.Deserialize(PlayerPrefs.GetString("LikeJson")) as Dictionary<string,object>).ToDictionary(c=>c.Key,c=>(int)(long)c.Value);
                }
                return _categoryDictionary;
            }
            else
            {
                return _categoryDictionary;
            }
        }
    }

}
