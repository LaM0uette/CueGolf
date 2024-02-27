using Game.Modules.Player.Inputs;
using Game.Modules.Player.PlayerStateMachine.States;
using Game.Modules.Scripts.StateMachines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine
{
    [RequireComponent(typeof(PlayerSoap))]
    [RequireComponent(typeof(InputsReader))]
    public class PlayerStateMachine : StateMachine
    {
        #region Statements

        public InputsReader Inputs { get; private set; }
        public PlayerSoap Soap { get; private set; }
        public Camera PlayerCamera { get; private set; }
        
        [Space, Title("Player Settings")]
        public Rigidbody Rigidbody;
        public float ForceMultiplier = 100f;
        public float CuePosOffset = -0.55f;
        public Transform PlayerBall;
        public Cue Cue;
        
        [Space, Title("Cinemachine")]
        public float MouseSensitivity = 45f;
        [Range(0f, 180f)] public float TopClamp = 90.0f;
        [Range(0f, -180f)] public float BottomClamp;
        public GameObject CameraTarget;

        private void Awake()
        {
            Inputs = GetComponent<InputsReader>();
            Soap = GetComponent<PlayerSoap>();
            
            PlayerCamera = Camera.main;
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

 