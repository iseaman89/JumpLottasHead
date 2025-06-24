using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI
    {
        private readonly Sprite _soundOn;
        private readonly Sprite _soundOff;
        private readonly MainMenuAnimation _mainMenuAnimation;
        public Button PlayButton { get; private set; }
        public Button SoundButton { get; private set; }

        public MainMenuUI(GameObject mainMenuWindow, Sprite soundOn, Sprite soundOff)
        {
            _soundOn = soundOn;
            _soundOff = soundOff;
            _mainMenuAnimation = new MainMenuAnimation(mainMenuWindow);
            PlayButton = mainMenuWindow.GetComponentsInChildren<Button>()[0];
            SoundButton = mainMenuWindow.GetComponentsInChildren<Button>()[1];
            LoadSpriteForSoundButton();
        }

        public void Show()
        {
            _mainMenuAnimation.Enter();
            SoundButton.onClick.AddListener(OnSoundButtonClicked);
        }

        public void Hide()
        {
            _mainMenuAnimation.Exit();
            SoundButton.onClick.RemoveListener(OnSoundButtonClicked);
        }

        private void LoadSpriteForSoundButton()
        {
            var isOn = PlayerPrefs.GetInt("SoundEnabled") == 1;
            SoundButton.image.sprite = isOn ? _soundOn : _soundOff;
        }

        private void OnSoundButtonClicked()
        {
            var isOn = PlayerPrefs.GetInt("SoundEnabled") == 1;
            SoundButton.image.sprite = !isOn ? _soundOn : _soundOff;
        }
    }
}