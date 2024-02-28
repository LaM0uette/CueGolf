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
        }

        #endregion

        #region Functions

        private void OnPutterRelease()
        {
            if (_putterPosition < StateMachine.ShootDistanceOffset)
            {
                StateMachine.SwitchState(new PlayerIdleState(StateMachine));
                return;
            }
            
            Shoot();
        }

        private void Shoot()
        {
            var forceDirection = (StateMachine.transform.position - StateMachine.PlayerCamera.transform.position).normalized;
            
            if (forceDirection.y >= -0.2f)
                forceDirection.y = 0;
            
            var force = forceDirection * (_putterPosition * StateMachine.ForceMultiplier);

            StateMachine.Rigidbody.AddForce(force, ForceMode.Impulse);
            
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        #endregion
    }
}