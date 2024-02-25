using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerPutterState : PlayerBaseState
    {
        #region Statements
        
        private Vector3 _lastMousePosition;
        private float _putterY;
        private bool _putterIsReady;

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
            
            _lastMousePosition = Mouse.current.position.ReadValue();
        }
        
        public override void Exit()
        {
            StateMachine.Soap.PutterReleaseEvent.OnRaised -= OnPutterRelease;
        }

        #endregion

        #region Events
        
        public override void Tick(float deltaTime)
        {
            _putterY += StateMachine.Inputs.LookValue.y * deltaTime;
            
            if (_putterY > 0.01f)
            {
                _putterIsReady = true;
            }
            
            if (_putterY <= 0 && _putterIsReady)
            {
                _putterIsReady = false;
                Shoot();
            }
        }

        #endregion

        #region Functions

        private void OnPutterRelease()
        {
            _putterIsReady = false;
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        private void Shoot()
        {
            var mouseDelta = (Vector3)Mouse.current.position.ReadValue() - _lastMousePosition;
            var speed = mouseDelta.magnitude / Time.deltaTime;
            speed /= 100f;

            var hitDirection = (StateMachine.transform.position - StateMachine.PlayerCamera.transform.position).normalized;
            var force = hitDirection * (speed * StateMachine.ForceMultiplier);

            StateMachine.Rigidbody.AddForce(force, ForceMode.Impulse);
            
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        #endregion
    }
}