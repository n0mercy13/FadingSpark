using UnityEngine;
using System.Collections;

namespace Codebase.Logic
{
    public class SpriteColorHandler
    {
        private Material _material;

        public void Initialize(Material material) => 
            _material = material;

        public Color CurrentColor
        {
            get => _material.color;
            set => _material.color = value;
        }             

        public IEnumerator ChangeColorOverTime(
            Color initial, Color final, float time)
        {
            float delta = 0f;

            while (delta < time)
            {
                delta += Time.deltaTime;
                CurrentColor = Color.Lerp(initial, final, delta / time);

                yield return null;
            }
        }
    }
}