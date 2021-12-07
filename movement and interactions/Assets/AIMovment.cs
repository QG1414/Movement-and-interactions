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
        SmellObject smellObject;
        AIDetection detection;
        Vector3 startingPos;
        float speed;

        float timer;
        bool isChasing = false;
        bool waiting = false;
        bool smelled = false;

        public AIType aiType;


        [SerializeField]
        float timeOfChasing = 3f, patrolTime = 8f;




        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
            detection = GetComponent<AIDetection>();
            startingPos = transform.position;
            speed = agent.speed;
        }

        void Update()
        {
            if (detection.playerInRange || isChasing)
                Mover();
            if (!isChasing && !waiting)
                StartCoroutine(Patrol());
            if (smelled)
                smelled = SmellIsActive();

        }

        private bool SmellIsActive()
        {
            if(GameObject.Find("SmellObject(Clone)") != null)
                return true;
            return false;
        }

        private void Mover()
        {
            if (detection.FieldOfView() || detection.HeardPlayer() || smelled)
            {
                if (speed != agent.speed)
                    agent.speed += 1f;
                agent.isStopped = false;
                timer = timeOfChasing;
                agent.destination = AIType.Aggressive == aiType ? player.transform.position : RunningDirection();
                isChasing = true;
            }
            else if (timer > 0)
            {
                agent.destination = AIType.Aggressive == aiType ? player.transform.position : RunningDirection();
            }
            else
            {
                if (isChasing)
                    agent.isStopped = true;
                if (AIType.Passive == aiType)
                    startingPos = transform.position;
                isChasing = false;
            }
            timer -= Time.deltaTime;
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
            if (aiType == AIType.Aggressive && !isChasing)
            {
                GameObject smell = GameObject.Find("SmellObject(Clone)");
                if (smell != null && SmellPlayer(smell.transform.position))
                {
                    smellObject = GameObject.Find("SmellObject(Clone)").GetComponent<SmellObject>();
                    smelled = true;
                    agent.speed -= 1f;
                }
             }
        }

        private Vector3 RunningDirection()
        {
            float pos_x = player.transform.position.x;
            float pos_y = player.transform.position.y;
            float pos_z = player.transform.position.z;
            float runningDistance = Mathf.Max(detection.radius, detection.hearingDistance) * 2f;
            if (transform.position.x > pos_x)
                return pos_z > transform.position.z ? new Vector3(pos_x + runningDistance, pos_y, pos_z - runningDistance) : new Vector3(pos_x + runningDistance, pos_y, pos_z+ runningDistance);
            else
                return pos_z > transform.position.z ? new Vector3(pos_x - runningDistance, pos_y, pos_z - runningDistance) : new Vector3(pos_x - runningDistance, pos_y, pos_z + runningDistance);
        }

        private bool SmellPlayer(Vector3 pos)
        {
            if (Vector3.Distance(transform.position, pos) <= Mathf.Min(detection.radius, detection.hearingDistance) - 10f)
                return true;
            return false;
        }

    }

    public enum AIType
    {
        Aggressive,
        Passive
    }

}
