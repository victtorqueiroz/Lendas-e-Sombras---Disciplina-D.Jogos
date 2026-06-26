namespace Player.Commands
{
    public class DashCommand : IPlayerCommand
    {
        public void Execute(PlayerController controller)
        {
            controller.Dash();
        }
    }
}