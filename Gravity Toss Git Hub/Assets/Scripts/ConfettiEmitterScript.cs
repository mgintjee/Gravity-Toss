using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiEmitterScript : MonoBehaviour {

    public GameObject ConfettiEmitter;
    public GameObject Confetti;
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
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

    private Sprite RandomSprite( int color)
    {
        switch (color)
        {
            case 1:
                return sprite1;
            case 2:
                return sprite2;
            case 3:
                return sprite3;
            case 4:
                return sprite4;
            case 5:
                return sprite5;

            default:
                return sprite0;
        }
    }
}
