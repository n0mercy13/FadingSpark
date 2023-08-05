using Codebase.UI.Elements;
using Codebase.UI.Manager;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameOverState : IState
    {
        private readonly IUIManager _uiManager;

        private UI_GameOverScreen_Button_Restart _buttonRestart;

        public void Enter()
        {
            _uiManager.GetGameOverScreen();
        }

        public void Exit()
        {
        }
    }
}