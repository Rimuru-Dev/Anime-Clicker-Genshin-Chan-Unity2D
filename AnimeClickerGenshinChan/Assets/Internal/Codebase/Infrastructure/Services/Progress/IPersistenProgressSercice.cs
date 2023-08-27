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
using AbyssMoth.Internal.Codebase.Runtime.GameData;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.Progress
{
    public interface IPersistenProgressSercice
    {
        public event Action OnUpdateFillingProgress;
        public Storage CurrentStorage { get; set; }
        public void StartAutoSave();
        public void Save();
    }
}