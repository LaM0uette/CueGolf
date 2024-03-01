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
        private static Vector2 _cinemachineTarget;
        
        protected PlayerBaseState(PlayerStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        #endregion

        #region Functions

        protected void RotateCamera()
        {
            var deltaTimeMultiplier = Time.deltaTime;
            
            _cinemachineTarget.x += StateMachine.Inputs.LookValue.x * deltaTimeMultiplier * StateMachine.MouseSensitivity * 10;
            _cinemachineTarget.y += StateMachine.Inputs.LookValue.y * deltaTimeMultiplier * StateMachine.MouseSensitivity * 10;

            _cinemachineTarget.x = _cinemachineTarget.x.ClampAngle(float.MinValue, float.MaxValue);
            _cinemachineTarget.y = _cinemachineTarget.y.ClampAngle(StateMachine.BottomClamp, StateMachine.TopClamp);

            StateMachine.CameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTarget.y, _cinemachineTarget.x, 0.0f);
        }
        
        protected void OnZoom(float zoomValue)
        {
            StateMachine.FollowCamera.CameraDistance += zoomValue * StateMachine.ZoomForce * Time.deltaTime;
            if (StateMachine.FollowCamera.CameraDistance <= StateMachine.MinZoom) StateMachine.FollowCamera.CameraDistance = StateMachine.MinZoom;
            if (StateMachine.FollowCamera.CameraDistance >= StateMachine.MaxZoom) StateMachine.FollowCamera.CameraDistance = StateMachine.MaxZoom;
        }

        #endregion
    }
}
