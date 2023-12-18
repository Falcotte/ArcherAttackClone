using UnityEngine;
using UnityEngine.Events;

namespace ArcherAttack.Archer
{
    public class ArcherController : MonoBehaviour
    {
        [SerializeField] private ArcherMovementController _movementController;
        public ArcherMovementController MovementController => _movementController;

        [SerializeField] private ArcherAnimationController _animationController;
        public ArcherAnimationController AnimationController => _animationController;

        public static UnityAction OnAimed;
    }
}