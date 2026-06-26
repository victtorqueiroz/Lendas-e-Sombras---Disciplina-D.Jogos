using UnityEngine;

namespace Player.States
{
    public class MoveState : PlayerState
    {
        private Vector2 currentDirection;

        public MoveState(PlayerController ctrl) : base(ctrl) { }

        public override void OnEnter()
        {
            controller.SetAnimationBool("isMoving", true);
            currentDirection = Vector2.zero;
        }

        public override void OnUpdate()
        {
            // Movimentação é aplicada diretamente em PlayerController.Move()
            // Esta função só gerencia o estado de animação
        }

        public override void OnExit()
        {
            controller.SetAnimationBool("isMoving", false);
        }

        public void SetDirection(Vector2 direction)
        {
            currentDirection = direction;
            if (direction != Vector2.zero)
            {
                controller.SetAnimationFloat("frente", direction.y);
                controller.SetAnimationFloat("lado", direction.x);
            }
        }

        public Vector2 GetDirection() => currentDirection;
    }
}