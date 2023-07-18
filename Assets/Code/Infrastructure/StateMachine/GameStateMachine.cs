using System;
using System.Collections.Generic;

namespace Codebase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public GameStateMachine()
        {

        }
    }
}
