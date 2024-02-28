using Game.Modules.Player.Inputs;
using Game.Modules.Player.PlayerStateMachine.States;
using Game.Modules.Scripts.StateMachines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules.Player.PlayerStateMachine
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerSoap))]
    [RequireComponent(typeof(InputsReader))]
    public class PlayerStateMachine : StateMachine
    {
        #region Statements

        public Rigidbody Rigidbody { get; private set; }
        public PlayerSoap Soap { get; private set; }
        public InputsReader Inputs { get; private set; }
        public Camera PlayerCamera { get; private set; }
        
        [Space, Title("Player Settings")]
        public float ForceMultiplier = 100f;
        public float ShootDistanceOffset = 0.002f;
        
        [Space, Title("Cinemachine")]
        public float MouseSensitivity = 45f;
        [Range(0f, 180f)] public float TopClamp = 90.0f;
        [Range(0f, -180f)] public float BottomClamp;
        public GameObject CameraTarget;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Soap = GetComponent<PlayerSoap>();
            Inputs = GetComponent<InputsReader>();
            
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

 