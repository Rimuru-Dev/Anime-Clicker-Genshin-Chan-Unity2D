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
    public sealed class GearAnimation : MonoBehaviour
    {
        public RectTransform gearIcon;
        public Button settingsButton;
        public float animationDuration = 0.5f;
        public int rotationAmount = 360;
        private bool isAnimating;

        private void Start() =>
            settingsButton.onClick.AddListener(AnimateGear);

        private void OnDestroy() =>
            settingsButton.onClick.RemoveListener(AnimateGear);

        private void AnimateGear()
        {
            if (isAnimating)
                return;

            isAnimating = true;

            gearIcon.DORotate(new Vector3(0, 0, rotationAmount), animationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutElastic)
                .OnComplete(Revers);
        }

        private void Revers()
        {
            gearIcon.DORotate(Vector3.zero, animationDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => { isAnimating = false; });
        }
    }
}