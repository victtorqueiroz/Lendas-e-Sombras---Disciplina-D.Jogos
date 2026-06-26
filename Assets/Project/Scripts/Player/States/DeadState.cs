using UnityEngine;

namespace Player.States
{
    public class DeadState : PlayerState
    {
        private float deathDelay = 1f;
        private float deathTimer = 0f;

        public DeadState(PlayerController ctrl) : base(ctrl) { }

        public override void OnEnter()
        {
            controller.SetAnimationBool("isDead", true);
            controller.PlayDeathAnimation();
            deathTimer = 0f;
        }

        public override void OnUpdate()
        {
            deathTimer += Time.deltaTime;
            if (deathTimer >= deathDelay)
            {
                // controller.RespawnAtLobby();
            }
        }

        public override void OnExit()
        {
            controller.SetAnimationBool("isDead", false);
        }
    }
}