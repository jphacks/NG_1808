using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Linq;

public class FacebookLogin : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        FBInitialize();
    }

    public void LoginButton()
    {
        if (!FB.IsLoggedIn && !UserData.IsCreateUserData())
        {
            var list = new List<string> { "public_profile", "user_friends", "user_birthday", "user_gender" };
            FB.LogInWithReadPermissions(list, result =>
            {
                var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
                var userId = aToken.UserId;
                var Token = aToken.TokenString;

                Debug.Log("Facebook Login UserId: " + userId);
                Debug.Log("Access Token: " + Token);
                Debug.Log("Expiration Time: " + aToken.ExpirationTime.ToLocalTime().ToString());

                UserData.Init(aToken);
            });

        }
        else
        {
            Debug.Log("Facebook: Already have user_information");
        }
    }

    public void GetFriends()
    {
        if (UserData.IsCreateUserData())
        {
            FB.API("/me/friends", HttpMethod.GET, GraphResult =>
            {
                ((List<object>)GraphResult.ResultDictionary["data"]).ForEach(c => { Debug.Log(((Dictionary<string, object>)c)["name"]); });
            }, new Dictionary<string, string> { { "access_token", UserData.userData.userAccessToken } });
        }
    }

    public void LogoutButton()
    {
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
            Debug.Log("Facebook: User Logout");
        }else
        {
            Debug.Log("Facebook: Please Login");
        }
        UserData.userData.DestroyUserData();
    }

    private void FBInitialize()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
            },
            isShown=> {

            });
        }
        else
        {
            FB.ActivateApp();
        }
    }
}
