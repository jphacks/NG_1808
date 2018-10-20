using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneTransferer : MonoBehaviour
{
    public void TransitTo(string nextScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
