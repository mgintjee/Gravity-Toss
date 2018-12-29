using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour {

    public int Speed;
    public float Reflect;
    public bool ActivePlayer = false;
    public GameObject ObjectBall;
    public Vector3 SpawnDefault;
    public Vector2 BoundsMovement;

    private float Margin = 1f;
    private Vector3 BallCurrentPosition;
    private Vector3 BallPreviousPosition;

    void Start () {
        SpawnDefault = this.transform.position;
        if(SpawnDefault.x < 0)
            BoundsMovement = new Vector2(-2.5f, 6f);
        else
            BoundsMovement = new Vector2(-6, 2.5f);

    }
	
	void FixedUpdate ()
    {
        BoundaryCheck();
        if (!ActivePlayer)
        {
            HandleAIMovement();
        }
    }

    void HandleAIMovement()
    {
        float ballX = ObjectBall.transform.position.x;
        float paddleX = this.transform.position.x;
        float PaddleSide = (SpawnDefault.x < 0) ? -1 : 1;
        float targetX;

        BallCurrentPosition = ObjectBall.transform.position;
        Vector3 ballDirection = BallCurrentPosition - BallPreviousPosition;
        float ballDirectionX = ballDirection.x;

        if (PaddleSide > 0) // Right
        {
            targetX = HandleRTurn(ballDirectionX, ballX, PaddleSide);
        }
        else // Left
        {
            targetX = HandleLTurn(ballDirectionX, ballX, PaddleSide);
        }

        if (!WithinRange(targetX, paddleX))
        {
            HandleMovement(targetX, paddleX);
        }
        else
        {
            Move(0);
        }

        BallPreviousPosition = BallCurrentPosition;
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

        if ((ballDirectionX < 0 && BallCurrentPosition.x < 0) ||
            BallCurrentPosition.x < 5 * tagValue)
        {
            targetX = ballX;
        }
        else
        {
            targetX = SpawnDefault.x;
        }

        return targetX;
    }
    float HandleRTurn(float ballDirectionX, float ballX, float tagValue)
    {
        float targetX = 0f;

        if ((ballDirectionX > 0 && BallCurrentPosition.x > 0) ||
            BallCurrentPosition.x > 5 * tagValue)
        {
            targetX = ballX;
        }
        else
        {
            targetX = SpawnDefault.x;
        }

        return targetX;
    }
    bool WithinRange(float target, float value)
    {
        return target + Margin > value && target - Margin < value;
    }
    public void Move(int Direction)
    {
        this.transform.Translate(Direction * Vector3.right * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            Vector3 ballVelocity = ObjectBall.GetComponent<Rigidbody2D>().velocity;
            ObjectBall.GetComponent<Rigidbody2D>().velocity = ballVelocity * Reflect;
        }
    }
    private void BoundaryCheck()
    {
        float DistanceFromSpawn = this.transform.position.x - SpawnDefault.x;
        if(DistanceFromSpawn < BoundsMovement.x)
        {
            // Below Lower Bound
            this.transform.position = new Vector3(SpawnDefault.x+BoundsMovement.x, SpawnDefault.y, SpawnDefault.z);
        }
        else if(DistanceFromSpawn > BoundsMovement.y)
        {
            // Above Upper Bound
            this.transform.position = new Vector3(SpawnDefault.x+BoundsMovement.y, SpawnDefault.y, SpawnDefault.z);
        }
    }
    private void ResetPosition()
    {
        this.transform.position = SpawnDefault;
    }
}
