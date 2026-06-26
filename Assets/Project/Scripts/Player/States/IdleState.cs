using UnityEngine;

namespace Player.States
{
    public class IdleState : PlayerState
    {
        public IdleState(PlayerController ctrl) : base(ctrl) { }

        public override void OnEnter()
        {
            controller.SetAnimationBool("isMoving", false);
            controller.SetAnimationBool("isAttacking", false);
            controller.ApplyMovement(Vector2.zero);
        }

        public override void OnUpdate()
        {
            // Transições são gerenciadas pelo PlayerController
        }

        public override void OnExit()
        {
        }
    }
}