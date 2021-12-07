using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Delore.AI {
    public class AIDetection : MonoBehaviour
    {
        private GameObject player;
        private ThirdPersonCharacter character;
        private LayerMask playerMask;
        private LayerMask obstructionMask;

        internal bool playerInRange = false;

        


        [Range(0, 50)]
        public float radius = 10;

        [Range(0, 360)]
        public float angle = 120;

        [Range(0, 100)]
        public float hearingDistance = 10;

        [SerializeField]
        bool tracking = false;



        void Start()
        {
            player = GameObject.FindWithTag("Player");
            character = player.GetComponent<ThirdPersonCharacter>();
            playerMask = LayerMask.GetMask("Player");
            obstructionMask = LayerMask.GetMask("Default");

            SphereCollider playerInDistance = gameObject.AddComponent<SphereCollider>();
            playerInDistance.radius = Mathf.Max(radius, hearingDistance);
            playerInDistance.isTrigger = true;

        }

        
        public bool HeardPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) <= hearingDistance * character.SoundLevel / 10;
        }

        #region FieldOfView
        public bool FieldOfView()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(target.position, transform.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                        return true;

                    return false;
                }
                return false;
            }
            return false;
        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (tracking && !GameObject.Find("SmellObject(Clone)"))
            {
                other.GetComponent<ThirdPersonCharacter>().LeftSmell();
            }
            if (other.tag == "Player")
                playerInRange = true;
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
                playerInRange = false;
        }


        #region DrawHearingDistance
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, hearingDistance);
        }
        #endregion

    }
}
