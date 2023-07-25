using System;
using UnityEngine;
using Codebase.StaticData;

namespace Codebase.Services.Tick
{
    public partial class TickProviderService : ITickProviderService
    {
        private readonly IUpdateProvider _updateProvider;
        private readonly int _framesPerSecond;

        private int _ticksCount;
        private float _tickTimer;

        public event Action<int> Ticked = delegate { };

        public TickProviderService(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            updateProvider.Updated += OnUpdate;

            _framesPerSecond = Constants.Screen.FPS_60;
            _ticksCount = 0;
        }

        public float DeltaTime => 
            1f / _framesPerSecond;        

        private void OnUpdate()
        {
            _tickTimer += Time.deltaTime;

            if(_tickTimer >= DeltaTime)
            {
                _tickTimer -= DeltaTime;
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