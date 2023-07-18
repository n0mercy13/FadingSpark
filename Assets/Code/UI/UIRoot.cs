using UnityEngine;
using Codebase.Infrastructure;

namespace Codebase.UI
{
    public class UIRoot : MonoBehaviour, ICoroutineRunner
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}