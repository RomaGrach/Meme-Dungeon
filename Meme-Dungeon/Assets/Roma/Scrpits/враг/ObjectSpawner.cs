using UnityEngine;
using UnityEngine.AI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectToSpawn; // Префаб объекта, который вы хотите создать
    public float spawnRadius = 10f; // Радиус вокруг центра для создания объекта
    public LayerMask groundLayer; // Слой объектов, на которых может находиться NavMesh
    public int CountOfSpawn = 1;

    void SpawnObjectOnNavMesh()
    {
        // Генерация случайной точки в радиусе спавна
        Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;
        randomPoint += transform.position;

        // Найти ближайшую точку на NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, groundLayer))
        {
            // Если найдена точка на NavMesh, создать объект в этой позиции
            Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], hit.position, Quaternion.identity);
        }
    }

    private void Start()
    {
        for( float i = 0; i < CountOfSpawn; i++)
        {
            SpawnObjectOnNavMesh();
        }
    }
    void Update()
    {
        // Вызвать функцию создания объекта по нажатию клавиши (можно изменить на свое усмотрение)
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SpawnObjectOnNavMesh();
        //}
    }
}
