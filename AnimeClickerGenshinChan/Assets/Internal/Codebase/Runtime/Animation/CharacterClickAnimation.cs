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
    public sealed class CharacterClickAnimation : MonoBehaviour
    {
        public Image characterImage;
        public Button button;

        public float scaleDuration = 0.1f;
        public float scaleAmount = 1.2f;

        private bool isAnimating;

        private void Start() =>
            button.onClick.AddListener(AnimateCharacterClick);

        private void OnDestroy() =>
            button.onClick.RemoveListener(AnimateCharacterClick);

        private void AnimateCharacterClick()
        {
            if (isAnimating)
                return;

            isAnimating = true;

            var originalScale = characterImage.transform.localScale;

            characterImage.transform
                .DOScale(originalScale * scaleAmount, scaleDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    ResetCharacterScale();
                    isAnimating = false;
                });
        }

        private void ResetCharacterScale() =>
            characterImage.transform.localScale = Vector3.one;
    }
}