using Unity.Collections;
using UnityEngine;

namespace Game.Modules.Player
{
    public class GhostCollisionCatcher : MonoBehaviour {
 
        private void OnEnable() 
        {
            Physics.ContactModifyEvent += ModificationEventDCD;
            Physics.ContactModifyEventCCD += ModificationEventCCD;
            
            GetComponent<Collider>().hasModifiableContacts = true;
        }
 
        private void OnDisable() 
        {
            Physics.ContactModifyEvent -= ModificationEventDCD;
            Physics.ContactModifyEventCCD -= ModificationEventCCD;
        }
        
        private static void ModificationEventDCD( PhysicsScene scene, NativeArray<ModifiableContactPair> pairs ) 
        {
            ModificationEvent( pairs );
        }
        
        private static void ModificationEventCCD( PhysicsScene scene, NativeArray<ModifiableContactPair> pairs ) 
        {
            ModificationEvent( pairs );
        }
        
        private static void ModificationEvent( NativeArray<ModifiableContactPair> pairs ) 
        {
            foreach ( var pair in pairs ) 
            {
                for ( var i = 0; i < pair.contactCount; ++i ) 
                {
                    if (pair.GetSeparation(i) > 0)
                        pair.IgnoreContact(i);
                }
            }
        }
    }
}