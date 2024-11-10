using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UI_AgentManager : MonoBehaviour
{
    [SerializeField] Transform _objective;
    private NavMeshAgent _agent;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _agent.destination = _objective.position;
    }
}
