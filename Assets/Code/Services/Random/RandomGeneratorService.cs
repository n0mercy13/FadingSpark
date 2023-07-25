using UnityEngine;

namespace Codebase.Services.RandomGenerator
{
    public class RandomGeneratorService : IRandomGeneratorService
    {
        public Vector2 GetPositionOutsideViewport()
        {
            Vector2 screenCenter = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
            Vector2 maxScreenBoundary = Camera.main.ViewportToWorldPoint(Vector2.one);
            float radius = (maxScreenBoundary - screenCenter).magnitude;

            float randomAngle = Random.Range(0f, 2 * Mathf.PI - float.Epsilon);
            Vector2 randomPointOnCircle = new Vector2(
                Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * radius;

            return randomPointOnCircle;
        }
    }
}