using System.Collections;
using UnityEngine;
using UnityEngine.AI;


/*
public class EnemyVision : MonoBehaviour
{
    public Transform player;
    public float visionDistance = 15f;
    public float viewAngle = 90f;
    public float patrolSpeed = 2f;
    public Vector3 lastKnownPlayerPosition;
    public float timeToRememberPlayer = 5f;
    public float rotationSpeed = 5f; // Увеличенная скорость поворота врага

    private NavMeshAgent enemyAgent;
    private bool isPlayerInSight;
    private bool isPatrolling;
    private float elapsedTimeSincePlayerLastSeen;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player reference is not set in EnemyVision script!");
        }

        // Начать патрулирование при старте
        StartCoroutine(Patrol());
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;

        // Проверка видимости игрока в зоне обзора
        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle * 0.5f)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isPlayerInSight = true;
                    lastKnownPlayerPosition = player.position;
                    elapsedTimeSincePlayerLastSeen = 0f;
                }
                else
                {
                    isPlayerInSight = false;
                }
            }
        }

        // Потеря игрока из виду
        if (!isPlayerInSight)
        {
            elapsedTimeSincePlayerLastSeen += Time.deltaTime;

            if (elapsedTimeSincePlayerLastSeen >= timeToRememberPlayer && !isPatrolling)
            {
                // Если прошло достаточно времени после потери игрока, начать патрулирование
                StartCoroutine(Patrol());
            }
        }
        else
        {
            StopCoroutine(Patrol()); // Остановить патрулирование при обнаружении игрока
        }
    }

    // Корутина для патрулирования
    IEnumerator Patrol()
    {
        isPatrolling = true;

        while (true)
        {
            Vector3 randomPoint = Random.insideUnitSphere * visionDistance;
            NavMeshHit navHit;
            NavMesh.SamplePosition(transform.position + randomPoint, out navHit, visionDistance, NavMesh.AllAreas);

            enemyAgent.speed = patrolSpeed;

            // Перемещение к случайной точке в пределах дистанции видимости
            enemyAgent.destination = navHit.position;

            while (Vector3.Distance(transform.position, navHit.position) > 1.5f)
            {
                yield return null;
            }

            // После достижения точки, осмотр окружения
            float angle = 0f;
            while (angle < 360f)
            {
                transform.Rotate(Vector3.up * rotationSpeed);
                angle += rotationSpeed;
                yield return null;
            }
        }
    }
}
*/


public class EnemyVision : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float visionDistance = 15f; // Дистанция видимости врага
    public float viewAngle = 90f; // Угол обзора врага
    public float patrolSpeed = 2f; // Скорость перемещения при патрулировании
    public Vector3 lastKnownPlayerPosition; // Последняя известная позиция игрока
    public float timeToRememberPlayer = 5f; // Время "памяти" врага после потери игрока
    public float rotationSpeed = 2f; // Скорость поворота врага при осмотре окружения

    private NavMeshAgent enemyAgent;
    private bool isPlayerInSight;
    private bool isPatrolling;
    private float elapsedTimeSincePlayerLastSeen;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player reference is not set in EnemyVision script!");
        }
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;

        // Проверка видимости игрока в зоне обзора
        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle * 0.5f)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isPlayerInSight = true;
                    lastKnownPlayerPosition = player.position;
                    elapsedTimeSincePlayerLastSeen = 0f;
                }
                else
                {
                    isPlayerInSight = false;
                }
            }
        }

        // Потеря игрока из виду
        if (!isPlayerInSight)
        {
            elapsedTimeSincePlayerLastSeen += Time.deltaTime;

            if (elapsedTimeSincePlayerLastSeen >= timeToRememberPlayer)
            {
                // Если прошло достаточно времени после потери игрока, начать патрулирование
                if (!isPatrolling)
                {
                    StartCoroutine(Patrol());
                }
            }
            else
            {
                // Пока враг помнит позицию игрока, двигаться к последней известной позиции
                enemyAgent.destination = lastKnownPlayerPosition;
            }
        }
        else
        {
            // Если игрок в зоне видимости, двигаться к его позиции
            enemyAgent.destination = player.position;
        }
    }

    // Корутина для патрулирования
    IEnumerator Patrol()
    {
        isPatrolling = true;
        Vector3 randomPoint = Random.insideUnitSphere * visionDistance;
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position + randomPoint, out navHit, visionDistance, NavMesh.AllAreas);

        enemyAgent.speed = patrolSpeed;

        // Перемещение к случайной точке в пределах дистанции видимости
        enemyAgent.destination = navHit.position;

        while (Vector3.Distance(transform.position, navHit.position) > 1.5f)
        {
            yield return null;
        }

        // После достижения точки, осмотр окружения
        float angle = 0f;
        while (angle < 360f)
        {
            transform.Rotate(Vector3.up * rotationSpeed);
            angle += rotationSpeed;
            yield return null;
        }

        isPatrolling = false;
        enemyAgent.speed = 0f; // Остановка врага на месте
    }
}
