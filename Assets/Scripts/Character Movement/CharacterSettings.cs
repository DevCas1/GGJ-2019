namespace Sjouke.Controls
{
    using UnityEngine;

    [CreateAssetMenu]
    public class CharacterSettings : ScriptableObject
    {
        [Header("General")]
        public float MovementSpeed;
        [Space(10), Header("Jumping")]
        public float JumpForce;

        public float JumpFallSubstraction;
        public float JumpCheckDistance;
        public Vector3 JumpCheckOffset;
    }
}