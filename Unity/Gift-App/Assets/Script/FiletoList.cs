using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FiletoList : MonoBehaviour {
    public List<string> filetolist()
    {
        StreamReader sr = new StreamReader(@"Assets\Data\categolies_all.md", Encoding.GetEncoding("UTF-8"));
        var catlist = new List<string>();
        while (sr.Peek() != -1){
            catlist.Add(sr.ReadLine());
        }

        sr.Close();
        return catlist;
    }
}
