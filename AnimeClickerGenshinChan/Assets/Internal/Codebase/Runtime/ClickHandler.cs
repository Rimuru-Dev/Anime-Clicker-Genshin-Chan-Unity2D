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
using System.Collections;
using AbyssMoth.Internal.Codebase.Runtime.UI;
using AbyssMoth.Internal.Codebase.Runtime.Audio;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;

namespace AbyssMoth.Internal.Codebase.Runtime
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class ClickHandler : MonoBehaviour
    {
        [SerializeField] private float autoClickCooldown = 1f;

        [Inject] private UIHandler uiHandler;
        [Inject] private HorizontalCanvasUIView view;
        [Inject] private AudioHandler audioHandler;
        [Inject] private IPersistenProgressSercice persistenProgress;

        private void Start()
        {
            view.heroButton.onClick.AddListener(Click);
            StartCoroutine(AutoClick());
        }

        private void OnDestroy() =>
            view.heroButton.onClick.RemoveAllListeners();

        private void Click()
        {
            audioHandler.ClickHeroSFX();

            var storage = persistenProgress.CurrentStorage;

            if (storage.advMultiplyPerClick > 0)
                storage.currancy += (storage.powerPerClick * storage.advMultiplyPerClick);
            else
                storage.currancy += storage.powerPerClick;

            uiHandler.UpdateUI();
        }

        private IEnumerator AutoClick()
        {
            while (true)
            {
                var storage = persistenProgress.CurrentStorage;

                if (storage.advMultiplyPerSecond > 0)
                    storage.currancy += (storage.powerPerSecond * storage.advMultiplyPerSecond);
                else
                    storage.currancy += storage.powerPerSecond;

                uiHandler.UpdateUI();

                yield return new WaitForSeconds(autoClickCooldown);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}