using Cinemachine;
using Game.Modules.Player.Inputs;
using Game.Modules.Player.PlayerStateMachine.States;
using Game.Modules.Scripts.Generics;
using Game.Modules.Scripts.StateMachines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Modules.Player.PlayerStateMachine
{
    [RequireComponent(typeof(PlayerInput))]
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
        public Cinemachine3rdPersonFollow FollowCamera { get; private set; }
        
        [Space, Title("Player Settings")]
        public float ForceMultiplier = 100f;
        public float ReboundForce = 0.8f;
        public float ShootDistanceOffset = 0.002f;
        public float StopMovementThreshold = 0.05f;
        
        [Space, Title("Cinemachine")]
        [SerializeField] private CinemachineVirtualCamera _virtualFollowCamera;
        public float MouseSensitivity = 45f;
        public float ZoomForce = 10f;
        public float MinZoom = 1f;
        public float MaxZoom = 8f;
        [Range(0f, 180f)] public float TopClamp = 90.0f;
        [Range(0f, -180f)] public float BottomClamp;
        public GameObject CameraTarget;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Soap = GetComponent<PlayerSoap>();
            Inputs = GetComponent<InputsReader>();
            
            PlayerCamera = Camera.main;
            FollowCamera = _virtualFollowCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        }

        private void Start()
        {
            CursorHandler.HideCursor();

            SwitchState(new PlayerIdleState(this));
        }

        #endregion

        #region Events

        private void OnCollisionEnter(Collision collision)
        {
            var ballVelocity = Rigidbody.velocity;
            var incomingVector = new Vector3(ballVelocity.x, 0, ballVelocity.z);
            Rigidbody.velocity = incomingVector;
            Rigidbody.angularVelocity = Vector3.zero;
            
            if (collision.gameObject.CompareTag("Floor"))
                return;
            
            var normal = collision.contacts[0].normal;
            var reflectVector = Vector3.Reflect(incomingVector, normal);

            reflectVector *= ReboundForce;
            Rigidbody.velocity = reflectVector;
        }

        #endregion
    }
}

 