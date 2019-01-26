namespace Sjouke.Controls
{
    using UnityEngine;
    using CodeArchitecture.Variables;

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Inputs (REQUIRED)")]
        public CharacterSettings CharacterSettings;
        public InputVariable MovementInput;
        public BoolVariable SpacebarVariable;

        protected Rigidbody _rb;
        protected RaycastHit[] _raycastBuffer = new RaycastHit[5];
        private bool _isJumping = false;
        private float _currentJumpVelocity;

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            _rb = GetComponent<Rigidbody>();
        }
#endif

        protected virtual void Start()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            Vector3 newFrameVelocity = Vector3.zero;

            if (Physics.RaycastNonAlloc(transform.position + CharacterSettings.JumpCheckOffset, -transform.up, _raycastBuffer, CharacterSettings.JumpCheckDistance) > 0)
            {
                if (SpacebarVariable.Value)
                {
                    _isJumping = true;
                    _currentJumpVelocity = CharacterSettings.JumpForce;
                }
                else
                {
                    _isJumping = false;
                    _currentJumpVelocity = 0;
                }
            }

            if (_isJumping)
                _currentJumpVelocity -= CharacterSettings.JumpFallSubstraction;

            float forwardMotion = Mathf.Abs(MovementInput.Value.x) > 0.05 ? MovementInput.Value.x : 0;
            newFrameVelocity = new Vector3(forwardMotion * CharacterSettings.MovementSpeed, _currentJumpVelocity, 0);

            if (Mathf.Abs(MovementInput.Value.x) > 0.05)
            {
                var currentRotation = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0, forwardMotion > 0 ? 0 : 180, currentRotation.z);
            }

            _rb.MovePosition(_rb.position + newFrameVelocity * Time.deltaTime);
        }
    }
}