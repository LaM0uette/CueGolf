using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerPutterState : PlayerBaseState
    {
        #region Statements
        
        private float _putterPosition;

        public PlayerPutterState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        #endregion
        
        #region Events
        
        public override void OnDisable()
        {
            StateMachine.Soap.PutterReleaseEvent.OnRaised -= OnPutterRelease;
        }
        
        public override void Enter()
        {
            StateMachine.Soap.PutterReleaseEvent.OnRaised += OnPutterRelease;
            
            StateMachine.Cue.SetLocalPosition(new Vector3(0, 0, StateMachine.CuePosOffset));
        }
        
        public override void Exit()
        {
            StateMachine.Soap.PutterReleaseEvent.OnRaised -= OnPutterRelease;
        }

        #endregion

        #region Events
        
        public override void Tick(float deltaTime)
        {
            _putterPosition += StateMachine.Inputs.LookValue.y * deltaTime;
            StateMachine.Cue.SetLocalPosition(new Vector3(0, 0.05f, -_putterPosition + StateMachine.CuePosOffset));
        }

        #endregion

        #region Functions

        private void OnPutterRelease()
        {
            if (_putterPosition < 0.1f)
            {
                StateMachine.SwitchState(new PlayerIdleState(StateMachine));
                return;
            }
            
            Shoot();
        }

        private void Shoot()
        {
            var forceDirection = (StateMachine.PlayerBall.position - StateMachine.PlayerCamera.transform.position).normalized;
            //var forceDirection = (StateMachine.PlayerBall.position - StateMachine.Cue.transform.position).normalized;
            var force = forceDirection * _putterPosition * 10f;

            StateMachine.Rigidbody.AddForce(force, ForceMode.Impulse);
            
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        #endregion
    }
}