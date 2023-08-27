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
using AbyssMoth.Plugins.Audio.Core;
using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Runtime.UI;
using AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress;

namespace AbyssMoth.Internal.Codebase.Runtime.Audio
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class AudioHandler : MonoBehaviour
    {
        private const string Music_0 = "Music_0";
        private const string ClickSFX_1 = "ClickSFX_1";

        public SourceAudio bakgroundMusic;
        public SourceAudio clickOnUIElements;

        [Inject] private IPersistenProgressSercice pesistenProgressSercice;
        [Inject] private HorizontalCanvasUIView uiView;

        private void Awake() =>
            uiView.slider.onValueChanged.AddListener(OnValueChanged);

        private void Start() =>
            PlayBackgroundMusic();

        private void OnDestroy() =>
            uiView.slider.onValueChanged.RemoveListener(OnValueChanged);

        public void ClickHeroSFX() =>
            clickOnUIElements.Play(ClickSFX_1);

        public void UpdateAudioSettings(float value)
        {
            pesistenProgressSercice.CurrentStorage.audioSetting = value;

            AudioListener.volume = pesistenProgressSercice.CurrentStorage.audioSetting;

            uiView.slider.value = pesistenProgressSercice.CurrentStorage.audioSetting;
        }

        private void PlayBackgroundMusic()
        {
            bakgroundMusic.Play(Music_0);
            bakgroundMusic.Loop = true;
        }

        private void OnValueChanged(float value)
        {
            AudioListener.volume = value;

            pesistenProgressSercice.CurrentStorage.audioSetting = value;

            YandexGame.savesData.storage.audioSetting = value;
        }
    }
}