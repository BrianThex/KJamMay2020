using System.Collections.Generic;
using ToLC.Player;
using UnityEngine;
using UnityEngine.AI;

namespace ToLC.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AI : MonoBehaviour
    {
        public HealthManager healthManager = null;

        private NavMeshAgent agent = null;

        private Collider[] rangeColliders;

        private Transform aggroTarget = null;

        private bool hasAggro = false;

        public float range = 20;

        public float damage = 5;

        public float attackCooldown = 0;

        public float baseCooldown = 1f;

        private float distance;

        public float aggroRange, attackRange, moveSpeed;

        private void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            attackCooldown = baseCooldown;
        }

        private void Update()
        {
            attackCooldown -= Time.deltaTime;

            if (!hasAggro)
            {
                CheckForEnenyTargets();
            }
            else
            {
                if (aggroTarget == null)
                {
                    hasAggro = false;
                    return;
                }

                transform.LookAt(aggroTarget);
                MoveToAggroTarget();

                if (distance <= attackRange)
                {
                    if (attackCooldown <= 0)
                    {
                        AttackPlayer();
                    }
                }
            }
        }

        private void AttackPlayer()
        {
            List<PlayerInput> enemies = new List<PlayerInput>();

            Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
            Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

            RaycastHit hit;
            Quaternion angle = transform.rotation * startingAngle;
            Vector3 direction = angle * Vector3.forward;
            var pos = new Vector3(transform.position.x, .5f, transform.position.z);
            for (var i = 0; i < 24; i++)
            {
                //Debug.DrawRay(pos, direction * range, Color.red, 100);

                if (Physics.Raycast(pos, direction, out hit, range))
                {
                    var enemy = hit.collider.GetComponent<PlayerInput>();
                    if (enemy)
                    {
                        //Adding enemy to list
                        if (!enemies.Contains(enemy))
                        {
                            enemies.Add(enemy);
                            Debug.Log(enemy.transform.gameObject.name);
                        }
                    }
                }
                direction = stepAngle * direction;
            }
            // enemy takes damage
            for (int e = 0; e < enemies.Count; e++)
            {
                enemies[e].healthManager.TakeDamage(damage);
            }
            attackCooldown = baseCooldown;
        }

        private void Die()
        {
            Destroy(gameObject);
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

