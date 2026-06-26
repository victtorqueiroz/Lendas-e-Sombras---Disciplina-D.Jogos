namespace Player.Commands
{
    public class SpecialCommand : IPlayerCommand
    {
        public void Execute(PlayerController controller)
        {
            controller.Special();
        }
    }
}