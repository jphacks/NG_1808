using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.MiniJSON;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour {
    [SerializeField]
    CategoryDatabase categoryDatabase;

    [SerializeField]
    OptionsComponent LeftButton;
    string leftID;

    [SerializeField]
    OptionsComponent RightButton;
    string rightID;

    public enum CategoryChoice { Left, Right, Neither, No};

    CategoryChoice currentChoice = CategoryChoice.No;

    [SerializeField]
    int QuestionCount = 10;
    [SerializeField]
    int HigherRanking = 5;
    [SerializeField]
    int HigherQuestion = 5;

    static QuestionManager _instance;

    public static QuestionManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.Find("QuestionManageObject").GetComponent<QuestionManager>();
                return _instance;
            }else
            {
                return _instance;
            }
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(SetQuestions());
	}

    IEnumerator SetQuestions()
    {
        for(int i=0; i< QuestionCount; i++)
        {
            SetTwoCategory(ref LeftButton, ref RightButton, i);

            while (currentChoice == CategoryChoice.No) yield return null;

            AddCategoryScore(currentChoice);
            ResetProperty();

            yield return null;
        }
        string json = Json.Serialize(categoryDatabase.categoryDictionary);
        Debug.Log(json);
        PlayerPrefs.SetString("LikeJson", json);
        UserPreference userPreference = new UserPreference(UserData.userData.userId, UserData.userData.userName, UserData.userData.userAccessToken, json);
        userPreference.Save();
        SceneManager.LoadScene("Send");
    }

    public void AddCategoryScore(CategoryChoice currentChoice)
    {
        int difference = categoryDatabase.categoryDictionary[leftID] - categoryDatabase.categoryDictionary[rightID];
        int dif = Mathf.Abs(difference);
        switch (currentChoice)
        {
            case CategoryChoice.Left:
                if (difference > 0)
                    categoryDatabase.categoryDictionary[leftID] += 1;
                else if (difference == 0)
                    categoryDatabase.categoryDictionary[leftID] += 1;
                else if (difference < 0)
                    categoryDatabase.categoryDictionary[leftID] += dif + 1;
                break;
            case CategoryChoice.Right:
                if (difference > 0)
                    categoryDatabase.categoryDictionary[rightID] += dif + 1;
                else if (difference == 0)
                    categoryDatabase.categoryDictionary[rightID] += 1;
                else if (difference < 0)
                    categoryDatabase.categoryDictionary[rightID] += 1;
                break;
            case CategoryChoice.Neither:
                break;
        }
    }

    public void SetTwoCategory(ref OptionsComponent left, ref OptionsComponent right, int question_number)
    {
        List<string> sortedList = categoryDatabase.categoryList.OrderByDescending(a => categoryDatabase.categoryDictionary[a]).ToList();
        if (question_number < HigherQuestion)
        {
            do
            {
                leftID = sortedList[Random.Range(0, HigherRanking)];
                rightID = sortedList[Random.Range(0, HigherRanking)];
            } while (leftID == rightID);
        }
        else
        {
            do
            {
                string higherID = sortedList[Random.Range(0, HigherRanking)];
                string lowerID = sortedList[Random.Range(HigherRanking, sortedList.Count)];
                List<string> horl = new List<string> { higherID, lowerID };
                int randhorl = Random.Range(0, 2);
                leftID = horl[randhorl];
                horl.RemoveAt(randhorl);
                rightID = horl[0];
            } while (leftID == rightID);
        }

        left.SetButtonText(leftID);
        right.SetButtonText(rightID);
        left.SetButtonSprite(Sprite.Create(categoryDatabase._categoryList.Where(c => c.categoryName == leftID).ToList()[0].categoryImage, new Rect(0, 0, 64, 64), Vector2.zero));
        right.SetButtonSprite(Sprite.Create(categoryDatabase._categoryList.Where(c => c.categoryName == rightID).ToList()[0].categoryImage, new Rect(0, 0, 64, 64), Vector2.zero));
    }

    public void GetChoiceButton(CategoryChoice choice)
    {
        currentChoice = choice;
    }

    void ResetProperty()
    {
        leftID = "";
        rightID = "";
        currentChoice = CategoryChoice.No;
    }

    public void ResetSaveData()
    {
        PlayerPrefs.DeleteKey("LikeJson");
    }
}
