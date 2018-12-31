using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCustomVortex : MonoBehaviour {
    public GameObject Ball;
    public float Radius = 4;
    public float VortexPull = 5;
    public Vector3 ForceVector;
    public bool ApplyForce;

    private void Start()
    {
        Ball = GameObject.Find("ObjectBall");
    }

    void FixedUpdate()
    {
        ApplyForce = WithinRadius();
        if (WithinRadius())
            ApplyForceOnBall();
    }
    private void ApplyForceOnBall()
    {
        Vector3 difference = this.transform.position - Ball.transform.position;
        ForceVector = difference;
        Ball.GetComponent<Rigidbody>().AddForce(difference.normalized * VortexPull, ForceMode.Force);
    }
    private bool WithinRadius()
    {
        float Distance = (Ball.transform.position - this.transform.position).magnitude;
        return Distance < Radius;
    }
}