using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBall : MonoBehaviour {
    public int MaxSpeed = 25;
    public float Speed;
    public int RallyCount = 0;
    public int WallCount = 0;
    public int ConsecutiveRally = 0;
    public int ConsecutiveWall = 0;
    public int LastPaddle = 0;
    public int GravScale = 4;
    public float DefaultSpawn = 5f;
    public Vector3 BallDirection;
    public Vector3 SpawnDefault;
    public int GoalLine = -4;

    // Use this for initialization
    void Start ()
    {
        SpawnDefault = this.transform.position;
        canvasObject = GameObject.FindGameObjectWithTag("GameCanvas");
        HardReset();
    }

    private void Update()
    {
        if (!paused)
        {
            Speed = this.GetComponent<Rigidbody>().velocity.magnitude;
            if (Speed > MaxSpeed)
            {
                Vector3 normalizedVelocity = this.GetComponent<Rigidbody>().velocity.normalized;
                this.GetComponent<Rigidbody>().velocity = normalizedVelocity * MaxSpeed;
                Speed = MaxSpeed;
            }
        }
    }

    public void UpdateUI()
    {
        Transform textTr = canvasObject.transform.Find("Commentary");
        Text text = textTr.GetComponent<Text>();
        text.text = "\"Game has a Rally Count of " + RallyCount + "\"";
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject Object = collision.gameObject;
        string ObjectName = Object.name;
        //Debug.Log(ObjectName);
        switch (ObjectName)
        {
            case "ObjectBarrierGoal":
                break;
            case "ObjectBarrierBack":
                break;
            case "ObjectPaddle":
                CollisionWithPaddle(Object);
                break;
            case "Confetti":
                break;
            case "Emitter":
                break;
            default:
                break;
                // Should Never Get Here
        }
        /*
        if(tag.Equals("Wall"))
        {
            WallCount++;
            ConsecutiveWall++;
            ConsecutiveRally = 0;
        }
        else if( tag.EndsWith("Paddle"))
        {
            RallyCount++;
            ConsecutiveRally++;
            ConsecutiveWall = 0;

            if ( tag.Equals("L Paddle"))
            {
                LastPaddle = 1;
            }
            else
            {
                LastPaddle = 2;
            }
        }
        */
    }
    private void CollisionWithPaddle(GameObject Paddle)
    {
        float ReflectPower = Paddle.GetComponent<ObjectPaddleAI>().Reflect;
        this.GetComponent<Rigidbody>().velocity *= ReflectPower;
    }
    public void ResetBall()
    {
        GiveCommentary();
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        RallyCount = 0;
        WallCount = 0;
        ConsecutiveWall = 0;
        ConsecutiveRally = 0;

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
        this.transform.position = SpawnDefault;
        this.GetComponent<Rigidbody>().velocity = new Vector3(dir * randVel, randVel, 0);
    }

    private IEnumerator GoalDelay()
    {
        yield return new WaitForSeconds(delay);
        RandomStart();
    }

    public void HardReset()
    {
        RallyCount = 0;
        //this.GetComponent<Rigidbody>(). = GravScale;
        RandomStart();
        //UpdateUI();
    }
    private GameObject canvasObject;
    private bool paused = false;
    private int delay = 2;
}
