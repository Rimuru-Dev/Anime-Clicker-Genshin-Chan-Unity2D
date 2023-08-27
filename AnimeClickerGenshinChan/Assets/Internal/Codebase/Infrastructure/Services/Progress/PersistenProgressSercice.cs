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
using UnityEngine;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using AbyssMoth.Internal.Codebase.Runtime.GameData;
using AbyssMoth.Internal.Codebase.Infrastructure.Installers.MonoBehaviours;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class PersistenProgressSercice : IPersistenProgressSercice
    {
        public event Action OnUpdateFillingProgress;

        private const float autosaveCooldown = 4f;
        private readonly ICoroutineRunner coroutineRunner;

        private Storage storage;
        private bool isActivaAutoSave;
        private Coroutine autoSaveCoroutine;

        public Storage CurrentStorage
        {
            get
            {
                if (storage == null)
                    Load();

                return storage;
            }
            set => storage = value;
        }

        public PersistenProgressSercice(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;

            Initialize();
        }

        ~PersistenProgressSercice() =>
            Unsubscribe();

        #region IPersistenProgressSercice inplementation

        public void StartAutoSave()
        {
            isActivaAutoSave = true;
            autoSaveCoroutine = coroutineRunner?.StartCoroutine(AutoSave());
        }

        public void Save()
        {
            if (!YandexGame.SDKEnabled)
                return;

            if (CurrentStorage == null || YandexGame.savesData.storage == null)
                Load();

            YandexGame.savesData.storage = CurrentStorage;
            YandexGame.SaveProgress();
        }

        public void Load()
        {
            if (YandexGame.savesData.storage == null)
                LoadDefault();
            else
                LoadUser();

            OnUpdateFillingProgress?.Invoke();
        }

        #endregion

        #region Method's

        private void Initialize()
        {
            if (YandexGame.SDKEnabled)
                Load();

            Subscribe();
        }

        private void Subscribe() =>
            YandexGame.GetDataEvent += Load;

        private void Unsubscribe() =>
            YandexGame.GetDataEvent -= Load;

        private IEnumerator AutoSave()
        {
            while (isActivaAutoSave)
            {
                Save();

                yield return new WaitForSeconds(autosaveCooldown);
            }
        }

        private void LoadDefault()
        {
            CurrentStorage = new Storage();
            YandexGame.savesData.storage = CurrentStorage;
        }

        private void LoadUser() =>
            CurrentStorage = YandexGame.savesData.storage;

        #endregion
    }
}