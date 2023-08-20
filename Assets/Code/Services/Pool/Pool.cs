using System;
using System.Collections.Generic;
using UnityEngine;
using Codebase.Services.Factory;

namespace Codebase.Services.Pool
{
    public partial class Pool<TFactory, TSpawnableBase> 
        where TFactory : IFactory<TSpawnableBase>
        where TSpawnableBase : MonoBehaviour
    {
        private const int InitialCount = 10;

        private readonly TFactory _factory;
        private readonly Dictionary<Type, List<TSpawnableBase>> _spawnables = new();

        private TSpawnableBase _spawnable;

        public Pool(TFactory factory) => 
            _factory = factory;
                
        private void AddNewSpawnable<TSpawnable>() where TSpawnable : TSpawnableBase
        {
            List<TSpawnableBase> newSpawnables = new();

            for(int i = 0; i < InitialCount; i++)
            {
                _spawnable = Instantiate<TSpawnable>();
                newSpawnables.Add(_spawnable);
            }

            _spawnables.Add(typeof(TSpawnable), newSpawnables);
        }

        private void DoubleCapacity<TSpawnable>() where TSpawnable : TSpawnableBase
        {
            const int two = 2;

            List<TSpawnableBase> spawnablesList = _spawnables[typeof(TSpawnable)];

            for(int i = spawnablesList.Count; i < spawnablesList.Count * two; i++)
            {
                _spawnable = Instantiate<TSpawnable>();
                spawnablesList.Add(_spawnable);
            }
        }

        private TSpawnableBase Instantiate<TSpawnable>() where TSpawnable : TSpawnableBase
        {
            _spawnable = _factory.Create<TSpawnable>();
            _spawnable.gameObject.SetActive(false);

            return _spawnable;
        }
    }

    public partial class Pool<TFactory, TSpawnableBase> : IPool<TSpawnableBase>
    {
        public TSpawnable Spawn<TSpawnable>(Vector3 position) where TSpawnable : TSpawnableBase
        {
            if(_spawnables.TryGetValue(typeof(TSpawnable), out List<TSpawnableBase> spawnables))
            {
                _spawnable = null;

                for (int i = 0; i < spawnables.Count; i++)
                {
                    if (spawnables[i].isActiveAndEnabled.Equals(false))
                    {
                        _spawnable = spawnables[i];

                        break;
                    }
                }

                if (_spawnable.Equals(null))
                {
                    DoubleCapacity<TSpawnable>();

                    return Spawn<TSpawnable>(position);
                }

                _spawnable.transform.position = position;
                _spawnable.gameObject.SetActive(true);

                return _spawnable as TSpawnable;
            }
            else
            {
                AddNewSpawnable<TSpawnable>();

                return Spawn<TSpawnable>(position);
            }
        }
    }
}