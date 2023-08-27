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

namespace AbyssMoth.Internal.Codebase.Infrastructure.Services.NumberAbbreviator
{
    public interface INumberAbbreviatorService
    {
        public string AbbreviateNumber(double value, bool includeSuffixName = false);
    }
}