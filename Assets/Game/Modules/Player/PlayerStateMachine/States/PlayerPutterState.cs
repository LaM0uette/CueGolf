namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerPutterState : PlayerBaseState
    {
        #region Statements

        public PlayerPutterState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        #endregion
        
        #region Events
        
        public override void Enter()
        {
            StateMachine.Soap.PutterReleaseEvent.OnRaised += OnPutterRelease;
        }
        
        public override void Exit()
        {
            StateMachine.Soap.PutterReleaseEvent.OnRaised -= OnPutterRelease;
        }

        #endregion

        #region Functions

        private void OnPutterRelease()
        {
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        #endregion
    }
}