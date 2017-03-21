using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class LoadInfo : MonoBehaviour {

    public Text scoreTxt;
    public Text wrongTxt;
    public Text reactionTime;

    private int score;
    private int failHits;
    private float reactionCorrect;

    void Start()
    {
        if (Load())
        {
            scoreTxt.text += " " + score;
            wrongTxt.text += " " + failHits;
            reactionTime.text += " " + reactionCorrect.ToString("0.00"); ;

        }
        else
        {
            scoreTxt.text = "No hay información de juego";
            wrongTxt.text = "";
            reactionTime.text = "";

        }
    }

    public bool Load()
    {
        PlayerData data = null;
        string path = "./currentPlayerInfo.json";
        if (File.Exists(path))
        {
            //FileStream file = File.Open("./currentPlayerInfo.json", FileMode.Open);
            data = JsonUtility.FromJson<PlayerData>(File.ReadAllText(path));
            //file.Close();

            score = data.score;
            failHits = data.failHits;
            reactionCorrect = data.reactionCorrect;
            return true;
        }
        return false;
    }
}
