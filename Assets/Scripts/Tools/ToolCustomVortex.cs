using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCustomVortex : MonoBehaviour {

    private float ConfettiVortexPull = 2;
    private float BallVortexPull = 1;
    private float UpperConfettiRadius = 5;
    private float LowerConfettiRadius = 1;
    private float BallRadius = 3;


    public bool UseGravity;
    Rigidbody m_rb;

    void FixedUpdate()
    {
        if(GameObject.Find("Confetti"))
            ApplyPullOnConfetti();
    }

    private void ApplyPullOnConfetti()
    {
        Vector3 Center = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(Center, UpperConfettiRadius);
        foreach (Collider hit in colliders)
        {
            Vector3 DistanceVector = this.transform.position - hit.transform.position;
            float Distance = DistanceVector.magnitude;
            if (hit && hit.name.Equals("Confetti") && Distance > LowerConfettiRadius && hit.transform != this.transform && hit.GetComponent<Rigidbody>())
            {
                hit.GetComponent<Rigidbody>().AddForce(DistanceVector.normalized * ConfettiVortexPull, ForceMode.Force);
            }
        }
    }
}