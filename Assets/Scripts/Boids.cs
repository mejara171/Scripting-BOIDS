using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += velocity * Time.deltaTime;
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }
}
