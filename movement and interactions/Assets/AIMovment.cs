using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Delore.AI
{
    public class AIMovment : MonoBehaviour
    {
        NavMeshAgent agent;
        GameObject player;
        AIDetection detection;
        Vector3 startingPos;

        float timer;
        bool isChasing = false;
        bool waiting = false;

        public EnemyType enemyType;


        [SerializeField]
        float timeOfChasing = 3f, patrolTime = 8f;

        

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player"); 
            detection = GetComponent<AIDetection>();
            startingPos = transform.position;
        }

        void Update()
        {
            if (detection.playerInRange || isChasing)
                Mover();
            if (!isChasing && !waiting)
                StartCoroutine(Patrol());
        }

        private void Mover()
        {
            switch (enemyType)
            {
                case EnemyType.Human:
                    if (detection.FieldOfView() || detection.HeardPlayer())
                    {
                        agent.isStopped = false;
                        timer = timeOfChasing;
                        agent.destination = player.transform.position;
                        isChasing = true;
                    }
                    else if (timer > 0)
                    {
                        agent.destination = player.transform.position;
                    }
                    else
                    {
                        if (isChasing)
                            agent.isStopped = true;
                        isChasing = false;
                    }
                    timer -= Time.deltaTime;
                    break;
                case EnemyType.AgressiveAnimal:
                    break;
                case EnemyType.PassiveAnimal:
                    break;
            }

        }

        private IEnumerator Patrol()
        {
            waiting = true;
            if (agent.isStopped)
                agent.isStopped = false;
            float pos_z = startingPos.z - Random.value * detection.radius;
            float pos_x = startingPos.x - Random.value * detection.radius;
            agent.destination = new Vector3(pos_x, 0, pos_z);
            yield return new WaitForSeconds(Random.Range(patrolTime,patrolTime+3));
            waiting = false;

        }
 
    }

    public enum EnemyType
    {
        Human,
        AgressiveAnimal,
        PassiveAnimal
    }

}
