using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Delore.UI
{
    public class InteractionsDetection : MonoBehaviour
    {
        [SerializeField] GameObject chestPanel;

        private bool playerCanOpen = false;

        void Start()
        {
            chestPanel.SetActive(false);
        }
        void Update()
        {
            if (playerCanOpen && Input.GetKeyDown(KeyCode.E))
            {
                chestPanel.SetActive(!chestPanel.activeSelf);
                Cursor.visible = chestPanel.activeSelf;
                if (chestPanel.activeSelf)
                    Cursor.lockState = CursorLockMode.None;
                else
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            playerCanOpen = true;
        }

        private void OnTriggerExit(Collider other)
        {
            playerCanOpen = false;
            chestPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}