using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PathBall : MonoBehaviour
{
    public float TravelTime = 60f;
    private float remainingTime;
    public float TrailTime = 1000f;
    public float startWidth = 0.1f;
    public float endWidth = 0.1f;
    private Transform tr;
    private Vector3 pos;
    private Vector3 End;
    private NavMeshAgent agent;
    private NavMeshAgent agentEnd;
    private float startTime = 0f;
    private bool EndFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        FinishGate EndPoint = FindFirstObjectByType<FinishGate>();
        startTime = Time.time;
        agent = GetComponent<NavMeshAgent>();
        tr = gameObject.transform;
        pos = gameObject.transform.position;
        End = EndPoint.transform.position;
        agent.destination = End;
        Debug.Log($"Start Position {pos}");
        Debug.Log($"End Destination {End}");
        if (agent.destination != End)
        {
            float diff = Mathf.Abs(End.x - pos.x) + Mathf.Abs(End.z - pos.z);
            NavMeshHit hit;
            NavMesh.SamplePosition(End, out hit, diff, NavMesh.AllAreas);
            End = hit.position;
            Debug.Log($"Difference {diff}");
            Debug.Log($"Calculated pos {End}");
            agent.destination = End;
        }
        if (agent.isOnNavMesh)
        {
            TrailRenderer trail = GetComponent<TrailRenderer>();
            trail.time = TrailTime;
            trail.startWidth = startWidth;
            trail.endWidth = endWidth;
            trail.enabled = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if ((Mathf.Abs(tr.position.x - End.x) < 0.1f && Mathf.Abs(tr.position.z - End.z) < 0.1f && !EndFlag) || remainingTime >= TravelTime) _Disable();
        if (EndFlag && remainingTime >= TrailTime) Destroy(gameObject);
        remainingTime = Time.time - startTime;
    }
    private void _Disable()
    {
        EndFlag = true;
        startTime = Time.time;
        remainingTime = 0;
        //EndPoint.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }
}
