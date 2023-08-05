using System.Collections.Generic;

namespace Codebase.Services.Initialize
{
    public class InitializationService : IInitializationService
    {
        private readonly List<IInitializable> _initializables = new();

        public void InitializeAll()
        {
            foreach (IInitializable initializable in _initializables)
                initializable.Initialize();
        }

        public void Register(IInitializable initializable)
        {
            _initializables.Add(initializable);
        }
    }
}