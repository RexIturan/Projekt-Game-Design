using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [Serializable]
    private class MyTestClass
    {
        private int zahlen = 0;
        public string name = "Irgendwas";
        public List<int> liste = new List<int>();


        public void add(int zahl)
        {
            liste.Add(zahl);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTestClass test = new MyTestClass();

        test.add(1);
        test.add(2);
        test.add(5);
        test.add(9);
        test.add(10);

        string json = JsonUtility.ToJson(test, true);
        String testst = "test";
        Debug.Log(testst);
        Debug.Log(json);


        string path = null;
#if UNITY_EDITOR
        //TODO muss noch geändert werden
        path = "Assets/Resources/test.json";
#endif
#if UNITY_STANDALONE
        // You cannot add a subfolder, at least it does not work for me
        //TODO muss noch geändert werden
        path = "Assets/Resources/test.json";
#endif
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
            }
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}