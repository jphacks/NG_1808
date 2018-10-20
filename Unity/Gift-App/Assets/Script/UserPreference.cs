using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class UserPreference
{
    #region Public Fields

    public static readonly string ClassName = "UserPreference";

    #endregion

    #region Private Fields

    public string id;
    public string name;
    public string token;
    public string json;
    NCMBObject obj;

    #endregion

    #region Public Properties

    public string ObjectId
    {
        get { return obj.ObjectId; }
    }

    #endregion

    #region Delegate

    public delegate void NCMBFindCallback(UserPreference userPreference);

    #endregion

    #region Static Methods

    public static void GetPreferencefromID(string userId, NCMBFindCallback callback)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(ClassName);
        query.WhereEqualTo("id", userId);
        query.FindAsync((List<NCMBObject> objlist, NCMBException e) =>
        {
            if(e == null)
            {
                NCMBObject obj = objlist[0];
                UserPreference userPreference = new UserPreference(obj["id"].ToString(), obj["name"].ToString(), obj["AccessToken"].ToString(), obj["json"].ToString());
                callback(userPreference);
            }
        });
    }

    public static void SaveParameter(string parameter, string value)
    {
        NCMBObject obj = new NCMBObject(ClassName);
        obj.ObjectId = PlayerPrefs.GetString("ObjectID");
        obj[parameter] = value;
        obj.SaveAsync();
    }

    #endregion

    #region Constructors

    public UserPreference(string id, string name, string token, string json, string objid = null)
    {

        this.id = id;
        this.name = name;
        this.token = token;
        this.json = json;
        this.obj = new NCMBObject(ClassName);
        if (objid == null)
        {
            this.AssignObjectId();
        }
        else
        {
            this.obj.ObjectId = objid;
        }
    }

    #endregion

    #region Public Methods

    public void Save()
    {
        NCMBObject obj = new NCMBObject(ClassName);
        obj["id"] = this.id;
        obj["name"] = this.name;
        obj["token"] = this.token;
        obj["json"] = this.json;
        obj.SaveAsync((NCMBException e) =>
        {
            if (e == null)
            {
                Debug.Log("保存に成功しました。オブジェクトid: " + obj.ObjectId);
                PlayerPrefs.SetString("ObjectID", obj.ObjectId);
            }
            else
            {
                Debug.LogError("保存に失敗しました。エラーコード: " + e.ErrorCode);
            }
        });
    }

    public void Print()
    {
        var texts = new List<string>()
        {
            this.id.ToString(), this.name.ToString(), this.token.ToString(), this.json.ToString()
        };

        foreach (var t in texts)
        {
            Debug.Log(t);
        }
    }

    #endregion

    #region Private Methods

    private void AssignObjectId()
    {
        var query = new NCMBQuery<NCMBObject>(ClassName);
        query.WhereEqualTo("id", this.id);

        query.FindAsync((List<NCMBObject> objs, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogError("データの検索に失敗しました。エラーコード: ");
                return;
            }

            if (objs.Count != 0)
            {
                Debug.Log("このidは既に存在しているためObjectIdを同期します。");
                this.id = objs[0].ObjectId;
            }
        });
    }

    #endregion
}
