namespace Sjouke.Controls
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public sealed class CharacterController : MonoBehaviour
    {
        private Rigidbody _rb;

#if UNITY_EDITOR
        private void Reset()
        {
            _rb = GetComponent<Rigidbody>();
        }
#endif

        private void Start()
        {
            if (_rb == null) 
                _rb = GetComponent<Rigidbody>();
        }
    }
}