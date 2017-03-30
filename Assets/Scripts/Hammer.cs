using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Hit());
        }
    }

    private IEnumerator Hit()
    {
        anim.SetBool("Hitting", true);
        yield return new WaitForSeconds((float)0.2);
        anim.SetBool("Hitting", false);
    }
}
