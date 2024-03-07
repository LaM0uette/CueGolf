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
            var direction = (StateMachine.transform.position - StateMachine.PlayerCamera.transform.position).normalized;
            var force =  _putterPosition * StateMachine.ForceMultiplier;
            
            if (direction.y >= -0.3f)
                direction.y = 0;
            
            direction.y = 0;
            
            // la rotation de la balle au moment du tir n'est pas bonne du tout, ça peut être la cause de bug mais surtout visuelement c'est degeu
            //StateMachine.transform.LookAt(direction);
            
            StateMachine.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
            var spinAmount = CalculateSpin(direction, force);
            StateMachine.Rigidbody.AddTorque(spinAmount, ForceMode.Impulse);
            
            StateMachine.SwitchState(new PlayerMoveState(StateMachine));
        }
        
        Vector3 CalculateSpin(Vector3 direction, float force)
        {
            return new Vector3(direction.z, 0, -direction.x) * force * 0.1f;
        }

        #endregion
    }
}