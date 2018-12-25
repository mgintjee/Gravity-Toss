using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour {

    public GameObject Ball;
    public Vector3 SpawnDefault;
    public Vector2 BoundsMovement = new Vector2(-6,2.5f);

    // Use this for initialization
    void Start () {
        SpawnDefault = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        BoundaryCheck();

    }

    private void BoundaryCheck()
    {
        float DistanceFromSpawn = this.transform.position.x - SpawnDefault.x;
        if(DistanceFromSpawn < -6)
        {
            // Below Lower Bound
            Debug.Log("Lower Bound: ");
        }
        else if(DistanceFromSpawn > 2.5)
        {
            // Above Upper Bound
            Debug.Log("Upper Bound: ");
        }
    }
    private void ResetPosition()
    {
        this.transform.position = SpawnDefault;
    }
}
