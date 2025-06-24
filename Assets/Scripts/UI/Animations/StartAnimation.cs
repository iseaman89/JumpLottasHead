using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Animations
{
    public class StartAnimation
    {
        private readonly Image _image;
        private readonly TextMeshProUGUI _label;

        public StartAnimation(GameObject startGameWindow)
        {
            _image = startGameWindow.GetComponentInChildren<Image>();
            _label = startGameWindow.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Enter()
        {
            _image.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InBack);
            _label.transform.DOScale(Vector3.one, .5f).SetEase(Ease.InBack);
        }

        public void Exit()
        {
            _image.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack);
            _label.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack);
        }
    }
}