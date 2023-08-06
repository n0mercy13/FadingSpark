using System;
using Codebase.Services.Factory;
using Codebase.Services.StaticData;
using Codebase.StaticData;
using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Logic.PlayerComponents.Manager
{
    public partial class PlayerManager
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIManager _uiManager;
        private readonly IEnergy _playerEnergy;

        private UI_Bar_PlayerEnergy _energyBar;
        private Player _player;

        public PlayerManager(
            IPlayerFactory playerFactory,
            IStaticDataService staticDataService,
            IUIManager uiManager,
            IEnergy playerEnergy)
        {
            _playerFactory = playerFactory;
            _staticDataService = staticDataService;
            _uiManager = uiManager;
            _playerEnergy = playerEnergy;

            _playerEnergy.Changed += OnPlayerEnergyChanged;
            _playerEnergy.Died += OnPlayerDied;
        }

        private void OnPlayerDied() => 
            _player.gameObject.SetActive(false);

        private void OnPlayerEnergyChanged(int current, int max) => 
            _energyBar.SetValues(current, max);

        private void ResetPlayer()
        {
            PlayerStaticData playerData = _staticDataService.ForPlayer();

            SetEnergyBar();
            SetPlayerPosition(playerData);

            _player.Reset();
            _player.gameObject.SetActive(true);
        }

        private void SetEnergyBar()
        {
            if(_energyBar == null
                && _uiManager.TryGetUIComponent<UI_HUD, UI_Bar_PlayerEnergy>(
                    out UI_Bar_PlayerEnergy energyBar))
            {
                _energyBar = energyBar;
            }            
        }

        private void SetPlayerPosition(PlayerStaticData playerData)
        {
            _player.transform.position = playerData.InitialPosition;
        }
    }

    public partial class PlayerManager : IPlayerManager
    {
        public void Reset()
        {
            _player ??= _playerFactory.Create();

            ResetPlayer();           
        }
    }

    public partial class PlayerManager : IDisposable
    {
        public void Dispose()
        {
            _playerEnergy.Changed -= OnPlayerEnergyChanged;
            _playerEnergy.Died -= OnPlayerDied;
        }
    }
}