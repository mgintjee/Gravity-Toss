using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPaddleAI : MonoBehaviour {

    public int Speed;
    public float Reflect;
    public bool ActivePlayer = false;
    public GameObject ObjectBall;
    public Vector3 SpawnDefault;
    public Vector2 BoundsMovement;
    public float TargetX;

    public void Start () {
        SpawnDefault = this.transform.position;
        if(SpawnDefault.x < 0)
            BoundsMovement = new Vector2(-BoundsMovement.y, -BoundsMovement.x);
    }
	public void FixedUpdate ()
    {
        BoundaryCheck();
        if (!ActivePlayer)
        {
            HandleAIMovement();
        }
    }

    public void ResetPosition()
    {
        this.transform.position = SpawnDefault;
    }
    public void Move(int Direction)
    {
        if (Direction != 0)
            this.transform.Translate(Direction * Vector3.right * Speed * Time.deltaTime);
        else
            this.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void UpdateTextForPlayer()
    {
        this.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = "Player";
    }
    public void UpdateTextForAI()
    {
        this.transform.Find("Canvas").Find("Text").GetComponent<Text>().text = "A.I.";
    }
    private void HandleAIMovement()
    {
        float ballX = ObjectBall.transform.position.x;
        float PaddleX = this.transform.position.x;
        float PaddleSide = (SpawnDefault.x < 0) ? -1 : 1;

        BallCurrentPosition = ObjectBall.transform.position;
        Vector3 BallDirection = BallCurrentPosition - BallPreviousPosition;

        if (PaddleSide > 0) // Right
        {
            TargetX = HandleRTurn(BallDirection.x, ballX, PaddleSide);
        }
        else // Left
        {
            TargetX = HandleLTurn(BallDirection.x, ballX, PaddleSide);
        }

        bool WithinMargin = WithinRange(TargetX, PaddleX);

        if (!WithinMargin)
        {
            HandleMovement(TargetX, PaddleX);
        }
        else
        {
            Move(0);
        }

        BallPreviousPosition = BallCurrentPosition;
    }
    private void HandleMovement(float targetX, float paddleX)
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
    private float HandleLTurn(float ballDirectionX, float ballX, float tagValue)
    {
        float targetX = 0f;

        if ((ballDirectionX < 0 && BallCurrentPosition.x < 0) ||
            BallCurrentPosition.x < 0.5 * tagValue)
        {
            targetX = ballX;
        }
        else
        {
            targetX = SpawnDefault.x;
        }

        return targetX;
    }
    private float HandleRTurn(float ballDirectionX, float ballX, float tagValue)
    {
        float targetX = 0f;

        if ((ballDirectionX > 0 && BallCurrentPosition.x > 0) ||
            BallCurrentPosition.x > 0.5 * tagValue)
        {
            targetX = ballX;
        }
        else
        {
            targetX = SpawnDefault.x;
        }

        return targetX;
    }
    private bool WithinRange(float target, float value)
    {
        return( target + Margin > value && target - Margin < value);
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

    private float Margin = 0.25f;
    private Vector3 BallCurrentPosition;
    private Vector3 BallPreviousPosition;
}
