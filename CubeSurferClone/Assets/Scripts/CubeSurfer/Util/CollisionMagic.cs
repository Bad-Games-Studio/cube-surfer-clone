using UnityEngine;

namespace CubeSurfer.Util
{
    public static class CollisionMagic
    {
        /// <summary>
        /// Checks if a cube collides with its sides (left, right, front, or back).
        /// Assumes that the first object is a cube.
        /// </summary>
        /// <param name="cube">An object in which the `OnCollisionEnter` or other method was invoked.</param>
        /// <param name="collision">A collision object from `OnCollisionEnter` or other methods.</param>
        /// <returns>`true` if a cube collides with left, right, front, or back side; `false` otherwise.</returns>
        public static bool CollidesWithSides(Transform cube, Collision collision)
        {
            var normal = -collision.GetContact(0).normal;

            var forward = cube.forward;
            var right = cube.right;

            return (normal == forward) || (normal == -forward) || (normal == right) || (normal == -right);
        }

        /// <summary>
        /// Checks if a cube collides with its sides (top or bottom).
        /// Assumes that the first object is a cube.
        /// </summary>
        /// <param name="cube">An object in which the `OnCollisionEnter` or other method was invoked.</param>
        /// <param name="collision">A collision object from `OnCollisionEnter` or other methods.</param>
        /// <returns>`true` if a cube collides with top or bottom side; `false` otherwise.</returns>
        public static bool CollidesWithTopBottomSides(Transform cube, Collision collision)
        {
            var normal = -collision.GetContact(0).normal;

            var up = cube.up;
            return (normal == up) || (normal == -up);
        }
    }
}