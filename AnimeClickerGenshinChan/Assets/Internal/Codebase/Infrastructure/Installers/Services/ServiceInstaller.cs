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

using YG;
using System;
using Zenject;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Runtime.Common;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;
using AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.NumberAbbreviator;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Installers.Services
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private YandexGame yandexSDKPrefab;

        public override void InstallBindings() =>
            BindServices();

        private void BindServices()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<INumberAbbreviatorService>().To<NumberAbbreviatorService>().AsSingle();

            var yandexSDKInstace = Instantiate(yandexSDKPrefab) ?? throw new ArgumentNullException($"Instantiate(yandexSDKPrefab)");
            Container.Bind<YandexGame>().FromInstance(yandexSDKInstace).AsSingle();

            var pesistenProgressSercice = new PersistenProgressSercice(this);
            Container.Bind<IPersistenProgressSercice>().FromInstance(pesistenProgressSercice).AsSingle();

            yandexSDKInstace.GetComponent<YandexGamesViewingAdsAddendum>().Constuctor(pesistenProgressSercice);
        }
    }
}