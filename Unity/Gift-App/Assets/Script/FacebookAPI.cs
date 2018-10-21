using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Linq;

public class FacebookAPI : MonoBehaviour {
    public delegate void FriendsIDCallback(List<string> friendsIDs);
    public delegate void UserProfileCallback(Dictionary<string,string> profiles);

	public static void GetFriendsID(FriendsIDCallback callback)
    {
        if (UserData.IsCreateUserData())
        {
            FB.API("/me/friends", HttpMethod.GET, GraphResult =>
            {
                List<string> friendIDs = ((List<object>)GraphResult.ResultDictionary["data"]).Select(c => ((Dictionary<string,object>)c)["id"].ToString()).ToList();
                callback(friendIDs);
            }, new Dictionary<string, string> { { "access_token", UserData.userData.userAccessToken } });
        }
    }

    public static void GetUserProfile(string UserId, UserProfileCallback callback)
    {
        Debug.Log("GetUserProfile Called");
        if (UserData.IsCreateUserData())
        {
            FB.API("/" + UserId, HttpMethod.GET, GraphResult =>
             {
                 Debug.Log(GraphResult.RawResult);
                 Dictionary<string, string> userProfiles = new Dictionary<string, string>();
                 userProfiles.Add("name", GraphResult.ResultDictionary["name"] as string);
                 userProfiles.Add("gender", GraphResult.ResultDictionary["gender"] as string);
                 userProfiles.Add("birthday", GraphResult.ResultDictionary["birthday"] as string);
                 callback(userProfiles);
             }, new Dictionary<string, string> { { "access_token", UserData.userData.userAccessToken}, { "fields","name,gender,birthday"} });
        }
    }
}
