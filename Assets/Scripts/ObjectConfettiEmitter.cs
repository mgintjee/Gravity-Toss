using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfettiEmitter : MonoBehaviour {

    private GameObject[] GameObjectsConfetti;
    private Material[] MaterialsConfetti;
    private int Direction;
	// Use this for initialization
	void Start ()
    {
        Random.InitState(0);
        string PathToPrefabs = "Prefabs/Confetti/";
        string PathToMaterials = "Materials/Confetti/";
        GameObjectsConfetti = Resources.LoadAll<GameObject>(PathToPrefabs);
        MaterialsConfetti = Resources.LoadAll<Material>(PathToMaterials);
        Debug.Log(transform.rotation.eulerAngles);
        Direction = (this.transform.rotation.eulerAngles.z == 90) ? -1 : 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 3; ++i )
                EmitConfetti();
        }
	}

    public void EmitConfetti()
    {
        float RandomVelocity = Random.Range(2.5f, 5f);
        int RandomPrefabIndex = Random.Range(0, GameObjectsConfetti.Length);
        int RandomMaterialIndex = Random.Range(0, MaterialsConfetti.Length);
        float RandomDuration = Random.Range(1f, 3f);

        GameObject TemporaryConfettiHandler;
        Transform EmitterTarget = this.transform.Find("EmitterTarget");
        TemporaryConfettiHandler = Instantiate(GameObjectsConfetti[RandomPrefabIndex], EmitterTarget.position, Quaternion.identity);
        TemporaryConfettiHandler.transform.Find("Confetti_1.model").GetComponent<MeshRenderer>().material = MaterialsConfetti[RandomMaterialIndex];

        Rigidbody TemporaryRigidBody2D;
        TemporaryRigidBody2D = TemporaryConfettiHandler.transform.Find("Confetti_1.model").GetComponent<Rigidbody>();
        TemporaryRigidBody2D.velocity = new Vector3(Direction, 5f, -.25f).normalized * RandomVelocity;
        Destroy(TemporaryConfettiHandler, RandomDuration);
    }
}
