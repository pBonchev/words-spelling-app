using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DateState : MonoBehaviour
{
    private void Awake()
    {
        if (!File.Exists(Application.persistentDataPath + "/dates.txt"))
        {
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/dates.txt");
            writer.Close();
        }
    }

    public bool SolvedToday()
    {
        //return false;//

        string date = System.DateTime.Now.Day + " " + System.DateTime.Now.Month + " " + System.DateTime.Now.Year;

        StreamReader reader = new StreamReader(Application.persistentDataPath + "/dates.txt");

        while (!reader.EndOfStream)
        {
            if (date == reader.ReadLine())
            {
                reader.Close();
                return true;
            }
        }

        reader.Close();
        return false;
    }

    public void AddToday()
    {
        if (SolvedToday()) return;

        string date = System.DateTime.Now.Day + " " + System.DateTime.Now.Month + " " + System.DateTime.Now.Year;

        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/dates.txt", true);

        writer.WriteLine(date);

        writer.Close();
    }

    public void SaveStats(List<Pair> stats)
    {
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/stats.txt");

        for (int i = 0; i < stats.Count; i++)
        {
            writer.WriteLine(stats[i].first);
            writer.WriteLine(stats[i].second);
        }

        writer.Close();
    }

    public List<Pair> LoadStats()
    {
        //return new List<Pair>();

        if (!File.Exists(Application.persistentDataPath + "/stats.txt")) return new List<Pair>();

        List<Pair> ret = new List<Pair>();

        StreamReader reader = new StreamReader(Application.persistentDataPath + "/stats.txt");

        while (!reader.EndOfStream)
            ret.Add(new Pair(reader.ReadLine(), reader.ReadLine()));

        reader.Close();

        return ret;
    }
}
