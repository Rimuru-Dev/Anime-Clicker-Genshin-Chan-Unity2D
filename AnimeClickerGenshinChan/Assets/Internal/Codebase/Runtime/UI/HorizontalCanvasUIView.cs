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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Runtime.UI
{
    public sealed class HorizontalCanvasUIView : MonoBehaviour
    {
        [Header("Hero")]
        public Image heroImage;
        public Button heroButton;
        
        [Header("Background")]
        public SpriteRenderer background;
        
        [Header("Currency")]
        public TextMeshProUGUI curractyText;

        [Header("Upgrade Click")]
        public TextMeshProUGUI upgradeClickText;
        public Button upgradeClickButton;

        [Header("Upgrade Second")]
        public TextMeshProUGUI upgradeSecondText;
        public Button upgradeSecondButton;

        [Header("Buy Hero")]
        public TextMeshProUGUI buyHeroText;
        public TextMeshProUGUI textNewHero;
        public Button buyHeroButton;

        [Header("Buy Background")]
        public TextMeshProUGUI buyBackgroundText;
        public TextMeshProUGUI textNewBackground;
        public Button buyBackgroundButton;

        [Header("Advertisement Upgrade Click")]
        public Button advUpgradeClick;
        public TextMeshProUGUI advUpgradeClickText;

        [Header("Advertisement Upgrade Second")]
        public Button advUpgradeSecond;
        public TextMeshProUGUI advUpgradeSecondText;

        [Header("Ifo [Hero || Background]")]
        public TextMeshProUGUI currentHeroCollection; 
        public TextMeshProUGUI currentBackgroundCollection;   

        [Header("Audio Panel")]
        public Slider slider;
        public TextMeshProUGUI saveButtonText;
        public TextMeshProUGUI resetButtonText;
        public TextMeshProUGUI warbibgButtonText;

        [Header("Info [Click || Second || Adv Click || Adv Second]")]
        public TextMeshProUGUI infoText;
        public TextMeshProUGUI perClickText;
        public TextMeshProUGUI perSecondText;
        public TextMeshProUGUI multiplyClickText;
        public TextMeshProUGUI multiplySecondText;
        
        [Header("Adv pause")]
        public TextMeshProUGUI pauseText;
    }
}