using UnityEngine;

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
        
        public override void OnDisable()
        {
            StateMachine.Soap.ZoomEvent -= OnZoom;
            StateMachine.Soap.PutterPressEvent.OnRaised -= OnPutterPress;
        }
        
        public override void Enter()
        {
            StateMachine.Soap.ZoomEvent += OnZoom;
            StateMachine.Soap.PutterPressEvent.OnRaised += OnPutterPress;
        }
        
        public override void Exit()
        {
            StateMachine.Soap.ZoomEvent -= OnZoom;
            StateMachine.Soap.PutterPressEvent.OnRaised -= OnPutterPress;
        }

        public override void CheckState()
        {
            if (IsMoving())
            {
                StateMachine.SwitchState(new PlayerMoveState(StateMachine));
            }
        }
        
        public override void Tick(float deltaTime)
        {
            RotateCamera();
        }

        #endregion

        #region Functions

        private void OnPutterPress()
        {
            StateMachine.SwitchState(new PlayerPutterState(StateMachine));
        }

        #endregion
    }
}