using UnityEngine;

namespace Game.Modules.Scripts.StateMachines
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public void SwitchState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        private void OnEnable()
        {
            _currentState?.OnEnable();
        }
        
        private void OnDisable()
        {
            _currentState?.OnDisable();
        }

        private void Update()
        {
            _currentState.CheckState();
            _currentState.Tick(Time.deltaTime);
        }
        
        private void LateUpdate()
        {
            _currentState.TickLate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _currentState.TickFixed(Time.deltaTime);
        }
    }
}
