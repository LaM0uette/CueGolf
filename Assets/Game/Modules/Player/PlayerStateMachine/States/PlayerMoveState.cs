using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        #region Statements
        
        private float _ballVelocity;
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
            _ballVelocity = StateMachine.Rigidbody.velocity.magnitude;
            _minimumTimeElapsed += deltaTime;

            CheckForStopMovement();
            RotateCamera();
        }

        #endregion

        #region Functions
        
        private void CheckForStopMovement()
        {
            if (!(_ballVelocity <= StateMachine.StopMovementThreshold)) 
                return;
            
            StateMachine.Rigidbody.velocity = Vector3.zero;
            StateMachine.Rigidbody.angularVelocity = Vector3.zero;
        }
        
        private bool IsMoving()
        {
            return StateMachine.Rigidbody.velocity.magnitude != 0 || StateMachine.Rigidbody.angularVelocity.magnitude != 0;
        }

        #endregion
    }
}