using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiEmitterScript : MonoBehaviour {

    public GameObject ConfettiEmitter;
    public GameObject Confetti;
    public Sprite WhiteSprite;

    public void EmitConfetti()
    {
        for( int i = 0; i < 15; ++i)
        {
            int randVel = Random.Range(5, 10);
            GameObject TemporaryConfettiHandler;
            TemporaryConfettiHandler = Instantiate(Confetti, ConfettiEmitter.transform.position, ConfettiEmitter.transform.rotation);
            TemporaryConfettiHandler.GetComponent<SpriteRenderer>().sprite = WhiteSprite;
            TemporaryConfettiHandler.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f,1f,1f,1f,1f,1f);
            Rigidbody2D TemporaryRigidBody2D;
            TemporaryRigidBody2D = TemporaryConfettiHandler.GetComponent<Rigidbody2D>();
            TemporaryRigidBody2D.velocity = new Vector3(randVel, randVel, 0);
            Destroy(TemporaryConfettiHandler, 2f);
        }
    }
}
