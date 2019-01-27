using Sjouke.Controls;
using Sjouke.Input;
using UnityEngine;

public class Penguin_Movement : PlayerController
{
    [Header("Everything else")]
    public Mesh standingMesh, glidingMesh, swimmingMesh;

    public float jumpStrength = 200;
    public float bouyancyStrength = 1;
    public float glideSpeed = 0.05f;
    public float swimSpeed = 20;

    public bool isInSwimmingZone;

    float timerForJump;

    Rigidbody rb;
    BoxCollider boxCol;

    Vector3 StandingBoxCol_Center => new Vector3(0, 0.2653827f, 0);
    Vector3 StandingBoxCol_Size => new Vector3(0.3483039f, 0.5323153f, 0.3636921f);
    Vector3 GlidingBoxCol_Center => new Vector3(0.1f, 0.17f, 0);
    Vector3 GlidingBoxCol_Size => new Vector3(0.52f, 0.32f, 0.36f);

    new void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();
    }

    new void Update()
    {
        if (!isInSwimmingZone) NormalMovement();
        else if (isInSwimmingZone) MovementInSwimmingZone();
    }

    public void EnterSwimZone()
    {
        isInSwimmingZone = true;
        SetMeshToGliding();
    }
    public void ExitSwimZone()
    {
        SetMeshToStanding();
        isInSwimmingZone = false;
    }

    void SetMeshToStanding()
    {
        GetComponent<MeshFilter>().mesh = standingMesh;
        boxCol.center = StandingBoxCol_Center;
        boxCol.size = StandingBoxCol_Size;
    }
    void SetMeshToSwimming()
    {
        GetComponent<MeshFilter>().mesh = swimmingMesh;
        boxCol.center = StandingBoxCol_Center;
        boxCol.size = StandingBoxCol_Size;
    }
    void SetMeshToGliding()
    {
        GetComponent<MeshFilter>().mesh = glidingMesh;
        boxCol.center = GlidingBoxCol_Center;
        boxCol.size = GlidingBoxCol_Size;
    }

    void NormalMovement()
    {
        float horizontalInput = MovementInput.Value.x;
        if (timerForJump <= 0)
        {
            if (JumpButtonVariable.Value)
            {
                rb.AddForceAtPosition(Vector3.up * jumpStrength, transform.position, ForceMode.Force);
                timerForJump = 0.75f;
            }
        }
        else timerForJump -= Time.deltaTime;
        if (Mathf.Abs(horizontalInput) < 0.05f) SetMeshToStanding();
        else
        {
            if (horizontalInput < 0)
            {
                SetMeshToGliding();
                transform.eulerAngles = Vector3.up * 180;
                transform.position += Vector3.left * glideSpeed;
            }
            if (horizontalInput > 0)
            {
                SetMeshToGliding();
                transform.eulerAngles = Vector3.zero;
                transform.position += Vector3.right * glideSpeed;
            }
            else if (Mathf.Abs(horizontalInput) < 0.05f) SetMeshToStanding();
        }
    }

    void MovementInSwimmingZone()
    {
        var horizontal = MovementInput.Value.x;
        var vertical = MovementInput.Value.y;

        SetMeshToSwimming();
        //if (Input.GetKey(KeyCode.W)) rb.AddForceAtPosition(transform.up * swimSpeed, transform.position, ForceMode.Force);
        if (vertical > 0) rb.AddForceAtPosition(transform.up * swimSpeed, transform.position, ForceMode.Force);
        //if (Input.GetKey(KeyCode.S)) rb.AddForceAtPosition(-(transform.up) * (swimSpeed / 3), transform.position, ForceMode.Force);
        if (vertical < 0) rb.AddForceAtPosition(-(transform.up) * (swimSpeed / 3), transform.position, ForceMode.Force);
        //if (Input.GetKey(KeyCode.A)) transform.eulerAngles += (Vector3.forward * 180) * Time.deltaTime;
        if (horizontal > 0) transform.eulerAngles += (Vector3.forward * 180) * Time.deltaTime;
        //if (Input.GetKey(KeyCode.D)) transform.eulerAngles += (-Vector3.forward * 180) * Time.deltaTime;
        if (horizontal < 0) transform.eulerAngles += (-Vector3.forward * 180) * Time.deltaTime;
        rb.AddForceAtPosition(Vector3.up * bouyancyStrength, transform.position, ForceMode.Force);
    }
}