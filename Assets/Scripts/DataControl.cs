﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class DataControl : MonoBehaviour {

    public static DataControl control;

    //Gamer data
    public int[] levelScore;
    public int[] levelFails;
    public int[] moles;
    public int[] rabbits;
    //Reaction times
    public List<float> timeCorrect;
    public List<float> timeIncorrect;
    public int numClicks;
    public string playerName;

    public Game game = null;

    void Awake () {
	    if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            numClicks++;
        }
	}

    public void Save()
    {
        float averageTimeCorrect = 0;
        float averageTimeIncorrect = 0;
        string path1 = "./currentPlayerInfo.json";
        string path2 = "./Data/playerInfo" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".json";

        FileStream file = File.Create(path1);
        FileStream file2 = File.Create(path2);

        PlayerData data = new PlayerData();
        data.playerName = playerName;
        Array.ForEach(levelScore, delegate (int i) { data.score += i; });
        Array.ForEach(levelFails, delegate (int i) { data.failHits += i; });
        data.levelScore = levelScore;
        data.levelFails = levelFails;
        data.moles = moles;
        data.rabbits = rabbits;
        data.numClicks = numClicks;
        for (int i = 0; i < timeCorrect.Count; i++)
        {
            averageTimeCorrect += timeCorrect[i];
        }
        for (int i = 0; i < timeIncorrect.Count; i++)
        {
            averageTimeIncorrect += timeIncorrect[i];
        }

        data.reactionCorrect = averageTimeCorrect/timeCorrect.Count;
        data.reactionIncorrect = averageTimeIncorrect/timeIncorrect.Count;

        string json = JsonUtility.ToJson(data);
        file.Dispose();
        file2.Dispose();
        File.WriteAllText(path1, json);
        File.WriteAllText(path2, json);

        file.Close();
        file2.Close();
    }
}

[Serializable]
class PlayerData
{
    public string playerName;
    public int score;
    public int failHits;
    public int[] levelScore;
    public int[] levelFails;
    public int[] moles;
    public int[] rabbits;
    public int numClicks;    
    public float reactionCorrect;
    public float reactionIncorrect; 
}
