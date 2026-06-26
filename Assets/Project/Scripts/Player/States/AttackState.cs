using UnityEngine;

namespace Player.States
{
    public class AttackState : PlayerState
    {
        public AttackState(PlayerController ctrl) : base(ctrl) { }

        public override void OnEnter()
        {
            controller.SetAnimationBool("isAttacking", true);
            ExecuteQuickAttack();
            controller.TransitionToState(controller.GetIdleState());
        }

        public override void OnUpdate() { }

        public override void OnExit()
        {
            controller.SetAnimationBool("isAttacking", false);
        }

        private void ExecuteQuickAttack()
        {
            Vector2 lastDir = controller.GetLastMoveDirection();
            
            if (lastDir.x < 0)
                controller.PlayAttackAnimation("attack_quick_left");
            else
                controller.PlayAttackAnimation("attack_quick_right");
            
            controller.DealDamage(10f);
        }
    }
}