using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Delore.AI
{
    public class AIMovment : MonoBehaviour
    {
        private NavMeshAgent agent;
        private GameObject player;
        private ThirdPersonCharacter character;

        private float timer;
        private bool canSeePlayer = false;

        [Range(0, 50)]
        public float radius = 10;

        [Range(0,360)]
        public float angle = 120;

        [Range(0, 100)]
        public float hearingDistance = 10;

        [SerializeField] float LengthOfChasing;
        
        
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
            character = player.GetComponent<ThirdPersonCharacter>();
            StartCoroutine(FOVRoutine());
        }

        // Update is called once per frame
        void Update()
        {
            Mover();
        }

        private void Mover()
        {

            if (canSeePlayer || HeardPlayer())
            {
                timer = LengthOfChasing;
                agent.destination = player.transform.position;
            }
            else if (timer > 0)
                agent.destination = player.transform.position;
            else
            {
                Patrol();
            }
            timer -= Time.deltaTime;

        }

        private void Patrol()
        {
            return;
        }
        private bool HeardPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= hearingDistance * character.SoundLevel / 10;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hearingDistance);
        }

        private IEnumerator FOVRoutine()
        {
            LayerMask playerMask = LayerMask.GetMask("Player"); ;
            LayerMask obstructionMask = LayerMask.GetMask("Default");

            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                FieldOfView(playerMask,obstructionMask);
            }
        }

        private void FieldOfView(LayerMask playerMask,LayerMask obstructionMask)
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius,playerMask);
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(target.position, transform.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        canSeePlayer = true;
                    else
                        canSeePlayer = false;
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
                canSeePlayer = false;
        }
    }
}
