using Game.Modules.Scripts.StateMachines;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerBaseState : State
    {
        #region Statements

        protected readonly PlayerStateMachine StateMachine;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        #endregion
    }
}
