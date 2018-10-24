using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {
    public int maxSpeed;
    public float speed;
    public int rallyCount = 0;
    public int wallCount = 0;
    public int consecutiveRally = 0;
    public int consecutiveWall = 0;
    public int lastPaddle = 0;
    public int gravScale = 4;
    public float defaultSpawn = 5f;
    public Vector3 ballDirection;

	// Use this for initialization
	void Start ()
    {
        canvasObject = GameObject.FindGameObjectWithTag("GameCanvas");
        RandomStart();
    }

    private void Update()
    {
        if (!paused)
        {
            speed = this.GetComponent<Rigidbody2D>().velocity.magnitude;
            if (speed > maxSpeed)
            {
                Vector3 normalizedVelocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
                this.GetComponent<Rigidbody2D>().velocity = normalizedVelocity * maxSpeed;
                speed = maxSpeed;
            }
        }
    }

    public void UpdateUI()
    {
        Transform textTr = canvasObject.transform.Find("Commentary");
        Text text = textTr.GetComponent<Text>();
        text.text = "\"Game has a Rally Count of " + rallyCount + "\"";
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag.Equals("Wall"))
        {
            wallCount++;
            consecutiveWall++;
            consecutiveRally = 0;
        }
        else if( tag.EndsWith("Paddle"))
        {
            rallyCount++;
            consecutiveRally++;
            consecutiveWall = 0;

            if ( tag.Equals("L Paddle"))
            {
                lastPaddle = 1;
            }
            else
            {
                lastPaddle = 2;
            }
        }
    }

    public void ResetBall()
    {
        GiveCommentary();
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        rallyCount = 0;
        wallCount = 0;
        consecutiveWall = 0;
        consecutiveRally = 0;
        this.GetComponent<Rigidbody2D>().isKinematic = true;

        StartCoroutine(GoalDelay());
    }

    private void GiveCommentary()
    {

    }

    public void RandomStart()
    {
        bool LStart = (Random.value > 0.5f);
        int randVel = Random.Range(5, 10);
        int dir = (LStart) ? -1 : 1;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.transform.position = new Vector3(0, defaultSpawn, 0);
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(dir * randVel, randVel, 0);
    }

    private IEnumerator GoalDelay()
    {
        yield return new WaitForSeconds(delay);
        RandomStart();
    }

    public void HardReset()
    {
        rallyCount = 0;
        this.GetComponent<Rigidbody2D>().gravityScale = gravScale;
        RandomStart();
        //UpdateUI();
    }
    private GameObject canvasObject;
    private bool paused = false;
    private int delay = 2;
}
