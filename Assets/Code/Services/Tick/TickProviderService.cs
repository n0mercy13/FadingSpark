using System;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Services.Tick
{
    public partial class TickProviderService : ITickProviderService
    {
        private readonly IUpdateProvider _updateProvider;

        private int _ticksCount;
        private float _tickTimer;

        public event Action<int> Ticked = delegate { };

        public TickProviderService(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            updateProvider.Updated += OnUpdate;

            FPS = Constants.Screen.FPS_60;
            _ticksCount = 0;
        }

        public int FPS { get; set; }
        private float _tickTimerMax => 1 / FPS;


        private void OnUpdate()
        {
            _tickTimer += Time.deltaTime;

            if(_tickTimer >= _tickTimerMax)
            {
                _tickTimer -= _tickTimerMax;
                _ticksCount++;

                Ticked.Invoke(_ticksCount);
            }
        }
    }

    public partial class TickProviderService : IDisposable
    {
        public void Dispose() => 
            _updateProvider.Updated -= OnUpdate;
    }
}