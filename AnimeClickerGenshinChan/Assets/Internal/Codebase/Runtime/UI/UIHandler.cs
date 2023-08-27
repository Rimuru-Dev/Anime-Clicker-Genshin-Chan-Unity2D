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
using AbyssMoth.Internal.Codebase.Runtime.Audio;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;
using AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.NumberAbbreviator;

namespace AbyssMoth.Internal.Codebase.Runtime.UI
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class UIHandler : MonoBehaviour
    {
        [SerializeField] private YandexSDKHelper yandexSDK;

        [Inject] private HorizontalCanvasUIView uiView;
        [Inject] private ShopConfig shopConfig;
        [Inject] private INumberAbbreviatorService abbreviator;
        [Inject] private IPersistenProgressSercice persistenProgress;
        [Inject] private AudioHandler audioHandler;
        private bool isInitUpdate = true;

        private void Awake() =>
            persistenProgress.OnUpdateFillingProgress += UpdateFillingProgress;

        private void Start() =>
            UpdateUI();

        private void OnDestroy() =>
            persistenProgress.OnUpdateFillingProgress -= UpdateFillingProgress;

        private void UpdateFillingProgress()
        {
            if (persistenProgress.CurrentStorage.currentHeroID < shopConfig.Heroes.Count)
            {
                uiView.heroImage.sprite = shopConfig.Heroes[persistenProgress.CurrentStorage.currentHeroID];
            }
            else if (persistenProgress.CurrentStorage.currentHeroID == shopConfig.Heroes.Count)
            {
                uiView.heroImage.sprite = shopConfig.Heroes[^1];
            }
            else if (persistenProgress.CurrentStorage.currentHeroID > shopConfig.Heroes.Count)
            {
                persistenProgress.CurrentStorage.currentHeroID = 0;
                uiView.heroImage.sprite = shopConfig.Heroes[persistenProgress.CurrentStorage.currentHeroID];
            }

            if (persistenProgress.CurrentStorage.currentBackgroudID < shopConfig.Backgrouns.Count)
            {
                uiView.background.sprite =
                    shopConfig.Backgrouns[persistenProgress.CurrentStorage.currentBackgroudID];
            }
            else if (persistenProgress.CurrentStorage.currentBackgroudID == shopConfig.Backgrouns.Count)
            {
                uiView.background.sprite = shopConfig.Backgrouns[^1];
            }
            else if (persistenProgress.CurrentStorage.currentBackgroudID > shopConfig.Backgrouns.Count)
            {
                persistenProgress.CurrentStorage.currentBackgroudID = 0;
                uiView.background.sprite =
                    shopConfig.Backgrouns[persistenProgress.CurrentStorage.currentBackgroudID];
            }

            audioHandler.UpdateAudioSettings(persistenProgress.CurrentStorage.audioSetting);

            yandexSDK.UpdateLeaderboard();
        }

        // TODO: Optimize ui view updates! Remove hardcode text! Optimize for localization!
        public void UpdateUI()
        {
            // TODO: Remove this!
            if (isInitUpdate)
            {
                isInitUpdate = false;
                UpdateFillingProgress();
            }

            // Currency
            uiView.curractyText.text =
                abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.currancy);

            // Info buy hero and background
            uiView.currentHeroCollection.text =
                $"{persistenProgress.CurrentStorage.currentHeroID}/{shopConfig.Heroes.Count}";
            uiView.currentBackgroundCollection.text =
                $"{persistenProgress.CurrentStorage.currentBackgroudID}/{shopConfig.Backgrouns.Count}";

            uiView.pauseText.text = "Пауза";

            uiView.textNewHero.text = "Новый";
            uiView.buyHeroText.text =
                $"За {abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.currentHeroPrice)}";

            uiView.textNewBackground.text = "Новый";
            uiView.buyBackgroundText.text =
                $"За {abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.currentBackgroundPrice)}";

            uiView.advUpgradeClickText.text = "+1X/клик";
            uiView.advUpgradeSecondText.text = "+1X/сек";

            uiView.upgradeClickText.text =
                $"Улучшить до +{abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.powerPerClickBonus)}/клик за {abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.powerPerClickCost)}";
            uiView.upgradeSecondText.text =
                $"Улучшить до +{abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.powerPerSecondBonus)}/сек за {abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.powerPerSecondCost)}";

            uiView.infoText.text = "Инфо:";

            uiView.perClickText.text =
                $"+{abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.powerPerClick)}/клик";
            uiView.perSecondText.text =
                $"+{abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.powerPerSecond)}/сек";

            uiView.multiplyClickText.text =
                $"X{abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.advMultiplyPerClick)}/клик";
            uiView.multiplySecondText.text =
                $"X{abbreviator.AbbreviateNumber(persistenProgress.CurrentStorage.advMultiplyPerSecond)}/сек";

            uiView.saveButtonText.text = "Сохранить прогресс";
            uiView.resetButtonText.text = "Стереть прогресс";
            uiView.warbibgButtonText.text = "Используйте эту кнопку 'Стереть прогресс' И начать игру с самого начала.";
        }
    }
}