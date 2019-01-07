using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfettiEmitter : MonoBehaviour {

    private GameObject[] GameObjectsConfetti;
    private Material[] MaterialsConfetti;
    private int EmitCount = 11;
    private float DistanceFromEmitter = 1.5f;

	// Use this for initialization
	void Start ()
    {
        Random.InitState(0);
        string PathToPrefabs = "Prefabs/Confetti/";
        string PathToMaterials = "Materials/Confetti/";
        GameObjectsConfetti = Resources.LoadAll<GameObject>(PathToPrefabs);
        MaterialsConfetti = Resources.LoadAll<Material>(PathToMaterials);
    }

    public void EmitAllConfetti()
    {
        float AngleToEmit = 180 / (EmitCount + 1);
        for (int i = 0; i < EmitCount; ++i)
        {
            EmitSingleConfetti(AngleToEmit * (i + 1));
        }
    }

    public void EmitSingleConfetti(float Angle)
    {
        float RandomVelocity = Random.Range(10f, 25f);
        int RandomPrefabIndex = Random.Range(0, GameObjectsConfetti.Length);
        int RandomMaterialIndex = Random.Range(0, MaterialsConfetti.Length);
        float RandomDuration = Random.Range(1f, 2f);
        float RandomScale = Random.Range(0.04f, 0.06f);
        
        GameObject TemporaryConfettiHandler;
        Vector3 ConfettiPosition = RandomConfettiPosition() + this.transform.position;
        TemporaryConfettiHandler = Instantiate(GameObjectsConfetti[RandomPrefabIndex], ConfettiPosition, Quaternion.identity, this.transform);
        TemporaryConfettiHandler.transform.localScale = new Vector3(RandomScale, RandomScale, RandomScale);
        TemporaryConfettiHandler.transform.GetChild(0).GetComponent<MeshRenderer>().material = MaterialsConfetti[RandomMaterialIndex];
        TemporaryConfettiHandler.name = "Confetti";

        Rigidbody TemporaryRigidBody2D;
        TemporaryRigidBody2D = TemporaryConfettiHandler.transform.GetChild(0).GetComponent<Rigidbody>();
        TemporaryRigidBody2D.velocity = (ConfettiPosition - this.transform.position).normalized * RandomVelocity;

        Destroy(TemporaryConfettiHandler, RandomDuration);
    }
    private Vector3 RandomConfettiPosition()
    {
        Vector3 RandomPosition = DistanceFromEmitter * Random.onUnitSphere;
        if (RandomPosition.y < 0)
            RandomPosition.y *= -1;
        return RandomPosition;
    }
}
