using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class InitializeFacebook : MonoBehaviour {

    private void Awake()
    {
        FBInitialize();
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

                if (UserData.IsCreateUserData())
                {
                    SceneManager.LoadScene("Send");
                }
                else
                {
                    SceneManager.LoadScene("Facebook");
                }
            },
            isShown => {

            });
        }
        else
        {
            FB.ActivateApp();
        }
    }
}
