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

using YG;
using System;
using Zenject;
using UnityEngine;
using System.Collections;
using AbyssMoth.Internal.Codebase.Runtime.Timer;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;

namespace AbyssMoth.Internal.Codebase.Runtime
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class YandexSDKHelper : MonoBehaviour
    {
        [SerializeField] private TimerVisualization timerVisualization;

        [Inject] private YandexGame yandexGame;
        [Inject] private IPersistenProgressSercice persistenProgress;

        private const int RewardClickID = 10;
        private const int RewardSecondID = 20;
        private const string LeaderBoardID = "AnimeClickerGenshinChan";

        private void Start() =>
            YandexGame.FullscreenShow();

        public void UpdateLeaderboard() =>
            YandexGame.NewLeaderboardScores(LeaderBoardID, persistenProgress.CurrentStorage.bestScore);

        public void CalculateLeaderboard()
        {
            if (persistenProgress.CurrentStorage.currancy > persistenProgress.CurrentStorage.bestScore)
            {
                persistenProgress.CurrentStorage.bestScore =
                    (int)Math.Round(persistenProgress.CurrentStorage.currancy);

                if (persistenProgress.CurrentStorage.bestScore >= int.MaxValue)
                    persistenProgress.CurrentStorage.bestScore = int.MaxValue;

                YandexGame.savesData.storage = persistenProgress.CurrentStorage;

                UpdateLeaderboard();
            }

            persistenProgress.Save();
        }

        public void ResetShowAdTimer() =>
            YandexGame.timerShowAd = 0;

        public void ShowAdvButtonClick() =>
            yandexGame._RewardedShow(RewardClickID);

        public void ShowAdvButtonSecond() =>
            yandexGame._RewardedShow(RewardSecondID);

        public IEnumerator CheckLeaderboardScore()
        {
            yield return new WaitForSeconds(10);

            if (persistenProgress.CurrentStorage.currancy > persistenProgress.CurrentStorage.bestScore)
            {
                persistenProgress.CurrentStorage.bestScore =
                    (int)Math.Round(persistenProgress.CurrentStorage.currancy);

                if (persistenProgress.CurrentStorage.bestScore >= int.MaxValue)
                    persistenProgress.CurrentStorage.bestScore = int.MaxValue;

                YandexGame.savesData.storage = persistenProgress.CurrentStorage;

                UpdateLeaderboard();

                persistenProgress.Save();
            }
        }

        public void FullScreenShow()
        {
            if (!YandexGame.nowFullAd && !YandexGame.nowVideoAd &&
                YandexGame.timerShowAd >= yandexGame.infoYG.fullscreenAdInterval)
            {
                YandexGame.timerShowAd = 0;

                timerVisualization.StartCountdown(OpenFullScreen);
            }
        }

        private void OpenFullScreen()
        {
            YandexGame.timerShowAd = 0;
#if !UNITY_EDITOR
                YandexGame.FullAdShow();
#else
            YandexGame.Message("Full Screen Ad");
            yandexGame.OpenFullAd();
            yandexGame.CloseFullAdInEditor();
#endif
        }
    }
}