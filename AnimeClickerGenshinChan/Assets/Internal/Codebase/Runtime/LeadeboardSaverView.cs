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

using Zenject;
using UnityEngine;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;

namespace AbyssMoth.Internal.Codebase.Runtime
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class LeadeboardSaverView : MonoBehaviour
    {
        [SerializeField] private YandexSDKHelper yandexSDKHelper;

        [Inject] private IPersistenProgressSercice persistenProgress;
        private Coroutine checkLeaderboardCoroutine;

        private void Start()
        {
            persistenProgress.StartAutoSave();
            checkLeaderboardCoroutine = StartCoroutine(yandexSDKHelper.CheckLeaderboardScore());
        }

        private void OnDisable()
        {
            yandexSDKHelper.CalculateLeaderboard();
            persistenProgress.Save();
        }

        private void OnDestroy()
        {
            yandexSDKHelper.CalculateLeaderboard();
            persistenProgress.Save();

            if (checkLeaderboardCoroutine != null)
                StopCoroutine(checkLeaderboardCoroutine);
        }
    }
}