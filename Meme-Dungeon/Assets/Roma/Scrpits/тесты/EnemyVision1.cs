using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVision1 : MonoBehaviour
{
    public Transform player;
    public float visionDistance = 15f;
    public float viewAngle = 90f;
    public float patrolSpeed = 2f;
    public Vector3 lastKnownPlayerPosition;
    public float timeToRememberPlayer = 5f;
    public float rotationSpeed = 2f;

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
        // Проверка видимости игрока
        CheckPlayerVisibility();
    }

    void CheckPlayerVisibility()
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

                    // Отобразить поле зрения, если игрок виден
                    DebugDrawFieldOfView();
                }
                else
                {
                    isPlayerInSight = false;
                }
            }
        }

        // Отображение поля зрения во время игры
        if (Application.isPlaying && !isPlayerInSight)
        {
            DebugDrawFieldOfView();
        }
    }

    void DebugDrawFieldOfView()
    {
        Vector3 directionA = DirFromAngle(-viewAngle / 2);
        Vector3 directionB = DirFromAngle(viewAngle / 2);

        Debug.DrawRay(transform.position, directionA * visionDistance, Color.red);
        Debug.DrawRay(transform.position, directionB * visionDistance, Color.red);

        for (float angle = -viewAngle / 2; angle < viewAngle / 2; angle += 5f)
        {
            Vector3 viewDir = DirFromAngle(angle);
            Debug.DrawRay(transform.position, viewDir * visionDistance, Color.red);
        }
    }

    Vector3 DirFromAngle(float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(angleInRadians), 0, Mathf.Cos(angleInRadians));
    }
}

