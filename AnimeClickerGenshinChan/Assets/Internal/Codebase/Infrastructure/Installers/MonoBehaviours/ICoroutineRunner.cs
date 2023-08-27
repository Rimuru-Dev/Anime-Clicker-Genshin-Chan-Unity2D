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

using UnityEngine;
using System.Collections;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours
{
    public interface ICoroutineRunner
    {
        public void StopAllCoroutines();
        public void StopCoroutine(Coroutine coroutine);
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}