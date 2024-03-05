using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        #region Statements
        
        private float _minimumTimeElapsed;

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        #endregion

        #region Events
        
        public override void OnDisable()
        {
            StateMachine.Soap.ZoomEvent -= OnZoom;
        }
        
        public override void Enter()
        {
            StateMachine.Soap.ZoomEvent += OnZoom;
            
            _minimumTimeElapsed = 0f;
        }
        
        public override void Exit()
        {
            StateMachine.Soap.ZoomEvent -= OnZoom;
        }
        
        public override void CheckState()
        {
            if (!IsMoving() && _minimumTimeElapsed > 2f)
            {
                StateMachine.SwitchState(new PlayerIdleState(StateMachine));
            }
        }
        
        public override void Tick(float deltaTime)
        {
            _minimumTimeElapsed += deltaTime;

            CheckForStopMovement();
            RotateCamera();
        }

        #endregion

        #region Functions
        
        private void CheckForStopMovement()
        {
            var magnitude = StateMachine.Rigidbody.velocity.magnitude + StateMachine.Rigidbody.angularVelocity.magnitude;
            
            if (_minimumTimeElapsed < 2f || magnitude > StateMachine.StopMovementThreshold )
                return;
            
            StateMachine.Rigidbody.velocity = Vector3.zero;
            StateMachine.Rigidbody.angularVelocity = Vector3.zero;
        }

        #endregion
    }
}