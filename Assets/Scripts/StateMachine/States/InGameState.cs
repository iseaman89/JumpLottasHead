using Audios;
using Pipes;
using PlayerStuff;
using Scores;
using UI;
using World;

namespace StateMachine.States
{
    public class InGameState : IState
    {
        private readonly InGameUI _inGameUI;
        private readonly GameStateMachine _stateMachine;
        private readonly Player _player;
        private readonly PipeSpawner _pipeSpawner;
        private readonly ScoreUpdater _scoreUpdater;
        private readonly ScoreUpdaterUI _scoreUpdaterUI;
        private readonly AudioPlayer _audioPlayer;
        private readonly WorldObjectsSpawner _objectsSpawner;

        public InGameState(InGameUI inGameUI, GameStateMachine stateMachine, Player player, PipeSpawner pipeSpawner,
            ScoreUpdater scoreUpdater, ScoreUpdaterUI scoreUpdaterUI, AudioPlayer audioPlayer, WorldObjectsSpawner objectsSpawner)
        {
            _inGameUI = inGameUI;
            _stateMachine = stateMachine;
            _player = player;
            _pipeSpawner = pipeSpawner;
            _scoreUpdater = scoreUpdater;
            _scoreUpdaterUI = scoreUpdaterUI;
            _audioPlayer = audioPlayer;
            _objectsSpawner = objectsSpawner;
        }
        
        public void Enter()
        {
            _player.Activate();
            _player.MoveToGamePosition();
            _pipeSpawner.Start();
            _inGameUI.Show();
            _inGameUI.PauseButton.onClick.AddListener(Pause);
            _player.OnCollision += GameOver;
            _player.OnTrigger += UpdateScore;
            _player.OnJump += PlayJumpSound;
            _scoreUpdater.OnValueChanged += _scoreUpdaterUI.UpdateScore;
            _scoreUpdater.OnValueChanged += PlayUpdateScoreSound;
            _scoreUpdater.OnValueChanged += _pipeSpawner.LevelUp;
            _scoreUpdater.OnToastyGoalReached += ShowToasty;
            _objectsSpawner.Start();
        }

        public void Exit()
        {
            _inGameUI.Hide();
            _inGameUI.PauseButton.onClick.RemoveListener(Pause);
            _player.OnCollision -= GameOver;
            _player.OnTrigger -= UpdateScore;
            _player.OnJump -= PlayJumpSound;
            _scoreUpdater.OnValueChanged -= _scoreUpdaterUI.UpdateScore;
            _scoreUpdater.OnValueChanged -= PlayUpdateScoreSound;
            _scoreUpdater.OnValueChanged -= _pipeSpawner.LevelUp;
            _scoreUpdater.OnToastyGoalReached -= ShowToasty;
            _objectsSpawner.Stop();
        }

        private void Pause() => _stateMachine.SetState<PauseState>();

        private void GameOver() 
        {
            _audioPlayer.PlaySound("Die");
            _stateMachine.SetState<GameOverState>();
        }

        private void UpdateScore()
        {
            _scoreUpdater.Update();
        }
        
        private void PlayUpdateScoreSound() => _audioPlayer.PlaySound("Score");

        private void ShowToasty()
        {
            _audioPlayer.PlaySound(  "Toasty");
            _inGameUI.InGameAnimation.ShowToasty();
        }

        private void PlayJumpSound() => _audioPlayer.PlaySound("Jump");
    }
}