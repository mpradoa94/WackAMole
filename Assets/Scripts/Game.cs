using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public GameObject[] holes;
    //Prefabs for levels
    public Level[] levels;
    //Level Duration
    public float levelTime;
    //delay for before and after a level
    public float delay;
    //GUI texts
    public Text scoreTxt;
    public Text negativeScore;
    public Text levelTxt;
    //Spawn Interval
    public float minTime;
    public float maxTime;

    private int currentLevel = 0;
    private float time;
    private bool levelStarted = false;

    public static int score = 0;
    public static int failHits = 0;
    
    private float actualTime;

    //Reaction times
    public static List<float> timeCorrect;
    public static List<float> timeIncorrect;

    private int numberOfMolesSpawned;
    private int numberOfRabbitsSpawned;

    // Use this for initialization
    void Start () {
        StartCoroutine(MainGame());
        timeCorrect = new List<float>();
        timeIncorrect = new List<float>();

        //Initialize some control variables to keep the right data
        DataControl.control.numClicks = 0;
        if(DataControl.control.game == null)
            DataControl.control.game = this;
        int levelLenght = levels.Length;
        DataControl.control.levelScore = new int [levelLenght];
        DataControl.control.levelFails = new int[levelLenght];
        DataControl.control.moles = new int[levelLenght];
        DataControl.control.rabbits = new int[levelLenght];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (levelStarted)
        {
            time += Time.deltaTime;
            scoreTxt.text = "Correctas: " + score;
            if (failHits > 0)
                negativeScore.text = "Errores: " + failHits;
            else
                negativeScore.text = "Errores: 0";
        }
        else
        {
            scoreTxt.text = "";
            negativeScore.text = "";
        }
    }

    private IEnumerator MainGame()
    {
        yield return StartCoroutine(BeginLoop());
        yield return StartCoroutine(LevelLoop());
        yield return StartCoroutine(EndLoop());

        if(currentLevel >= levels.Length)
        {
            levelTxt.text = "Guardando puntuacion...";
            DataControl.control.Save();
            SceneManager.LoadScene("endScene");
        }
        else
        {
            //Start next level
            StartCoroutine(MainGame());
        }
    }

    private IEnumerator BeginLoop()
    {
        time = 0.0f;
        levelTxt.text = "Nivel " + (currentLevel + 1);
        // Wait some time to begin level.
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator LevelLoop()
    {
        GameObject randHole;
        GameObject randAnimal;
        int randAux;
        float auxTime = maxTime;

        levelTxt.text = "";
        levelStarted = true;

        while (time < levelTime)
        {   
            //Get randoms for spawn
            randAux = Random.Range(0, holes.Length);
            randHole = holes[randAux]; 
            randAux = Random.Range(0, levels[currentLevel].prefabs.Length);
            randAnimal = levels[currentLevel].prefabs[randAux];

            if (PosIsEmpty(randHole.transform))
            {
                if (randAnimal.tag == "Bird")
                {
                    Spawn(randAnimal, new Vector3(17, 0, 0));
                    auxTime = minTime;
                }
                else if (randAnimal.tag == "Animal")
                {
                    Spawn(randAnimal, new Vector3(randHole.transform.position.x, (float)-2.4, 0));
                    auxTime = maxTime;
                    string aux = randAnimal.GetComponent<Animal>().type;
                    if (aux == "Rabbit")
                    {
                        DataControl.control.rabbits[currentLevel]++;
                    }
                    else if(aux == "Mole")
                    {
                        DataControl.control.moles[currentLevel]++;
                    }
                }
                else
                {
                    auxTime = minTime;
                    Spawn(randAnimal, new Vector3(0, 0, 0));
                }
            }

            //Wait some time before spawning new mole, rabbit or bird
            yield return new WaitForSeconds(Random.Range(minTime, auxTime));
        }
        
        yield return null;
    }

    private IEnumerator EndLoop()
    {
        DataControl.control.levelScore[currentLevel] = score;
        DataControl.control.levelFails[currentLevel] = failHits;
        score = 0;
        failHits = 0;
        levelStarted = false;
        currentLevel++;
        // Wait some time to start next level.
        yield return new WaitForSeconds(delay);
    }

    void Spawn(GameObject animal, Vector3 pos)
    {
        Instantiate(animal, pos, Quaternion.identity);

    }

    bool PosIsEmpty(Transform pos)
    {
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject current in animals)
        {
            if (current.transform.position.x == pos.position.x)
                return false;
        }
        return true;
    }
}


