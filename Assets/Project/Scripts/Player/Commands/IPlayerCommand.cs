namespace Player.Commands
{
    public interface IPlayerCommand
    {
        void Execute(PlayerController controller);
    }
}