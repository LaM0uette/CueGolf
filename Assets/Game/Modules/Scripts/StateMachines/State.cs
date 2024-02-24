namespace Game.Modules.Scripts.StateMachines
{
    public abstract class State
    {
        public virtual void OnEnable() {}
        public virtual void OnDisable() {}
        
        public virtual void Enter() {}
        public virtual void Exit() {}
        
        public virtual void CheckState() {}
        
        public virtual void Tick(float deltaTime) {}
        public virtual void TickLate(float deltaTime) {}
        public virtual void TickFixed(float deltaTime) {}
    }
}
