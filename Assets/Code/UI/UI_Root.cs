using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.UI
{
    public class UI_Root : MonoBehaviour, ICoroutineRunner
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}