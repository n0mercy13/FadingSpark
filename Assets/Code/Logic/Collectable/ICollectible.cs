using UnityEngine;

namespace Codebase.Logic.Collectables
{
    public interface ICollectible
    {
        int Collect();
        void MoveTowards(Transform target, float speed);
    }
}