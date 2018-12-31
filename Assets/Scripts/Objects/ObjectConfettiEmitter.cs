using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConfettiEmitter : MonoBehaviour {

    private GameObject[] GameObjectsConfetti;
    private Material[] MaterialsConfetti;
    private int EmitCount = 1;
    private float DistanceFromEmitter = 3f;
    private float TimeGap = 1f;
    public Vector2 RandomAngleRange;

	// Use this for initialization
	void Start ()
    {
        Random.InitState(0);
        string PathToPrefabs = "Prefabs/Confetti/";
        string PathToMaterials = "Materials/Confetti/";
        GameObjectsConfetti = Resources.LoadAll<GameObject>(PathToPrefabs);
        MaterialsConfetti = Resources.LoadAll<Material>(PathToMaterials);
        float LowerBoundAngle = this.transform.localEulerAngles.y - 135;
        float UpperBoundAngle = this.transform.localEulerAngles.y - 45;
        /*
        if (LowerBoundAngle < 0)
            LowerBoundAngle = 360 + LowerBoundAngle;
        if (UpperBoundAngle < 0)
            UpperBoundAngle = 360 + UpperBoundAngle;
        */    
        RandomAngleRange = new Vector2(LowerBoundAngle, UpperBoundAngle);
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
        float RandomVelocity = Random.Range(10f, 25f);
        int RandomPrefabIndex = Random.Range(0, GameObjectsConfetti.Length);
        int RandomMaterialIndex = Random.Range(0, MaterialsConfetti.Length);
        float RandomDuration = Random.Range(1f, 2f);

        Vector2 PositionVector = GetVectorFromAngle(Angle);
        GameObject TemporaryConfettiHandler;
        Vector3 ConfettiPosition = GetPositionFromVector(PositionVector);
        TemporaryConfettiHandler = Instantiate(GameObjectsConfetti[RandomPrefabIndex], ConfettiPosition, Quaternion.identity, this.transform);
        TemporaryConfettiHandler.transform.GetChild(0).GetComponent<MeshRenderer>().material = MaterialsConfetti[RandomMaterialIndex];
        TemporaryConfettiHandler.name = "Confetti";
        //Debug.Log(Angle + ", " + PositionVector + ", " + GetPositionFromVector(PositionVector));
        RandomConfettiPosition();

        Rigidbody TemporaryRigidBody2D;
        TemporaryRigidBody2D = TemporaryConfettiHandler.transform.GetChild(0).GetComponent<Rigidbody>();
        TemporaryRigidBody2D.velocity = (ConfettiPosition-this.transform.position).normalized * RandomVelocity;

        Destroy(TemporaryConfettiHandler, RandomDuration);
    }
    private void RandomConfettiPosition()
    {
        Random.InitState(22);
        float RandomLat = Random.Range(0f, 90f);
        float RandomLng = Random.Range(RandomAngleRange.x, RandomAngleRange.y);
        Vector2 LatLng = new Vector2(RandomLat, RandomLng);
        float LngAdjacent = Mathf.Cos(RandomLng);
        float LngOpposite = Mathf.Sin(RandomLng);
        Vector2 AdjOpp = new Vector2(LngAdjacent, LngOpposite);
        Debug.Log(this.name + "\n>LatLng" + LatLng + "\n>AdjOpp" + AdjOpp);
    }
    private Vector2 GetVectorFromAngle(float AngleInDegrees)
    {
        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;
        float Adjacent = -Mathf.Cos(AngleInRadians);
        float Opposite = -Mathf.Sin(AngleInRadians);
        return new Vector2(Opposite, Adjacent);
    }
    private Vector3 GetPositionFromVector(Vector2 PositionVector)
    {
        Vector3 Position = new Vector3();
        PositionVector = PositionVector * DistanceFromEmitter;
        Position.z = this.transform.position.z + PositionVector.x;
        Position.y = this.transform.position.y + PositionVector.y;
        Position.x = this.transform.position.x;
        return Position;
    }
}
