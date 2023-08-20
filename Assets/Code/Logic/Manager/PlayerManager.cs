using System;
using UnityEngine;
using Codebase.UI.Elements;
using Codebase.UI.Manager;
using Codebase.Services.Factory;
using Codebase.Services.StaticData;

namespace Codebase.Logic.PlayerComponents.Manager
{
    public partial class PlayerManager
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUIManager _uiManager;
        private readonly PlayerFactory _factory;

        private UI_Bar_PlayerEnergy _energyBar;
        private Player _player;

        public PlayerManager(
            IUIManager uiManager,
            PlayerFactory factory,
            IStaticDataService staticDataService)
        {
            _uiManager = uiManager;
            _factory = factory;
            _staticDataService = staticDataService;
        }

        private void OnPlayerDied() => 
            _player.Dispose();

        private void OnPlayerEnergyChanged(int current, int max) => 
            _energyBar.SetValues(current, max);

        private void SetEnergyBar()
        {
            if(_energyBar == null
                && _uiManager.TryGetUIComponent<UI_HUD, UI_Bar_PlayerEnergy>(
                    out UI_Bar_PlayerEnergy energyBar))
            {
                _energyBar = energyBar;
            }            
        }
    }

    public partial class PlayerManager : IPlayerManager
    {
        public Transform Player =>
            _player.transform;

        public void Spawn()
        {
            if (_player == null)
            {
                SetEnergyBar();

                _player = _factory
                    .Create(_staticDataService.ForPlayer());

                _player.Energy.Changed += OnPlayerEnergyChanged;
                _player.Energy.Died += OnPlayerDied;
            }

            // Spawn
            _player.Reset();
        }
    }

    public partial class PlayerManager : IDisposable
    {
        public void Dispose()
        {
            _player.Energy.Changed -= OnPlayerEnergyChanged;
            _player.Energy.Died -= OnPlayerDied;
        }
    }
}