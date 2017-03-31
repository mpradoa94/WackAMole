using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {

    public string type;
    public float lifeTime;
    private bool whacked;

    //sound
    public AudioClip clip;
    public AudioSource audio;

    //Time to hit
    private float time;

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        whacked = false;
        StartCoroutine(MainLoop());
    }

    void OnMouseDown()
    {
        if (DataControl.control.timeCorrect != null)
        {
            if (type == "Mole")
            {
                DataControl.control.timeCorrect.Add(time);
            }
            else if (type == "Rabbit")
            {
                DataControl.control.timeIncorrect.Add(time);
            }
        }
        Whack();
    }

    // Main loop for the sprite.
    private IEnumerator MainLoop()
    {
        yield return StartCoroutine(WaitForHit());
        Destroy(gameObject);
    }

    // Give the player time to hit the mole.
    private IEnumerator WaitForHit()
    {
        time = 0.0f;
        while (!whacked && time < lifeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    // Mole has been hit
    public void Whack()
    {
        if (type == "Mole")
        {
            Game.score++;
            //animation
            StartCoroutine(DeathAnim());
            //instantiate sound TO DO
        }
        else if (type == "Rabbit")
        {
            Game.failHits++;
            //animation
            StartCoroutine(DeathAnim());
        }      
    }

    private IEnumerator DeathAnim()
    {
        if(audio != null)
            audio.PlayOneShot(clip, 0.7F);
        anim.SetBool("Wacked", true);
        yield return new WaitForSeconds((float)0.3);
        whacked = true;
    }
}
