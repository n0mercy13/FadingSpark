﻿using UnityEngine;
using Codebase.PlayerComponents;

namespace Codebase.Services.Factory
{
    public interface IPlayerFactory : IService
    {
        Player CreatePlayer(Vector3 at);
    }
}