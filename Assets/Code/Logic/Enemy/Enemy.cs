using UnityEngine;
using Codebase.Logic.PlayerComponents;
using Codebase.Logic.Weapons;
using Codebase.Logic.Enemy.StateMachine;
using Zenject;

namespace Codebase.Logic.EnemyComponents
{
    public class Enemy : MonoBehaviour
    {
        protected Player Target;
        protected Weapon Weapon;
        protected Health Health;
        protected EnemyStateMachine StateMachine;

        [Inject]
        public void Construct(Player target)
        {
            Target = target;            
        }

        public void Initialize(Weapon weapon)
        {

        }
    }
}
