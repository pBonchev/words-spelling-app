using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RandomizeWordText : MonoBehaviour
{
    private void Start()
    {
        TextAsset txt = (TextAsset)Resources.Load("WordList", typeof(TextAsset));
        string[] content = txt.text.Split('\n');

        for (int i = 0; i < 10000; i++)
        {
            int x = Random.Range(0, content.Length);
            int y = Random.Range(0, content.Length);
            string z = content[x];
            content[x] = content[y];
            content[y] = z;
        }

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/wordlist.txt");

        for (int i = 0; i < content.Length; i++)
            writer.Write(content[i]);

        writer.Close();
    }
}
