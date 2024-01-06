using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class en : MonoBehaviour
{
    [Header("Игрок")]
    public Transform myPlayer;
    private NavMeshAgent myAgent;

    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        if (myAgent == null || myPlayer == null)
        {
            Debug.LogError("NavMeshAgent или myPlayer не назначены!");
            enabled = false;
            return;
        }

        // Проверка и перемещение начальной позиции игрока на NavMesh
        if (!NavMesh.SamplePosition(myPlayer.position, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
        {
            // Найти ближайшую точку на NavMesh к начальной позиции игрока
            NavMesh.FindClosestEdge(myPlayer.position, out hit, NavMesh.AllAreas);

            // Установить позицию игрока на найденную точку
            myPlayer.position = hit.position;
        }

        // Установка начальной целевой позиции для агента на позицию игрока
        myAgent.destination = myPlayer.position;
    }

    void Update()
    {
        // Устанавливаем целевую позицию агента на позицию игрока
        myAgent.destination = myPlayer.position;
    }
}
