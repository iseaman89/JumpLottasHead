using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class WorldObjectsSpawner
    {
        private readonly WorldObjectConfig[] _worldObjectConfig;
        private readonly Updater _updater;
        private List<WorldObjectMovement> _objectMovements = new();

        public WorldObjectsSpawner(WorldObjectConfig[] worldObjectConfig, Updater updater)
        {
            _worldObjectConfig = worldObjectConfig;
            _updater = updater;
        }

        public void Init()
        {
            foreach (var config in _worldObjectConfig)
            {
                var worldObjectFactory = new WorldObjectFactory(config);
                var worldObject = worldObjectFactory.Create();
                var materiel = worldObject.GetComponent<Renderer>().material;
                var movement = new WorldObjectMovement(config, _updater, materiel);
                _objectMovements.Add(movement);
            }
        }

        public void Start()
        {
            foreach (var movement in _objectMovements)
            {
                movement.Start();
            }
        }
        
        public void Stop()
        {
            foreach (var movement in _objectMovements)
            {
                movement.Stop();
            }
        }
    }
}