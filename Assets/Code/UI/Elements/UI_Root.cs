using Codebase.Infrastructure;

namespace Codebase.UI.Elements
{
    public class UI_Root : HideableUI, ICoroutineRunner
    {
        private void Awake() => 
            DontDestroyOnLoad(this);
    }
}