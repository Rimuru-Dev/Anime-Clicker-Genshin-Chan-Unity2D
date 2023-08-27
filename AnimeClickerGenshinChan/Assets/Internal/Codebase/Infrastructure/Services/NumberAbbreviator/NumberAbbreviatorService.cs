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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.NumberAbbreviator
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class NumberAbbreviatorService : INumberAbbreviatorService
    {
        private readonly List<string> abbreviationsNames;

        public NumberAbbreviatorService()
        {
            abbreviationsNames = new List<string>
            {
                "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion",
                "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion"
            };
        }

        public string AbbreviateNumber(double value, bool includeSuffixName = false)
        {
            var abbreviate = "";
            var suffix = "";

            double logValue = Mathf.Log10(Mathf.Max((float)value, 1));

            switch (logValue)
            {
                case < 3:
                    break;
                case >= 3 and < 6:
                    abbreviate = "K";
                    suffix = abbreviationsNames[0];
                    value /= 1000;
                    break;
                case >= 6 and < 9:
                    abbreviate = "M";
                    suffix = abbreviationsNames[1];
                    value /= 1000000;
                    break;
                case >= 9 and < 12:
                    abbreviate = "B";
                    suffix = abbreviationsNames[2];
                    value /= 1000000000;
                    break;
                case >= 12 and < 15:
                    abbreviate = "T";
                    suffix = abbreviationsNames[3];
                    value /= 1000000000000;
                    break;
                case >= 15 and < 18:
                    abbreviate = "Q";
                    suffix = abbreviationsNames[4];
                    value /= 1000000000000000;
                    break;
                case >= 18 and < 21:
                    abbreviate = "QQ";
                    suffix = abbreviationsNames[5];
                    value /= 1000000000000000000;
                    break;
                case >= 21 and <= (double.MaxValue * 2):
                {
                    if (logValue > 38)
                        return string.Format("Unreal Level!!! {0:E2} {1}", value,
                            includeSuffixName
                                ? $"{abbreviate} {suffix}"
                                : $"{abbreviate}");
                    else
                    {
                        return string.Format("{0:E2} {1}", value,
                            includeSuffixName
                                ? $"{abbreviate} {suffix}"
                                : $"{abbreviate}");
                    }
                }
            }

            return string.Format("{0:0.##} {1}", value, includeSuffixName ? $"{abbreviate} {suffix}" : abbreviate);
        }
    }
}