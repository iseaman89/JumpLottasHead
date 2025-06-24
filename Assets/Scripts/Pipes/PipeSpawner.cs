using System.Collections;
using System.Threading.Tasks;
using Scores;
using UnityEngine;

namespace Pipes
{
    public class PipeSpawner : IUpdateListener
    {
        private readonly PipeConfig _pipeConfig;
        private readonly PipePool _pool;
        private readonly Updater _updater;
        private readonly Score _score;
        private float _timer;
        private float _spawnDelay;
        private float _pipeSpeed;

        public PipeSpawner(PipeConfig pipeConfig, PipePool pool, Updater updater, Score score)
        {
            _pipeConfig = pipeConfig;
            _pool = pool;
            _updater = updater;
            _score = score;
            _spawnDelay = _pipeConfig.SpawnDelay;
            _pipeSpeed = _pipeConfig.Speed;
        }

        public void Start() => _updater.AddListener(this);

        public void Stop()
        {
            _updater.RemoveListener(this);
            _pipeConfig.SpawnDelay = _spawnDelay;
            _pipeConfig.Speed = _pipeSpeed;
        }
        
        private void SpawnPipes()
        {
            var pipe = _pool.GetPooledObject();
            pipe.transform.position = 
                new Vector2(_pipeConfig.SpawnPosX, Random.Range(_pipeConfig.SpawnMinY, _pipeConfig.SpawnMaxY));
        }

        public void LevelUp()
        {
            if (_score.LastScore % 10 != 0) return;
            _pipeConfig.SpawnDelay -= 0.1f;
            _pipeConfig.Speed += 0.1f;
        }

        public void Updater(float deltaTime)
        {
            if (_timer > _pipeConfig.SpawnDelay)
            {
                SpawnPipes();
                _timer = 0;
            }
        
            _timer += deltaTime;
        }
    }
}