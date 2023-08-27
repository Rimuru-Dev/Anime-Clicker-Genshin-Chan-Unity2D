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

namespace AbyssMoth.Internal.Codebase.Runtime.CurtainLogic
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class CurtainHandler : MonoBehaviour
    {
        [Inject] private CurtainView curtainView;
        [Inject] private IPersistenProgressSercice persistenProgress;

        private void Awake() =>
            persistenProgress.OnUpdateFillingProgress += HideCurtain;

        private void OnDestroy() =>
            persistenProgress.OnUpdateFillingProgress -= HideCurtain;

        private void HideCurtain()
        {
            curtainView.Constructor();
            curtainView.HideCurtain(true, null);
        }
    }
}