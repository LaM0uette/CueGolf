using Game.Modules.Scripts.Generics;
using Game.Modules.Scripts.StateMachines;
using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine.States
{
    public class PlayerBaseState : State
    {
        #region Statements

        protected readonly PlayerStateMachine StateMachine;
        
        // Camera Rotation
        private static float _cinemachineTargetYaw;
        private static float _cinemachineTargetPitch;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        #endregion

        #region Functions

        protected void RotateCamera()
        {
            var deltaTimeMultiplier = Time.deltaTime;

            _cinemachineTargetYaw += StateMachine.Inputs.LookValue.x * deltaTimeMultiplier * StateMachine.MouseSensitivity * 10;
            _cinemachineTargetPitch += StateMachine.Inputs.LookValue.y * deltaTimeMultiplier * StateMachine.MouseSensitivity * 10;

            _cinemachineTargetYaw = _cinemachineTargetYaw.ClampAngle(float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = _cinemachineTargetPitch.ClampAngle(StateMachine.BottomClamp, StateMachine.TopClamp);

            StateMachine.CameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
        }

        #endregion
    }
}
