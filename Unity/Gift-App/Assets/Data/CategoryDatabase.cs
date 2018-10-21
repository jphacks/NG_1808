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
                    _categoryDictionary = new Dictionary<string, int>();
                    if(PlayerPrefs.GetString("gender") == "male")
                    {
                        List<CategoryProperty> selectList = _categoryList.Where(c => !c.tag.Contains("女性専用")).ToList();

                        foreach(var property in selectList)
                        {
                            if (property.tag.Contains("女性傾向強"))
                            {
                                _categoryDictionary.Add(property.categoryName, -3);
                            }else if (property.tag.Contains("女性傾向弱"))
                            {
                                _categoryDictionary.Add(property.categoryName, -1);
                            }else
                            {
                                _categoryDictionary.Add(property.categoryName, 0);
                            }

                        }
                    }else
                    {
                        List<CategoryProperty> selectList = _categoryList.Where(c => !c.tag.Contains("男性専用")).ToList();

                        foreach (var property in selectList)
                        {
                            if (property.tag.Contains("男性傾向強"))
                            {
                                _categoryDictionary.Add(property.categoryName, -3);
                            }
                            else if (property.tag.Contains("男性傾向弱"))
                            {
                                _categoryDictionary.Add(property.categoryName, -1);
                            }
                            else
                            {
                                _categoryDictionary.Add(property.categoryName, 0);
                            }

                        }
                    }
                    
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
