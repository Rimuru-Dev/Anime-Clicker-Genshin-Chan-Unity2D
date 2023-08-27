// ReSharper disable CommentTypo
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//
// **************************************************************** //

using TMPro;
using System;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Runtime.Timer
{
    /// <summary>
    /// Classic timer for show AD for your game.
    /// </summary>
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class TimerVisualization : MonoBehaviour
    {
        [SerializeField] private string messageText;
        [SerializeField] private string messageTexten;
        [SerializeField] private float countdownTimer = 3f;
        [SerializeField] private float initialCooldownTimer = 3f;

        [SerializeField] private GameObject popup;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        private Action callback;
        private bool isCountdownStarted;
        private int languageID;

        private void Awake()
        {
            textMeshProUGUI.gameObject.SetActive(false);

            popup.SetActive(false);
        }

        private void Update()
        {
            if (!isCountdownStarted)
                return;

            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0f)
            {
                if (callback != null)
                    callback.Invoke();

                isCountdownStarted = false;


                textMeshProUGUI.gameObject.SetActive(false);
                popup.SetActive(false);
                gameObject.SetActive(false);
            }

            var displayTimer = Mathf.CeilToInt(countdownTimer);

            if (languageID == 0)
                textMeshProUGUI.text = string.Format(messageText, displayTimer);

            if (languageID == 1)
                textMeshProUGUI.text = string.Format(messageTexten, displayTimer);
        }

        /// <summary>
        /// Show AD panel with text "Advertising will begin in {0}...."
        /// <code></code>
        /// 3... 2... 1... call YandexGame.FullscreenShow();
        /// <code></code>
        /// <code>
        ///  === Example ===
        ///  private void FullscreenShow()
        ///  {
        ///      if (YandexGame.timerShowAd >= yandexGame.infoYG.fullscreenAdInterval)
        ///          timerVisualization.StartCountdown(YandexGame.FullscreenShow);
        ///      else
        ///          print(
        ///              $"До запроса к показу Fullscreen рекламы {yandexGame.infoYG.fullscreenAdInterval - YandexGame.timerShowAd:00.0} сек.");
        ///  }
        /// </code>
        /// </summary>
        /// <param name="endCallback"></param>
        /// <param name="id">LanguageID. 0 - ru, 1 - en, 2 - tr</param>
        public void StartCountdown(Action endCallback = null, int id = 0)
        {
            if (isCountdownStarted)
                return;

            callback = endCallback;
            languageID = id;

            countdownTimer = initialCooldownTimer;

            isCountdownStarted = true;

            gameObject.SetActive(true);

            popup.SetActive(true);
            textMeshProUGUI.gameObject.SetActive(true);

            textMeshProUGUI.text = string.Format(messageText, Mathf.CeilToInt(countdownTimer));
        }
    }
}