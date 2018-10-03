using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleTree : MonoBehaviour
{

    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;

    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    [Header("Set Dynamically")]
    public Text scoreGT;
    // Track how many apples have been dropped
    public int applesDropped = 0;

    // Use this for initialization
    void Start()
    {
        // Dropping apples every second
        Invoke("DropApple", 2f);

        // Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        // Get the Text Component of that GameObject
        scoreGT = scoreGO.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);       // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);      // Move left
        }
    }

    void FixedUpdate()
    {
        // Changing direction randomly is now time-based because of FixedUpdate ()
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;                    // Change direction
        }
    }

    void DropApple()
    {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        int score = int.Parse(scoreGT.text);
        var appleScript = apple.GetComponent<Apple>();
        if (applesDropped % 3 == 0 && applesDropped > 5)
        {
            apple.gameObject.tag = "FastApple";
            appleScript.thrust = score / 200;
        }
		apple.transform.position = transform.position;
		Invoke("DropApple", secondsBetweenAppleDrops);
        applesDropped++;
    }
}
