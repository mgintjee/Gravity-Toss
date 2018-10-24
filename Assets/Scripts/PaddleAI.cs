using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAI : MonoBehaviour {
    public int speed;
    public float reflect;
    public bool player = false;
    public Vector2 defaultSpawn = new Vector2(10, -7);

    // Use this for initialization
    void Start () {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ball");
        ball = objects[0];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        BoundaryCheck();
        if(!player)
        {
            HandleAIMovement();
        }
    }

    void BoundaryCheck()
    {
        float paddleX = this.transform.position.x;
        if ( this.tag.Equals("L Paddle"))
        {
            if( this.transform.position.x > -innerX)
            {
                this.transform.position = new Vector3(-innerX, defaultSpawn.y, 0);
            }

            if( this.transform.position.x < -outerX)
            {
                this.transform.position = new Vector3(-outerX, defaultSpawn.y, 0);
            }
        }
        else
        {
            if (this.transform.position.x < innerX)
            {
                this.transform.position = new Vector3(innerX, defaultSpawn.y, 0);
            }

            if (this.transform.position.x > outerX)
            {
                this.transform.position = new Vector3(outerX, defaultSpawn.y, 0);
            }
        }

        if (this.transform.position.y != defaultSpawn.y)
        {
            this.transform.position = new Vector3(paddleX, defaultSpawn.y, 0);
        }
    }
    void HandleAIMovement()
    {
        float ballX = ball.transform.position.x;
        float paddleX = this.transform.position.x;
        float tagValue = (this.tag.Equals("L Paddle")) ? -1 : 1;
        float targetX;

        ballCurrentPosition = ball.transform.position;
        Vector3 ballDirection = ballCurrentPosition - ballPreviousPosition;
        float ballDirectionX = ballDirection.x;

        if (tagValue > 0) // Right
        {
            targetX = HandleRTurn(ballDirectionX, ballX, tagValue);
        }
        else // Left
        {
            targetX = HandleLTurn(ballDirectionX, ballX, tagValue);
        }

        if (!WithinRange(targetX, paddleX))
        {
            HandleMovement(targetX, paddleX);
        }
        else
        {
            Move(0);
        }

        ballPreviousPosition = ballCurrentPosition;
    }

    void HandleMovement(float targetX, float paddleX)
    {
        if (targetX > paddleX)
        {
            Move(1);
        }
        else if (targetX < paddleX)
        {
            Move(-1);
        }
    }

    float HandleLTurn(float ballDirectionX, float ballX, float tagValue)
    {
        float targetX = 0f;

        if ((ballDirectionX < 0 && ballCurrentPosition.x < 0) ||
            ballCurrentPosition.x < 5 * tagValue)
        {
            targetX = ballX;
        }
        else
        {
            targetX = defaultSpawn.x;
        }

        return targetX;
    }
    float HandleRTurn(float ballDirectionX, float ballX, float tagValue)
    {
        float targetX = 0f;

        if ( (ballDirectionX > 0 && ballCurrentPosition.x > 0) || 
            ballCurrentPosition.x > 5 * tagValue)
        {
            targetX = ballX;
        }
        else
        {
            targetX = defaultSpawn.x;
        }

        return targetX;
    }
    bool WithinRange(float target, float value)
    {
        return target + margin > value && target - margin < value;
    }
    public void Move( int Direction)
    {
        Transform moveLeftFire = this.transform.Find("move_left_effect");
        Transform moveRightFire = this.transform.Find("move_right_effect");

        if ( Direction > 0)
        {
            moveLeftFire.gameObject.SetActive(false);
            moveRightFire.gameObject.SetActive(true);
        }
        else if( Direction < 0 )
        {
            moveLeftFire.gameObject.SetActive(true);
            moveRightFire.gameObject.SetActive(false);
        }
        else
        {
            moveLeftFire.gameObject.SetActive(false);
            moveRightFire.gameObject.SetActive(false);
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        this.transform.Translate(Direction * Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ball"))
        {
            Vector3 ballVelocity = ball.GetComponent<Rigidbody2D>().velocity;
            ball.GetComponent<Rigidbody2D>().velocity = ballVelocity * reflect;
        }
    }

    private float margin = 0.5f;
    private GameObject ball = null;
    private Vector3 ballCurrentPosition;
    private Vector3 ballPreviousPosition;
    private float innerX = 7f;
    private float outerX = 13f;
}
