using System;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private PlayerStaticData _playerData;

        public void Load()
        {
            _playerData = Resources
                .Load<PlayerStaticData>(Constants.StaticDataPaths.Player);
        }

        public PlayerStaticData ForPlayer() 
        {
            if (_playerData != null)
                return _playerData;
            else
                throw new ArgumentNullException($"{typeof(PlayerStaticData)} is not loaded");
        }        
    }
}