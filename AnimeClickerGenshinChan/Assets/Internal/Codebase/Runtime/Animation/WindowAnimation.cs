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

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Runtime.Animation
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class WindowAnimation : MonoBehaviour
    {
        [Header("Window")]
        public RectTransform windowRectTransform;
        public CanvasGroup canvasGroup;

        [Header("Open/Close Button's")] 
        public Button closeButton;
        public Button showButton;

        [Header("Position Window For Animation")]
        public RectTransform startPosition;
        public RectTransform endPosition;

        private Tween currentAnimation;

        private void Start()
        {
            closeButton.onClick.AddListener(HideWindow);
            showButton.onClick.AddListener(ShowWindow);

            windowRectTransform.SetParent(startPosition, false);
            windowRectTransform.localPosition = Vector3.zero;
            windowRectTransform.localScale = Vector3.zero;
            canvasGroup.alpha = 0f;
        }

        private void ShowWindow()
        {
            if (currentAnimation != null && currentAnimation.IsActive())
                currentAnimation.Kill();

            showButton.interactable = false;

            windowRectTransform.SetParent(endPosition, false);
            windowRectTransform.localPosition = Vector3.zero;
            windowRectTransform.localScale = Vector3.zero;

            currentAnimation = DOTween.Sequence()
                .Join(windowRectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
                .Join(canvasGroup.DOFade(1f, 0.5f));
        }

        private void HideWindow()
        {
            if (currentAnimation != null && currentAnimation.IsActive())
                currentAnimation.Kill();

            showButton.interactable = true;
            closeButton.interactable = false;

            currentAnimation = DOTween.Sequence()
                .Join(windowRectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack))
                .Join(canvasGroup.DOFade(0f, 0.5f))
                .OnComplete(() =>
                {
                    windowRectTransform.SetParent(startPosition, false);
                    windowRectTransform.localPosition = Vector3.zero;
                    windowRectTransform.localScale = Vector3.zero;
                    canvasGroup.alpha = 0f;
                    closeButton.interactable = true;
                });
        }
    }
}