using Game.Modules.Player.Inputs;
using Game.Modules.Player.PlayerStateMachine.States;
using Game.Modules.Scripts.StateMachines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine
{
    [RequireComponent(typeof(InputsReader))]
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerStateMachine : StateMachine
    {
        #region Statements
        
        public InputsReader Inputs { get; private set; }
        
        [Title("Cinemachine")]
        [Range(0f, 100f)] public float MouseSensitivity = 30f;
        [Range(0f, 180f)] public float TopClamp = 90.0f;
        [Range(0f, -180f)] public float BottomClamp;
        public GameObject CameraTarget;

        private void Awake()
        {
            Inputs = GetComponent<InputsReader>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SwitchState(new PlayerIdleState(this));
        }

        #endregion
    }
}

 