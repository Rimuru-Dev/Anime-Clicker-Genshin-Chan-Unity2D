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
using Zenject;
using UnityEngine;
using UnityEngine.Serialization;
using AbyssMoth.Internal.Codebase.Runtime.UI;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;
using AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours;

namespace AbyssMoth.Internal.Codebase.Runtime.ShopLogic
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class Shop : MonoBehaviour
    {
        private const int RewardClickID = 10;
        private const int RewardSecondID = 20;

        [FormerlySerializedAs("yandexSDKWrapper")] [SerializeField]
        private YandexSDKHelper yandexSDKHelper;

        private UIHandler uiHandler;
        private ShopConfig shopConfig;
        private HorizontalCanvasUIView uiView;
        private IPersistenProgressSercice progressSercice;

        [Inject]
        public void Constructor(
            IPersistenProgressSercice progressService,
            YandexGame yandexGame,
            ShopConfig config,
            UIHandler ui,
            HorizontalCanvasUIView view)
        {
            uiView = view;
            uiHandler = ui;
            shopConfig = config;
            progressSercice = progressService;
        }

        private void Start()
        {
            uiView.advUpgradeSecond.onClick.AddListener(yandexSDKHelper.ShowAdvButtonSecond);
            uiView.advUpgradeClick.onClick.AddListener(yandexSDKHelper.ShowAdvButtonClick);

            uiView.buyHeroButton.onClick.AddListener(BuyHero);
            uiView.buyBackgroundButton.onClick.AddListener(BuyBackground);

            uiView.upgradeClickButton.onClick.AddListener(UpgradeClick);
            uiView.upgradeSecondButton.onClick.AddListener(UpgradeSecond);
        }

        private void OnDestroy()
        {
            uiView.advUpgradeSecond.onClick.RemoveListener(yandexSDKHelper.ShowAdvButtonSecond);
            uiView.advUpgradeClick.onClick.RemoveListener(yandexSDKHelper.ShowAdvButtonClick);

            uiView.buyHeroButton.onClick.RemoveListener(BuyHero);
            uiView.buyBackgroundButton.onClick.RemoveListener(BuyBackground);

            uiView.upgradeClickButton.onClick.RemoveListener(UpgradeClick);
            uiView.upgradeSecondButton.onClick.RemoveListener(UpgradeSecond);

            YandexGame.RewardVideoEvent -= Rewarded;
        }

        private void OnEnable() =>
            YandexGame.RewardVideoEvent += Rewarded;

        private void OnDisable() =>
            YandexGame.RewardVideoEvent -= Rewarded;

        private void Rewarded(int id)
        {
            switch (id)
            {
                case RewardClickID:
                {
                    if (progressSercice.CurrentStorage.advMultiplyPerClick <= 0)
                        progressSercice.CurrentStorage.advMultiplyPerClick = 2;
                    else
                        progressSercice.CurrentStorage.advMultiplyPerClick += 1;
                }
                    break;
                case RewardSecondID:
                {
                    if (progressSercice.CurrentStorage.advMultiplyPerSecond <= 0)
                        progressSercice.CurrentStorage.advMultiplyPerSecond = 2;
                    else
                        progressSercice.CurrentStorage.advMultiplyPerSecond += 1;
                }
                    break;
            }
        }

        // *** TODO: Remove Magic number! And generalize the logic! *** //
        private void BuyHero()
        {
            if (progressSercice.CurrentStorage.currancy >= progressSercice.CurrentStorage.currentHeroPrice)
            {
                progressSercice.CurrentStorage.currancy -= progressSercice.CurrentStorage.currentHeroPrice;
                progressSercice.CurrentStorage.currentHeroPrice *= 3.5d;

                if (progressSercice.CurrentStorage.currentHeroID < shopConfig.Heroes.Count)
                {
                    progressSercice.CurrentStorage.currentHeroID++;
                    uiView.heroImage.sprite = shopConfig.Heroes[progressSercice.CurrentStorage.currentHeroID];
                }
                else if (progressSercice.CurrentStorage.currentHeroID == shopConfig.Heroes.Count)
                {
                    uiView.heroImage.sprite = shopConfig.Heroes[^1];
                    progressSercice.CurrentStorage.currentHeroID++;
                }
                else if (progressSercice.CurrentStorage.currentHeroID > shopConfig.Heroes.Count)
                {
                    progressSercice.CurrentStorage.currentHeroID = 0;
                    uiView.heroImage.sprite = shopConfig.Heroes[progressSercice.CurrentStorage.currentHeroID];
                }

                uiHandler.UpdateUI();

                progressSercice.Save();
            }

            yandexSDKHelper.FullScreenShow();
        }

        private void BuyBackground()
        {
            if (progressSercice.CurrentStorage.currancy >= progressSercice.CurrentStorage.currentBackgroundPrice)
            {
                progressSercice.CurrentStorage.currancy -= progressSercice.CurrentStorage.currentBackgroundPrice;
                progressSercice.CurrentStorage.currentBackgroundPrice *= 5d;

                if (progressSercice.CurrentStorage.currentBackgroudID < shopConfig.Backgrouns.Count)
                {
                    progressSercice.CurrentStorage.currentBackgroudID++;
                    uiView.background.sprite = shopConfig.Backgrouns[progressSercice.CurrentStorage.currentBackgroudID];
                }
                else if (progressSercice.CurrentStorage.currentBackgroudID == shopConfig.Backgrouns.Count)
                {
                    uiView.background.sprite = shopConfig.Backgrouns[^1];
                    progressSercice.CurrentStorage.currentBackgroudID++;
                }
                else if (progressSercice.CurrentStorage.currentBackgroudID > shopConfig.Backgrouns.Count)
                {
                    progressSercice.CurrentStorage.currentBackgroudID = 0;
                    uiView.background.sprite = shopConfig.Backgrouns[progressSercice.CurrentStorage.currentBackgroudID];
                }

                uiHandler.UpdateUI();

                progressSercice.Save();
            }

            yandexSDKHelper.FullScreenShow();
        }

        private void UpgradeClick()
        {
            if (progressSercice.CurrentStorage.currancy >= progressSercice.CurrentStorage.powerPerClickCost)
            {
                progressSercice.CurrentStorage.currancy -= progressSercice.CurrentStorage.powerPerClickCost;

                progressSercice.CurrentStorage.powerPerClickCost *= 1.16d;
                progressSercice.CurrentStorage.powerPerClickBonus *= 1.14d;

                progressSercice.CurrentStorage.powerPerClick += progressSercice.CurrentStorage.powerPerClickBonus;

                uiHandler.UpdateUI();

                progressSercice.Save();
            }

            yandexSDKHelper.FullScreenShow();
        }

        private void UpgradeSecond()
        {
            if (progressSercice.CurrentStorage.currancy >= progressSercice.CurrentStorage.powerPerSecondCost)
            {
                progressSercice.CurrentStorage.currancy -= progressSercice.CurrentStorage.powerPerSecondCost;

                progressSercice.CurrentStorage.powerPerSecondCost *= 1.16d;
                progressSercice.CurrentStorage.powerPerSecondBonus *= 1.14d;

                progressSercice.CurrentStorage.powerPerSecond += progressSercice.CurrentStorage.powerPerSecondBonus;

                uiHandler.UpdateUI();

                progressSercice.Save();
            }

            yandexSDKHelper.FullScreenShow();
        }
    }
}