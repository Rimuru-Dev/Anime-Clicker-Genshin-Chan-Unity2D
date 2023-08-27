// ReSharper disable CommentTypo
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - Gists:    https://gist.github.com/RimuruDev/61e9f0111b35d3e67ef18fab611d7595
//          - GitHub:   https://github.com/RimuruDev
// **************************************************************** //

using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Common.Components
{
    /// <summary>
    /// Automatically resize backgrounds based on SpriteRenderer. You can change the screen format any way you want, even 1920x1080 or 5000x800 the background will not go out of the screen.
    /// </summary>
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class BackgroundScaler : MonoBehaviour
    {
        [SerializeField] private Camera cameraRenderer;
        private SpriteRenderer backgroundSpriteRenderer;

        private void Awake() =>
            backgroundSpriteRenderer = GetComponent<SpriteRenderer>();

        private void Start() =>
            ScaleBackground();

        private void LateUpdate() =>
            ScaleBackground();

        private void ScaleBackground()
        {
            var targetHeight = cameraRenderer.orthographicSize * 2;
            var targetWidth = targetHeight * Screen.width / Screen.height;

            var backgroundSize = backgroundSpriteRenderer.sprite.bounds.size;
            var targetScale = Vector3.one;
            var widthRatio = targetWidth / backgroundSize.x;
            var heightRatio = targetHeight / backgroundSize.y;

            if (widthRatio > heightRatio)
                targetScale *= widthRatio;
            else
                targetScale *= heightRatio;

            transform.localScale = targetScale;
        }
    }
}