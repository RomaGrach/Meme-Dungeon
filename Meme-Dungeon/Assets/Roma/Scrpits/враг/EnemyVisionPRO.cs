using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVisionPRO : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float visionDistance = 15f; // Дистанция видимости врага
    public float visionDistance360 = 5f; // Дистанция видимости врага на 360
    public float viewAngle = 90f; // Угол обзора врага
    public float viewAngleBack = 360f; // Угол обзора врага
    public float patrolSpeed = 2f; // Скорость перемещения при патрулировании
    public Vector3 lastKnownPlayerPosition; // Последняя известная позиция игрока
    public float timeToRememberPlayer = 5f; // Время "памяти" врага после потери игрока
    public float rotationSpeed = 2f; // Скорость поворота врага при осмотре окружения
    public float DistanceToPoint = 5f;


    private NavMeshAgent enemyAgent;
    public bool isPlayerInSight;
    public bool isPatrolling;
    public float elapsedTimeSincePlayerLastSeen;
    public float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTimeSincePlayerLastSeen = timeToRememberPlayer;
        enemyAgent = GetComponent<NavMeshAgent>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        if (player == null)
        {
            Debug.LogError("Player reference is not set in EnemyVision script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;

        // Проверка видимости игрока в зоне на 360

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance360))
        {
            if (hit.collider.CompareTag("Player"))
            {
                isPatrolling = false;
                isPlayerInSight = true;
                //enemyAgent.destination = player.position;
                elapsedTimeSincePlayerLastSeen = 0f;
            }
            else
            {
                isPlayerInSight = false;
            }
        }
        // Проверка видимости игрока в зоне обзора
        else if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle * 0.5f)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isPatrolling = false;
                    isPlayerInSight = true;
                    //enemyAgent.destination = player.position;
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
                
                    Debug.Log("83");
                // Если прошло достаточно времени после потери игрока, начать патрулирование
                if (!isPatrolling)
                {
                    isPatrolling = true;
                    Debug.Log("88");
                    StartCoroutine(LookAround());
                }
            }
            else
            {
                // Пока враг помнит позицию игрока, двигаться к последней известной позиции
                //enemyAgent.destination  lastKnownPlayerPosition;
                // Пока враг помнит позицию игрока, двигаться к позиции игрока
                enemyAgent.destination = player.position;
            }
        }
        else
        {
            // Если игрок в зоне видимости, двигаться к его позиции
            enemyAgent.destination = player.position;
        }
    }
    IEnumerator Patrol()
    {
        Debug.Log("106");
        isPatrolling = true;
        Vector3 randomPoint = Random.insideUnitSphere * visionDistance;
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position + randomPoint, out navHit, visionDistance, NavMesh.AllAreas);

        enemyAgent.speed = patrolSpeed;

        // Перемещение к случайной точке в пределах дистанции видимости
        enemyAgent.destination = navHit.position;

        while (Vector3.Distance(transform.position, navHit.position) > DistanceToPoint)
        {
            Debug.Log("117");
            Debug.Log(Vector3.Distance(transform.position, navHit.position));
            yield return null;
            Debug.Log("122");
        }
        StartCoroutine(Patrol());

        // После достижения точки, осмотр окружения


        //enemyAgent.speed = 0f; // Остановка врага на месте
    }

    IEnumerator LookAround()
    {
        angle = 0f;
        while (angle < 360f)
        {
            Debug.Log("125");
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            angle += rotationSpeed * Time.deltaTime;
            yield return null;
        }

        StartCoroutine(Patrol());
    }

}
