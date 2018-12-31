using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ToolCustomGravity : MonoBehaviour {
    // Gravity Scale editable on the inspector
    // providing a gravity scale per object

    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;
    public bool UseGravity;
    Rigidbody m_rb;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }
    void FixedUpdate()
    {
        if (UseGravity)
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.forward;
            m_rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}