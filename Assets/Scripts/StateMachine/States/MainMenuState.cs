using Audios;
using LoadSave;
using PlayerStuff;
using UI;
using UnityEngine;

namespace StateMachine.States
{
    public class MainMenuState : IState
    {
        private readonly MainMenuUI _mainMenuUI;
        private readonly GameStateMachine _stateMachine;
        private readonly Player _player;
        private readonly AudioPlayer _audioPlayer;

        public MainMenuState(MainMenuUI mainMenuUI, GameStateMachine stateMachine, Player player, AudioPlayer audioPlayer)
        {
            _mainMenuUI = mainMenuUI;
            _stateMachine = stateMachine;
            _player = player;
            _audioPlayer = audioPlayer;
        }
        public void Enter()
        {
            _player.SetActive(false);
            PlayMusic();
            _mainMenuUI.Show();
            _mainMenuUI.PlayButton.onClick.AddListener(StartGame);
            _mainMenuUI.SoundButton.onClick.AddListener(ToggleSound);
        }

        public void Exit()
        {
            StopMusic();
            _mainMenuUI.Hide();
            _mainMenuUI.PlayButton.onClick.RemoveListener(StartGame);
            _mainMenuUI.SoundButton.onClick.RemoveListener(ToggleSound);
        }

        private void PlayMusic()
        {
            _audioPlayer.PlayMusic($"MainMenu{UnityEngine.Random.Range(1, 7)}");
        }

        private void StopMusic() => _audioPlayer.StopMusic();

        private void ToggleSound()
        {
            var isOn = PlayerPrefs.GetInt("SoundEnabled") == 1;
            _audioPlayer.ToggleSound(!isOn);
        }

        private void StartGame()
        {
            _stateMachine.SetState<StartGameState>();
        }
    }
}