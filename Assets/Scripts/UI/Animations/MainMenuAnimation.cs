using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace UI.Animations
{
    public class MainMenuAnimation
    {
        private readonly HorizontalLayoutGroup _buttonsContainer;
        private readonly TextMeshProUGUI _label;
        private readonly Button _soundButton;

        public MainMenuAnimation(GameObject mainMenuWindow)
        {
            _buttonsContainer = mainMenuWindow.GetComponentInChildren<HorizontalLayoutGroup>();
            _label = mainMenuWindow.GetComponentInChildren<TextMeshProUGUI>();
            _soundButton = mainMenuWindow.GetComponentsInChildren<Button>()[1];
        }

        public void Enter()
        {
            _label.transform.DOMoveY(2, .5f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                _label.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            });
            _buttonsContainer.transform.DOMoveY(-3, .5f).SetEase(Ease.InBounce);
            _soundButton.transform.DOMoveY(4.3f, .5f).SetEase(Ease.InBounce);
        }

        public void Exit()
        {
            _label.transform.DOMoveY(8, .5f).SetEase(Ease.OutBounce);
            _buttonsContainer.transform.DOMoveY(-10, .5f).SetEase(Ease.OutBounce);
            _soundButton.transform.DOMoveY(8, .5f).SetEase(Ease.OutBounce);
        }
    }
}