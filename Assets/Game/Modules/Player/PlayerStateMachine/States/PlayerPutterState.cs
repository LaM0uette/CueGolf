using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerPutterState : PlayerBaseState
    {
        #region Statements
        
        private Vector3 _lastMousePosition;

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

        #region Functions

        private void OnPutterRelease()
        {
            var mouseDelta = (Vector3)Mouse.current.position.ReadValue() - _lastMousePosition;
            var speed = mouseDelta.magnitude / Time.deltaTime;

            var hitDirection = (StateMachine.transform.position - StateMachine.PlayerCamera.transform.position).normalized;
            var force = hitDirection * (StateMachine.ForceMultiplier);

            Debug.Log($"force: {force}");
            Debug.Log($"speed: {speed}");
            StateMachine.Rigidbody.AddForce(force, ForceMode.Impulse);
            
            StateMachine.SwitchState(new PlayerIdleState(StateMachine));
        }

        #endregion
    }
}