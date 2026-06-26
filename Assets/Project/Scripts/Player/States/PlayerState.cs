namespace Player.States
{
    public abstract class PlayerState : IPlayerState
    {
        protected PlayerController controller;

        public PlayerState(PlayerController ctrl)
        {
            controller = ctrl;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}