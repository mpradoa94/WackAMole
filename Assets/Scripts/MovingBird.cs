using UnityEngine;
using System.Collections;

public class MovingBird : MonoBehaviour {

    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude;
    public Sprite sprite;
 
    private Vector3 tempPosition;

    void Start () 
    {
        //Random color
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        // apply it on current object's material
        gameObject.GetComponent<SpriteRenderer>().color = newColor;
        
        tempPosition = transform.position;
    }
 
    void FixedUpdate () 
    {
        tempPosition.x += horizontalSpeed;
        tempPosition.y = 1+ Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed)* amplitude;
        transform.position = tempPosition;
        if (transform.position.x <= -17)
            Destroy(this.gameObject);
    }
}