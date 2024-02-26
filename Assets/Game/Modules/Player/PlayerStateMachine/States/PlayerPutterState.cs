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
        private Vector2 _mouseVelocity;
        private float _forceMultiplier = 1f;

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
            Vector3 currentMousePosition = Mouse.current.position.ReadValue();
            _mouseVelocity = (currentMousePosition - _lastMousePosition) / deltaTime;
            _lastMousePosition = currentMousePosition;
            
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
            var forceDirection = (StateMachine.transform.position - StateMachine.PlayerCamera.transform.position).normalized;
            float forceMagnitude = _mouseVelocity.magnitude * _forceMultiplier;
            Vector3 force = forceDirection * forceMagnitude;

            Debug.Log("Force: " + force);
            
            StateMachine.Rigidbody.AddForce(force, ForceMode.Impulse);
            
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        #endregion
    }
}