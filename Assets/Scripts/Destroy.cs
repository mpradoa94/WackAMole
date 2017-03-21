using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public AudioClip clip;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitForPlay());
    }
	
    private IEnumerator WaitForPlay()
    {
        yield return new WaitForSeconds(clip.length);
        Destroy(gameObject);
    }
}
