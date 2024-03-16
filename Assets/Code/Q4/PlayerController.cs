using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

namespace FPS{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        public Transform povOrigin;
        public Transform projectileOrigin;
        public GameObject projectilePrefab;

        public float attackRange;

        public List<int> keyIdsObtained;
        private void Awake()
        {
            instance = this;
            keyIdsObtained = new List<int>();
        }

        void OnPrimaryAttack()
        {
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, attackRange);
            if (hitSomething)
            {
                Rigidbody targetRigidbody = hit.transform.GetComponent<Rigidbody>();
                if (targetRigidbody)
                {
                    targetRigidbody.AddForce(povOrigin.forward * 100f, ForceMode.Impulse);
                }
            }
        }

        void OnSecondaryAttack()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, Quaternion.LookRotation(povOrigin.forward));
            projectile.transform.localScale = Vector3.one * 5f;
            projectile.GetComponent<Rigidbody>().AddForce(povOrigin.forward * 25f, ForceMode.Impulse);
        }

        void OnInteract()
        {
            RaycastHit hit;
            if (Physics.Raycast(povOrigin.position, povOrigin.forward, out hit, 2f))
            {
                //print("Interacted with " + hit.transform.name + " from " + hit.distance + "m.");

                Door targetDoor = hit.transform.GetComponent<Door>();
                if (targetDoor)
                {
                    targetDoor.Interact();
                }

                InteractButton targetButton = hit.transform.GetComponent<InteractButton>();
                if (targetButton != null)
                {
                    targetButton.Interact();
                }

            }
        }
    }
}

