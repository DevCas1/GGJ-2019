namespace Sjouke.Controls
{
    using UnityEngine;
    using CodeArchitecture.Variables;
    
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PlayerController : MonoBehaviour 
    {
        [Header("Inputs (REQUIRED)")]
        public InputVariable MovementInput;
        public BoolVariable JumpButtonVariable;

        [Space(10), Header("General")]
        public float MovementSpeed;

        [Space(10), Header("Jumping")]
        public float JumpForce;
        public float JumpFallSubstraction;
        public float JumpCheckDistance;
        public Vector3 JumpCheckOffset;

        protected Rigidbody _rb;
        protected Vector3 _currentVelocity;
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
            PerformMovement();
        }

        protected virtual void PerformMovement()
        {
            _currentVelocity = Vector3.zero;

            if (Physics.RaycastNonAlloc(transform.position + JumpCheckOffset, -transform.up, _raycastBuffer, JumpCheckDistance) > 0)
            {
                if (JumpButtonVariable.Value)
                {
                    _isJumping = true;
                    _currentJumpVelocity = JumpForce;
                }
                else
                {
                    _isJumping = false;
                    _currentJumpVelocity = 0;
                }
            }

            if (_isJumping)
                _currentJumpVelocity -= JumpFallSubstraction;

            float forwardMotion = Mathf.Abs(MovementInput.Value.x) > 0.05 ? MovementInput.Value.x : 0;
            _currentVelocity = new Vector3(forwardMotion * MovementSpeed, _currentJumpVelocity, 0);

            if (Mathf.Abs(MovementInput.Value.x) > 0.05)
            {
                var currentRotation = _rb.rotation.eulerAngles;
                _rb.rotation = Quaternion.Euler(0, forwardMotion > 0 ? 0 : 180, currentRotation.z);
            }

            _rb.MovePosition(_rb.position + _currentVelocity * Time.deltaTime);
        }
    }
}