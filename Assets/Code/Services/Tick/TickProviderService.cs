using System;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Services.Tick
{
    public partial class TickProviderService
    {
        private readonly IUpdateProvider _updateProvider;
        private readonly int _framesPerSecond;

        private int _ticksCount;
        private float _tickTimer;
        private bool _canTick;

        public TickProviderService(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;

            updateProvider.Updated += OnUpdate;

            _framesPerSecond = Constants.Screen.FPS_60;
            _ticksCount = 0;
            _canTick = true;
        }

        private void OnUpdate()
        {
            if (_canTick == false)
                return;

            _tickTimer += Time.deltaTime;

            if(_tickTimer >= DeltaTime)
            {
                _tickTimer -= DeltaTime;
                _ticksCount++;

                Ticked.Invoke(_ticksCount);
            }
        }
    }

    public partial class TickProviderService : ITickProviderService
    {
        public event Action<int> Ticked = delegate { };

        public float DeltaTime =>
            1f / _framesPerSecond;

        public void Stop() => 
            _canTick = false;

        public void Resume() => 
            _canTick = true;
    }

    public partial class TickProviderService : IDisposable
    {
        public void Dispose() => 
            _updateProvider.Updated -= OnUpdate;
    }
}