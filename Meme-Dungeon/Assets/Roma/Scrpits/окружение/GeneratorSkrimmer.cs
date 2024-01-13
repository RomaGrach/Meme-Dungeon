using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class GeneratorSkrimmer : MonoBehaviour
{
    public GameObject[] objectToSpawn; // Префаб объекта, который вы хотите создать
    public float spawnRadius = 10f; // Радиус вокруг центра для создания объекта
    public LayerMask groundLayer; // Слой объектов, на которых может находиться NavMesh
    public int countOfSpawn = 1;
    public float minDistanceBetweenObjects = 2f; // Минимальное расстояние между объектами



    void SpawnObjectOnNavMesh()
    {
        // Генерация случайной точки внутри круга с равномерным распределением
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float radius = Mathf.Sqrt(Random.Range(0f, 1f)) * spawnRadius;

        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);

        Vector3 randomPoint = new Vector3(x, 0f, z) + transform.position;

        // Проверка минимального расстояния до других объектов
        if (!IsCloseToOtherObjects(randomPoint, minDistanceBetweenObjects))
        {
            // Найти ближайшую точку на NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, groundLayer))
            {
                // Если найдена точка на NavMesh, создать объект в этой позиции
                Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], hit.position, Quaternion.identity);
            }
        }
    }

    bool IsCloseToOtherObjects(Vector3 position, float minDistance)
    {
        Collider[] colliders = Physics.OverlapSphere(position, minDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("SpawnedObject"))
            {
                // Обнаружен объект с тегом "SpawnedObject" на минимальном расстоянии
                return true;
            }
        }

        // Нет объектов на минимальном расстоянии
        return false;
    }

    private void Start()
    {
        StartCoroutine(DelayedFunction());
    }
    IEnumerator DelayedFunction()
    {
        yield return new WaitForSeconds(0.1f);
        YourFunctionToExecute();
    }
    void YourFunctionToExecute()
    {
        for (int i = 0; i < countOfSpawn; i++)
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
