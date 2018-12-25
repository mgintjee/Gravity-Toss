using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfettiEmitter : MonoBehaviour {

    private GameObject[] GameObjectsConfetti;
    private Material[] MaterialsConfetti;
    private int Direction;
    private int EmitCount = 3;
    private float TimeGap = 1f;

	// Use this for initialization
	void Start ()
    {
        Random.InitState(0);
        string PathToPrefabs = "Prefabs/Confetti/";
        string PathToMaterials = "Materials/Confetti/";
        GameObjectsConfetti = Resources.LoadAll<GameObject>(PathToPrefabs);
        MaterialsConfetti = Resources.LoadAll<Material>(PathToMaterials);
        Direction = (this.transform.rotation.eulerAngles.z == 90) ? -1 : 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            float CurrentTime = Time.time;
            for(int i = 0; i < EmitCount; ++i)
            {
                EmitConfetti();
            }
        }
	}

    public void EmitConfetti()
    {
        float RandomVelocity = Random.Range(5f, 15f);
        int RandomPrefabIndex = Random.Range(0, GameObjectsConfetti.Length);
        int RandomMaterialIndex = Random.Range(0, MaterialsConfetti.Length);
        float RandomDuration = Random.Range(1f, 2f);

        GameObject TemporaryConfettiHandler;
        Transform EmitterTarget = this.transform.Find("EmitterTarget");
        TemporaryConfettiHandler = Instantiate(GameObjectsConfetti[RandomPrefabIndex], EmitterTarget.position, Quaternion.identity);
        TemporaryConfettiHandler.transform.Find("Confetti_1.model").GetComponent<MeshRenderer>().material = MaterialsConfetti[RandomMaterialIndex];
        Rigidbody TemporaryRigidBody2D;
        TemporaryRigidBody2D = TemporaryConfettiHandler.transform.Find("Confetti_1.model").GetComponent<Rigidbody>();
        TemporaryRigidBody2D.velocity = new Vector3(Direction, 5f, -.25f).normalized * RandomVelocity;
        Debug.Log(">" + this.transform.name + ":\n> Target:" + EmitterTarget.position + "\n > PushTowards:" + new Vector3(Direction, 5f, -.25f).normalized);
        Destroy(TemporaryConfettiHandler, RandomDuration);
    }
}
