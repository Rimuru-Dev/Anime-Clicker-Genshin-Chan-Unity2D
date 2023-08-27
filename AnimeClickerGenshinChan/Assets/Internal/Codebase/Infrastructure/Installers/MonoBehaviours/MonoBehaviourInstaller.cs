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
using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Runtime.UI;
using AbyssMoth.Internal.Codebase.Runtime.Audio;
using AbyssMoth.Internal.Codebase.Runtime.CurtainLogic;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class MonoBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private UIHandler uiHandler;
        [SerializeField] private ShopConfig shopConfig;
        [SerializeField] private CurtainView curtainView;
        [SerializeField] private AudioHandler audioHandler;
        [SerializeField] private HorizontalCanvasUIView horizontalCanvasUIView;

        public override void InstallBindings()
        {
            Container.Bind<UIHandler>().FromInstance(uiHandler).AsSingle().NonLazy();
            Container.Bind<ShopConfig>().FromInstance(shopConfig).AsSingle().NonLazy();
            Container.Bind<CurtainView>().FromInstance(curtainView).AsSingle().NonLazy();
            Container.Bind<AudioHandler>().FromInstance(audioHandler).AsSingle().NonLazy();
            Container.Bind<HorizontalCanvasUIView>().FromInstance(horizontalCanvasUIView).AsSingle().NonLazy();
        }
    }
}