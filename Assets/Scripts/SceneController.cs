using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    //Prefabs
    public BoidController BoidPrefab;

    //Lista para guardar los boids
    private List<BoidController> _Boids;

    //Variables de uso 
    public int SpawnBoids = 100;
    public float boidSpeed = 10f;
    public float boidSteeringSpeed = 100f;
    public float boidNoClumpingArea = 10f;
    public float boidLocalArea = 10f;
    public float boidSimulationArea = 50f;


    private void Start()
    {
        //Agregar boids a la lista y spawnearlos
        //Flyweight
        _Boids = new List<BoidController>();

        for (int i = 0; i < SpawnBoids; i++)
        {
            SpawnBoid(BoidPrefab.gameObject, 0);
        }
    }


    private void Update()
    {
        //Simular movimiento de los boids
        foreach (BoidController boid in _Boids)
        {
            boid.SimulateMovement(_Boids, Time.deltaTime);
        }
    }

    //Metodo para spawnear boids
    private void SpawnBoid(GameObject prefab, int swarmIndex)
    {
        //Instanciar los boids
        var boidInstance = Instantiate(prefab);

        //Posicionar los boids
        boidInstance.transform.localPosition += new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)); 
        boidInstance.transform.localRotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        //Obtener el componente BoidController
        var BoidController = boidInstance.GetComponent<BoidController>();

        //Definir valores de los boids
        BoidController.SwarmIndex = swarmIndex;
        BoidController.Speed = boidSpeed;
        BoidController.SteeringSpeed = boidSteeringSpeed;
        BoidController.LocalAreaRadius = boidLocalArea;
        BoidController.NoClumpingRadius = boidNoClumpingArea;

        _Boids.Add(BoidController);
    }
}
