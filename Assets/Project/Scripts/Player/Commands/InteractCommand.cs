namespace Player.Commands
{
    public class InteractCommand : IPlayerCommand
    {
        public void Execute(PlayerController controller)
        {
            controller.Interact();
        }
    }
}