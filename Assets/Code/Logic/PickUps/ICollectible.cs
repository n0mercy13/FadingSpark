using UnityEngine;

namespace Codebase.Logic.PickUps
{
    public interface ICollectible
    {
        int Collect();
        void MoveTowards(Transform target, float speed);
    }
}