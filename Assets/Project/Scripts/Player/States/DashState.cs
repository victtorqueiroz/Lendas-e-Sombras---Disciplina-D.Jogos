using UnityEngine;

namespace Player.States
{
    public class DashState : PlayerState
    {
        private float dashDuration = 0.15f;
        private float dashSpeed = 18f;
        private float dashTimer = 0f;
        private Vector2 dashDirection;
        private Rigidbody2D rb;

        public DashState(PlayerController ctrl) : base(ctrl) 
        {
            rb = ctrl.GetComponent<Rigidbody2D>();
        }

        public override void OnEnter()
        {
            controller.SetAnimationBool("isDashing", true);
            controller.SetInvulnerable(true);
            dashTimer = 0f;
            
            dashDirection = controller.GetLastMoveDirection();
            if (dashDirection == Vector2.zero)
                dashDirection = Vector2.zero;
        }

        public override void OnUpdate()
        {
            dashTimer += Time.deltaTime;
            
            if (dashDirection != Vector2.zero)
            {
                rb.linearVelocity = dashDirection * dashSpeed;
            }
            
            if (dashTimer >= dashDuration)
            {
                rb.linearVelocity = Vector2.zero;  // Para o movimento
                controller.TransitionToState(controller.GetIdleState());
            }
        }

        public override void OnExit()
        {
            controller.SetAnimationBool("isDashing", false);
            controller.SetInvulnerable(false);
        }
    }
}