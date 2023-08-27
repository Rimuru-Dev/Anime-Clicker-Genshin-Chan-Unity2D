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

using System;
using DG.Tweening;
using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Runtime.CurtainLogic
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class CurtainView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        private float animationDuration;

        public void Constructor(float animationDurationCooldown = 2.5f)
        {
            animationDuration = animationDurationCooldown;

            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = false;

            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }

        public void ShowCurtain(bool isAnimated, Action callback)
        {
            canvasGroup.DOKill();
            gameObject.SetActive(true);

            if (!isAnimated)
            {
                canvasGroup.alpha = 1;
                callback?.Invoke();
                return;
            }

            canvasGroup.alpha = 0;
            canvasGroup
                .DOFade(1, animationDuration)
                .OnComplete(() => callback?.Invoke());
        }

        public void HideCurtain(bool isAnimated, Action callback)
        {
            canvasGroup.DOKill();

            if (!isAnimated)
            {
                gameObject.SetActive(false);
                callback?.Invoke();

                return;
            }

            canvasGroup
                .DOFade(0, animationDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    callback?.Invoke();
                });
        }

        public void HideCurtain(float startDelay, Action callback)
        {
            canvasGroup
                .DOFade(0, animationDuration)
                .SetDelay(startDelay)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    callback?.Invoke();
                });
        }
    }
}