﻿using Services;
using Shared;
using UnityEngine;

namespace MainScreen
{
    public class UIController : MonoBehaviour
    {
        /// <summary>
        ///     The main camera
        /// </summary>
        public Camera mainCamera;

        /// <summary>
        ///     The sidebar
        /// </summary>
        public RectTransform sidebar;

        /// <summary>
        ///     The canvas for the main screen
        /// </summary>
        public Canvas mainCanvas;

        /// <summary>
        ///     The two screens
        /// </summary>
        public GameObject startScreen, mainScreen;

        /// <summary>
        ///     The dialog
        /// </summary>
        public DialogController dialog;

        /// <summary>
        ///     The configuration manager
        /// </summary>
        private readonly ConfigurationManager _configManager = ConfigurationManager.Instance;

        /// <summary>
        ///     The current width of the screen
        /// </summary>
        private int _width;

        /// <summary>
        ///     Check if the screen was resized
        /// </summary>
        private void Update()
        {
            var width = Screen.width;
            if (width == _width) return;

            // Get the actual layout sizes
            var localScale = mainCanvas.transform.localScale;
            var sidebarWidth = localScale.x * sidebar.rect.width;

            // Calculate the bounds
            var x = sidebarWidth / width;

            // Set the new bounds
            mainCamera.rect = new Rect(x, 0, 1 - x, 1);
            _width = width;
        }

        /// <summary>
        ///     Close a project
        /// </summary>
        public void CloseProject()
        {
            dialog.Show(
                "Close Project",
                "Are you sure?",
                () =>
                {
                    // Remove the last opened project 
                    _configManager.Config.lastProject = "";
                    _configManager.SaveConfig();

                    // Reset camera
                    _width = 0;
                    mainCamera.rect = new Rect(0, 0, 1, 1);

                    // Show the start screen
                    mainScreen.SetActive(false);
                    startScreen.SetActive(true);
                }
            );
        }
    }
}