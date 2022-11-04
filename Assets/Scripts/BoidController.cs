using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BoidController : MonoBehaviour
{
    //Metodos de acceso
    public int SwarmIndex { get; set; }
    public float NoClumpingRadius { get; set; }
    public float LocalAreaRadius { get; set; }
    public float Speed { get; set; }
    public float SteeringSpeed { get; set; }
    
    //Metodo para simular el movimiento
    public void SimulateMovement(List<BoidController> other, float time)
    {
        //Variables
        var steering = Vector3.zero;

        var SeparationDirection = Vector3.zero;
        var SeparationCount = 0;
        var AlignmentDirection = Vector3.zero;
        var AlignmentCount = 0;
        var CohesionDirection = Vector3.zero;
        var CohesionCount = 0;

        //Iterar cada boid
        foreach (BoidController boid in other)
        {
            if (boid == this) 
            {
                continue;
            }
             
            var distance = Vector3.Distance(boid.transform.position, this.transform.position);

            //Identificar vecino local
            if (distance < NoClumpingRadius)
            {
                SeparationDirection += boid.transform.position - transform.position;
                SeparationCount++;
            }

            //Identificar vecino local
            if (distance < LocalAreaRadius && boid.SwarmIndex == this.SwarmIndex)
            {
                AlignmentDirection += boid.transform.forward;
                AlignmentCount++;

                CohesionDirection += boid.transform.position - transform.position;
                CohesionCount++;
   
            }
        }


        if (SeparationCount > 0)
        {
            SeparationDirection /= SeparationCount;
        }
        //Voltearse
        SeparationDirection = -SeparationDirection;

        if (AlignmentCount > 0)
        {
            AlignmentDirection /= AlignmentCount;
        }

        if (CohesionCount > 0) 
        {
            CohesionDirection /= CohesionCount;
        }
        //Encontrar dirección al centro de masa
        CohesionDirection -= transform.position;


        steering += SeparationDirection.normalized;
        steering += AlignmentDirection.normalized;
        steering += CohesionDirection.normalized;

        //Aplicar dirección
        if (steering != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(steering), SteeringSpeed * time);
        }

        //Moverse
        transform.position += transform.TransformDirection(new Vector3(0, 0, Speed)) * time;
    }
}
