namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        #region Statements

        public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        #endregion

        #region Events

        public override void Tick(float deltaTime)
        {
            RotateCamera();
        }

        #endregion
    }
}