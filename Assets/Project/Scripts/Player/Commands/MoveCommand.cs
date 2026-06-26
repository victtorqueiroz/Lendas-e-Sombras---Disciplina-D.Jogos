using UnityEngine;

namespace Player.Commands
{
    public class MoveCommand : IPlayerCommand
    {
        private Vector2 direction;

        public MoveCommand(Vector2 dir)
        {
            direction = dir;
        }

        public void Execute(PlayerController controller)
        {
            controller.Move(direction);
        }
    }
}