using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfettiEmitter : MonoBehaviour {

    private GameObject[] GameObjectsConfetti;
    private Material[] MaterialsConfetti;
    private int Direction;
    private int EmitCount = 7;
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
            EmitAllConfetti();
        }
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
        float RandomVelocity = Random.Range(5f, 15f);
        int RandomPrefabIndex = Random.Range(0, GameObjectsConfetti.Length);
        int RandomMaterialIndex = Random.Range(0, MaterialsConfetti.Length);
        float RandomDuration = Random.Range(1f, 2f);

        Vector2 PositionVector = GetVectorFromAngle(Angle);
        GameObject TemporaryConfettiHandler;
        Vector3 ConfettiPosition = GetPositionFromVector(PositionVector);
        TemporaryConfettiHandler = Instantiate(GameObjectsConfetti[RandomPrefabIndex], ConfettiPosition, Quaternion.identity, this.transform);
        TemporaryConfettiHandler.transform.GetChild(0).GetComponent<MeshRenderer>().material = MaterialsConfetti[RandomMaterialIndex];

        Rigidbody TemporaryRigidBody2D;
        TemporaryRigidBody2D = TemporaryConfettiHandler.transform.GetChild(0).GetComponent<Rigidbody>();
        TemporaryRigidBody2D.velocity = PositionVector.normalized * RandomVelocity;
        Debug.Log(TemporaryRigidBody2D.velocity);

        Destroy(TemporaryConfettiHandler, RandomDuration);
    }
    private Vector2 GetVectorFromAngle(float AngleInDegrees)
    {
        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;
        int Direction = (this.transform.rotation.z > 0) ? -1 : 1;
        float Adjacent = Direction * Mathf.Cos(AngleInRadians);
        float Opposite = Direction * Mathf.Sin(AngleInRadians);
        return new Vector2(Opposite, Adjacent);
    }
    private Vector3 GetPositionFromVector(Vector2 PositionVector)
    {
        Vector3 Position = new Vector3();
        PositionVector = PositionVector * 1.5f;
        Position.x = this.transform.position.x + PositionVector.x;
        Position.y = this.transform.position.y + PositionVector.y;
        Position.z = this.transform.position.z;
        return Position;
    }
}
