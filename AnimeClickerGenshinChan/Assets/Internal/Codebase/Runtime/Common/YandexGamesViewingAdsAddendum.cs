﻿// ReSharper disable CommentTypo
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
// **************************************************************** //

using YG;
using Zenject;
using UnityEngine;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;

namespace AbyssMoth.Internal.Codebase.Runtime.Common
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(YandexGame))]
    [RequireComponent(typeof(ViewingAdsYG))]
    public sealed class YandexGamesViewingAdsAddendum : MonoBehaviour
    {
        [SerializeField] private bool disableComponent;
        [Inject] private IPersistenProgressSercice persistenProgressSercice;

        private const int Off = 0;

        public void Constuctor(IPersistenProgressSercice persistenProgress) =>
            persistenProgressSercice = persistenProgress;

        private void Start()
        {
            if (disableComponent)
                return;

            YandexGame.OpenVideoEvent += OpenAdv;
            YandexGame.CloseVideoEvent += CloseAdv;

            YandexGame.OpenFullAdEvent += OpenAdv;
            YandexGame.CloseFullAdEvent += CloseAdv;
        }

        private void OnDestroy()
        {
            if (disableComponent)
                return;

            YandexGame.OpenVideoEvent -= OpenAdv;
            YandexGame.CloseVideoEvent -= CloseAdv;

            YandexGame.OpenFullAdEvent -= OpenAdv;
            YandexGame.CloseFullAdEvent -= CloseAdv;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (disableComponent)
                return;

            SetGlobalAudioVolume(!hasFocus);
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (disableComponent)
                return;

            SetGlobalAudioVolume(isPaused);
        }

        private static void OpenAdv()
        {
            AudioListener.volume = 0;
            Time.timeScale = 0;
        }

        private void CloseAdv()
        {
            AudioListener.volume = persistenProgressSercice.CurrentStorage.audioSetting;
            Time.timeScale = 0;
        }

        private void SetGlobalAudioVolume(bool silence) =>
            AudioListener.volume = silence ? Off : persistenProgressSercice.CurrentStorage.audioSetting;
    }
}