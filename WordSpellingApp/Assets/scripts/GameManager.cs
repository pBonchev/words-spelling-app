using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField wordInput;
    [SerializeField]
    private TMP_Text wordNow;
    [SerializeField]
    private TMP_Text infoText;

    [SerializeField]
    private GameObject wordScreen;
    [SerializeField]
    private GameObject statScreen;

    [SerializeField]
    private WordInfo[] infos;

    private DateState dateState;

    private string[] data;
    private int currentWordIdx;
    private List<Pair> stats = new List<Pair>();

    private System.DateTime beginDate = new System.DateTime(2022, 7, 21);

    [SerializeField]
    private int wordsPerDay = 5;

    private void Awake()
    {
        dateState = GetComponent<DateState>();

        data = new string[wordsPerDay];
    }

    void Start()
    {
        if(dateState.SolvedToday())
        {
            ShowScore();
        }
        else
        {
            dateState.LoadStats();

            currentWordIdx = stats.Count;
            infoText.text = (stats.Count + 1) + "/" + data.Length;

            FillData();
            ShowWord();
        }

    }

    private void OnApplicationQuit()
    {
        dateState.SaveStats(stats);
    }

    public void NextWord()
    {
        SaveWord();

        if (currentWordIdx == data.Length - 1)
        {
            dateState.SaveStats(stats);
            ShowScore();
            return;
        }

        currentWordIdx++;

        infoText.text = (currentWordIdx + 1) + "/" + data.Length;

        ShowWord();
    }

    public void ChangeWordNow()
    {
        if (wordInput.text == "") return;

        wordNow.text = wordInput.text;
    }

    private void ShowWord()
    {
        wordInput.text = "";
        wordNow.text = DataWord(currentWordIdx);
        
        statScreen.SetActive(false);
        wordScreen.SetActive(true);
    }

    private void SaveWord()
    {
        stats.Add(new Pair(wordNow.text, DataWrightWord(currentWordIdx)));
    }

    private void ShowScore()
    {
        dateState.AddToday();

        statScreen.SetActive(true);
        wordScreen.SetActive(false);

        stats = dateState.LoadStats();

        for (int i = 0; i < wordsPerDay; i++)
        {
            if (stats[i].first == stats[i].second) infos[i].C(stats[i].second);
            else infos[i].W(stats[i].first, stats[i].second);
        }
    }

    private string DataWrightWord(int idx)
    {
        string ans = "";

        int i = 0;

        while (data[idx][i] != '=' && i < data[idx].Length) 
        {
            ans = ans + data[idx][i];
            i++;
        }

        return ans.Substring(0, ans.Length - 1);
    }

    private string DataWord(int idx)
    {
        int skip = DataWrightWord(idx).Length;

        return data[idx].Substring(skip + 3, data[idx].Length - skip - 4);
    }

    private void FillData()
    {
        TextAsset txt = (TextAsset)Resources.Load("WordList", typeof(TextAsset));
        string[] content = txt.text.Split('\n');

        int diff = (int)((System.TimeSpan)(System.DateTime.Now - beginDate)).TotalDays;

        diff %= (content.Length / wordsPerDay);

        for (int i = 0; i < wordsPerDay; i++)
        {
            data[i] = content[wordsPerDay * diff + i];
        }
    }
}

public class Pair
{
    public string first;
    public string second;

    public Pair()
    {
        ;
    }

    public Pair(string x, string y)
    {
        first = x;
        second = y;
    }
}
