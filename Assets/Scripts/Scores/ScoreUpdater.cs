using System;
using System.Threading.Tasks;

namespace Scores
{
    public class ScoreUpdater
    {
        private int _toastyGoal = 10;
        private bool _canScore = true;
        public Score Score { get; set; }

        public Action OnValueChanged;
        public Action OnToastyGoalReached;

        public ScoreUpdater(Score score)
        {
            Score = score;
        }

        public void Update()
        {
            if (!_canScore) return;
            Score.LastScore++;
            if (Score.BestScore <= Score.LastScore) Score.BestScore = Score.LastScore;
            ShowToasty();
            OnValueChanged?.Invoke();
            _canScore = false;
            _ = ResetScoreCooldown();
        }

        public void Reset()
        {
            Score.LastScore = 0;
            _toastyGoal = 10;
        }

        private async Task ResetScoreCooldown()
        {
            await Task.Delay(800);
            _canScore = true;
        }

        private void ShowToasty()
        {
            if (Score.LastScore < _toastyGoal) return;
            OnToastyGoalReached?.Invoke();
            _toastyGoal += 10;
        }
    }
}