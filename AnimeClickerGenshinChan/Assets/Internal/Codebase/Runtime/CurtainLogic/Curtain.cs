// ReSharper disable All
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//
// **************************************************************** //

using UnityEngine;
using AbyssMoth.Internal.Codebase.Runtime.CurtainLogic;

namespace RimuruDev.Internal.Codebase.Runtime.Curtain
{
    [CreateAssetMenu(menuName = "StaticData/Create CurtainConfig", fileName = "CurtainConfig", order = 0)]
    public sealed class CurtainConfig : ScriptableObject
    {
        [field: SerializeField] public CurtainView CurtainView { get; private set; }

        [field: Space(20)]
        [field: SerializeField]
        public float HideDelay { get; private set; } = 1.3f;

        [field: SerializeField] public float AnimationDuration { get; private set; } = 1.5f;
    }
}