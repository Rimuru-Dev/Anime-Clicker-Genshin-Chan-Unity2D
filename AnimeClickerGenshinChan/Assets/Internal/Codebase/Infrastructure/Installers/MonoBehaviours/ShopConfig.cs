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
using UnityEngine;
using System.Collections.Generic;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours
{
    [Serializable]
    public sealed class ShopConfig
    {
        [field: SerializeField] public List<Sprite> Heroes { get; private set; }
        [field: SerializeField] public List<Sprite> Backgrouns { get; private set; }
    }
}