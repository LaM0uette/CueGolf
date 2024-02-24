using Game.Modules.Player.PlayerStateMachine.States;
using Game.Modules.Scripts.StateMachines;

namespace Game.Modules.Player.PlayerStateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        #region Statements

        private void Start()
        {
            // Cursor.lockState = CursorLockMode.Locked;
            // Cursor.visible = false;

            SwitchState(new PlayerIdleState(this));
        }

        #endregion
    }
}

 