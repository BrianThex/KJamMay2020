using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace ToLC.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AI : MonoBehaviour
    {
        private NavMeshAgent agent = null;

        private Collider[] rangeColliders;

        private Transform aggroTarget = null;

        private bool hasAggro = false;

        private float distance;

        public float aggroRange, attackRange, moveSpeed;

        private void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
        }

        private void Update()
        {
            if (!hasAggro)
            {
                CheckForEnenyTargets();
            }
            else
            {
                MoveToAggroTarget();
            }
        }

        private void CheckForEnenyTargets()
        {
            rangeColliders = Physics.OverlapSphere(transform.position, aggroRange);

            for (int i =0; i < rangeColliders.Length; i++)
            {
                if (rangeColliders[i].gameObject.tag == "Player")
                {
                    aggroTarget = rangeColliders[i].gameObject.transform;
                    hasAggro = true;
                    break;
                }
            }
        }

        private void MoveToAggroTarget()
        {
            distance = Vector3.Distance(aggroTarget.position, transform.position);

            agent.stoppingDistance = (attackRange);

            if (distance <= aggroRange)
            {
                agent.SetDestination(aggroTarget.position);
            }
        }
    }
}

