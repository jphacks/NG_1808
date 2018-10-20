using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.MiniJSON;
using System.Linq;

[CreateAssetMenu(menuName ="ScriptableObject/CategoryDatabase")]
public class CategoryDatabase : ScriptableObject {

    [SerializeField]
    public List<string> categoryList;

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
