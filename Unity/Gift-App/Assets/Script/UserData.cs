using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class UserData : ScriptableObject {
    string _userId;
    string _userAccessToken;
    string _userName;

    static UserData _userData = null;

    public static UserData userData{
        get {
            if (_userData != null)
            {
                //Debug.Log("UserData: Find userData");
                return _userData;
            }
            else
            {
                if (PlayerPrefs.GetString("user_local_token", "") == "")
                {
                    //Debug.Log("UserData: No SaveData");
                    return null;
                }
                else
                {
                    //Debug.Log("UserData: Reload SaveData");
                    _userData = CreateInstance<UserData>();
                    _userData._userId = PlayerPrefs.GetString("user_local_ID", "");
                    _userData._userAccessToken = PlayerPrefs.GetString("user_local_token", "");
                    return _userData;
                }
            }
        }
    }

    public string userId
    {
        get
        {
            return this._userId;
        }
    }

    public string userAccessToken
    {
        get
        {
            return this._userAccessToken;
        }
    }

    public string userName
    {
        get
        {
            return this._userName;
        }
        set
        {
            this._userName = value;
            UserPreference.SaveParameter("name", value);
        }
    }

    public static void Init(Facebook.Unity.AccessToken aToken)
    {
        if (_userData == null)
        {
            _userData = UserData.CreateInstance<UserData>();
            _userData._userId = aToken.UserId;
            _userData._userAccessToken = aToken.TokenString;
            PlayerPrefs.SetString("user_local_ID", _userData._userId);
            PlayerPrefs.SetString("user_local_token", _userData._userAccessToken);
            Debug.Log("UserData: Create UserData");
        }
    }

    public void DestroyUserData()
    {
        _userData = null;
        PlayerPrefs.DeleteAll();
        Debug.Log("UserData: Destroy UserData");
    }

    public static bool IsCreateUserData()
    {
        return (userData != null);
    }

}
