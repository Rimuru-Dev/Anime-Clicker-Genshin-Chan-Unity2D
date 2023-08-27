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

namespace AbyssMoth.Internal.Codebase.Runtime.GameData
{
    [Serializable]
    public sealed class Storage
    {
        public double currancy;

        public double powerPerClick;
        public double powerPerSecond;

        public int advMultiplyPerClick;
        public int advMultiplyPerSecond;

        public int currentHeroID;
        public int currentBackgroudID;

        public float audioSetting;
        public int languageID;

        public double currentHeroPrice;
        public double currentBackgroundPrice;

        public double powerPerClickBonus;
        public double powerPerClickCost;

        public double powerPerSecondBonus;
        public double powerPerSecondCost;

        public int bestScore;

        public Storage()
        {
            currancy = 0;

            powerPerClick = 1;
            powerPerSecond = 1;

            audioSetting = 0.3f;

            currentHeroID = 0;
            currentBackgroudID = 0;

            advMultiplyPerClick = 0;
            advMultiplyPerSecond = 0;

            languageID = 0; // 0 - Russian // TODO: Remove magic number

            currentHeroPrice = 1000;
            currentBackgroundPrice = 3000;

            powerPerClickBonus = 1;
            powerPerClickCost = 60;

            powerPerSecondBonus = 1;
            powerPerSecondCost = 70;

            bestScore = 0;
        }
    }
}