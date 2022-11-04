using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boid : MonoBehaviour
{
    float speed;
    Vector2 direction;
    public List<Boid> BoidsInScene;
    [SerializeField] private float MoveToCenterStrength;
    float LocalBoidsDistance;
    [SerializeField] private float AvoidOtherStrength;
    float CollisionAvoidCheckDistance;
    [SerializeField] private float AlignWithOtherStrength;
    float AlignmentCheckDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AlignWithOthers();
        MoveToCenter();
        AvoidOtherBoids();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void MoveToCenter()
    {
        Vector2 PositionSum = transform.position;
        int count = 0;

        foreach (Boid boid in BoidsInScene)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= LocalBoidsDistance)
            {
                PositionSum += (Vector2)boid.transform.position;
                count++;
            }

        }
        
        if (count == 0)
        {
            return;
        }

        Vector2 PositionAverage = PositionSum / count;
        PositionAverage = PositionAverage.normalized;
        Vector2 FaceDirection = (PositionAverage - (Vector2)transform.position).normalized;


        float DeltaTimeStrength = MoveToCenterStrength * Time.deltaTime;
        direction = direction + DeltaTimeStrength * FaceDirection/(DeltaTimeStrength + 1);
        direction = direction.normalized;
    }

    void AvoidOtherBoids()
    {
        Vector2 FaceAwayDirection = Vector2.zero;

        foreach (Boid boid in BoidsInScene)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);

            if (distance <= CollisionAvoidCheckDistance)
            {
                FaceAwayDirection = FaceAwayDirection + (Vector2)(transform.position - boid.transform.position);
            }
        }

        FaceAwayDirection = FaceAwayDirection.normalized;

        direction = direction + AvoidOtherStrength * FaceAwayDirection / (AvoidOtherStrength + 1);
        direction = direction.normalized;
    }

    void AlignWithOthers()
    {
        Vector2 DirectionSum = Vector3.zero;
        int count = 0;

        foreach (var boid in BoidsInScene)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= AlignmentCheckDistance)
            {
                DirectionSum += boid.direction;
                count++;
            }
        }

        Vector2 DirectionAverage = DirectionSum / count;
        DirectionAverage = DirectionAverage.normalized;

        float DeltaTimeStrength = AlignWithOtherStrength * Time.deltaTime;
        direction = direction + DeltaTimeStrength * DirectionAverage / (DeltaTimeStrength + 1);
        direction = direction.normalized;

    }
}
