using UnityEngine;

public class VehicleMovement : MonoBehaviour {

    [Header("Drive Settings")]
    //The force that the engine generates
    [SerializeField] private float _driveForce = 17f;
    public float DriveForce {
        get { return _driveForce; }
        set { _driveForce = value; }
    }
    //The percentage of velocity the ship maintains when not thrusting (e.g., a value of .99 means the ship loses 1% velocity when not thrusting)
    [SerializeField] private float _slowingVelFactor = .99f;
    public float SlowingVelFactor {
        get { return _slowingVelFactor; }
        set { _slowingVelFactor = value; }
    }
    //The percentage of velocty the ship maintains when braking
    [SerializeField] private float _brakingVelFactor = .95f;
    public float BrakingVelFactor {
        get { return _brakingVelFactor; }
        set { _brakingVelFactor = value; }
    }
    //The angle that the ship "banks" into a turn
    [SerializeField] private float _angleOfRoll = 30f;
    public float AngleOfRoll {
        get { return _angleOfRoll; }
        set { _angleOfRoll = value; }
    }
    [SerializeField] private float _angleOfWheelsRoll = 60f;
    public float AngleOfWheelsRoll {
        get { return _angleOfWheelsRoll; }
        set { _angleOfRoll = value; }
    }
    [SerializeField] private float _turningSpeed = 10.0f;
    public float TurningSpeed {
        get { return _turningSpeed; }
        set { _turningSpeed = value; }
    }
    public float Speed { get; set; }

    [Header("Hover Settings")]
    //The height the ship maintains when hovering
    [SerializeField] private float _hoverHeight = 1.5f;
    public float HoverHeight {
        get { return _hoverHeight; }
        set { _hoverHeight = value; }
    }
    //The distance the ship can be above the ground before it is "falling"
    [SerializeField] private float _maxGroundDist = 5f;
    public float MaxGroundDist {
        get { return _maxGroundDist; }
        set { _maxGroundDist = value; }
    }
    //The force of the ship's hovering
    [SerializeField] private float _hoverForce = 300f;
    public float HoverForce {
        get { return _hoverForce; }
        set { _hoverForce = value; }
    }
    //A layer mask to determine what layer the ground is on
    [SerializeField] private LayerMask _whatIsGround;
    public LayerMask WhatIsGround {
        get { return _whatIsGround; }
        set { _whatIsGround = value; }
    }
    //A PID controller to smooth the ship's hovering
    [SerializeField] private PIDController _hoverPID;
    public PIDController HoverPID {
        get { return _hoverPID; }
        set { _hoverPID = value; }
    }

    [Header("Physics Settings")]
    //A reference to the ship's body, this is for cosmetics
    [SerializeField] private Transform _shipBody;
    public Transform ShipBody {
        get { return _shipBody; }
        set { _shipBody = value; }
    }
    [SerializeField] private Transform _wheelsFront;
    public Transform WheelsFront {
        get { return _wheelsFront; }
        set { _wheelsFront = value; }
    }
    [SerializeField] private Transform _wheelsBack;
    public Transform WheelsBack {
        get { return _wheelsBack; }
        set { _wheelsBack = value; }
    }
    //The max speed the ship can go
    [SerializeField] private float _terminalVelocity = 100f;
    public float TerminalVelocity {
        get { return _terminalVelocity; }
        set { _terminalVelocity = value; }
    }
    //The gravity applied to the ship while it is on the ground
    [SerializeField] private float _hoverGravity = 20f;
    public float HoverGravity {
        get { return _hoverGravity; }
        set { _hoverGravity = value; }
    }
    //The gravity applied to the ship while it is falling
    [SerializeField] private float _fallGravity = 80f;
    public float FallGravity {
        get { return _fallGravity; }
        set { _fallGravity = value; }
    }

    private Rigidbody rigidBody; //A reference to the ship's rigidbody
    private PlayerInput input; //A reference to the player's input					
    private float drag; //The air resistance the ship recieves in the forward direction
    private bool isOnGround; //A flag determining if the ship is currently on the ground
    private AudioSource audioSource;


    private void Start() {
        //Get references to the Rigidbody and PlayerInput components
        rigidBody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        audioSource = GetComponent<AudioSource>();

        //Calculate the ship's drag value
        drag = DriveForce / TerminalVelocity;
    }


    private void FixedUpdate() {
        //Calculate the current speed by using the dot product. This tells us
        //how much of the ship's velocity is in the "forward" direction 
        Speed = Vector3.Dot(rigidBody.velocity, transform.forward);
        float pitch = Mathf.Lerp(1.0f, 2.5f, Speed / TerminalVelocity);
        audioSource.pitch = pitch;

        //Calculate the forces to be applied to the ship
        CalculateHover();
        CalculatePropulsion();
    }


    private void CalculateHover() {
        //This variable will hold the "normal" of the ground. Think of it as a line
        //the points "up" from the surface of the ground
        Vector3 groundNormal;

        //Calculate a ray that points straight down from the ship
        Ray ray = new Ray(transform.position, -transform.up);

        //Declare a variable that will hold the result of a raycast
        RaycastHit hitInfo;

        //Determine if the ship is on the ground by Raycasting down and seeing if it hits 
        //any collider on the whatIsGround layer
        isOnGround = Physics.Raycast(ray, out hitInfo, MaxGroundDist, WhatIsGround);

        //If the ship is on the ground...
        if (isOnGround) {
            //...determine how high off the ground it is...
            float height = hitInfo.distance;
            //...save the normal of the ground...
            groundNormal = hitInfo.normal.normalized;
            //...use the PID controller to determine the amount of hover force needed...
            float forcePercent = HoverPID.Seek(HoverHeight, height);

            //...calulcate the total amount of hover force based on normal (or "up") of the ground...
            Vector3 force = groundNormal * HoverForce * forcePercent;
            //...calculate the force and direction of gravity to adhere the ship to the 
            //track (which is not always straight down in the world)...
            Vector3 gravity = -groundNormal * HoverGravity * height;

            //...and finally apply the hover and gravity forces
            rigidBody.AddForce(force, ForceMode.Acceleration);
            rigidBody.AddForce(gravity, ForceMode.Acceleration);
        } else {
            //...use Up to represent the "ground normal". This will cause our ship to
            //self-right itself in a case where it flips over
            groundNormal = Vector3.up;

            //Calculate and apply the stronger falling gravity straight down on the ship
            Vector3 gravity = -groundNormal * FallGravity;
            rigidBody.AddForce(gravity, ForceMode.Acceleration);
        }

        //Calculate the amount of pitch and roll the ship needs to match its orientation
        //with that of the ground. This is done by creating a projection and then calculating
        //the rotation needed to face that projection
        Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
        Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);

        //Move the ship over time to match the desired rotation to match the ground. This is 
        //done smoothly (using Lerp) to make it feel more realistic
        rigidBody.MoveRotation(Quaternion.Lerp(rigidBody.rotation, rotation, Time.deltaTime * TurningSpeed));

        //Calculate the angle we want the ship's body to bank into a turn based on the current rudder.
        //It is worth noting that these next few steps are completetly optional and are cosmetic.
        //It just feels so darn cool
        float angle = AngleOfRoll * -input.rudder;

        //Calculate the rotation needed for this new angle
        Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
        //Finally, apply this angle to the ship's body
        ShipBody.rotation = Quaternion.Lerp(ShipBody.rotation, bodyRotation, Time.deltaTime * 10f);
    }


    private void CalculatePropulsion() {
        //Calculate the yaw torque based on the rudder and current angular velocity
        float rotationTorque = input.rudder - rigidBody.angularVelocity.y;
        //Apply the torque to the ship's Y axis
        rigidBody.AddRelativeTorque(0f, rotationTorque, 0f, ForceMode.VelocityChange);

        //Calculate the current sideways speed by using the dot product. This tells us
        //how much of the ship's velocity is in the "right" or "left" direction
        float sidewaysSpeed = Vector3.Dot(rigidBody.velocity, transform.right);

        //Calculate the desired amount of friction to apply to the side of the vehicle. This
        //is what keeps the ship from drifting into the walls during turns. If you want to add
        //drifting to the game, divide Time.fixedDeltaTime by some amount
        Vector3 sideFriction = -transform.right * (sidewaysSpeed / Time.fixedDeltaTime);

        //Finally, apply the sideways friction
        rigidBody.AddForce(sideFriction, ForceMode.Acceleration);

        //If not propelling the ship, slow the ships velocity
        if (input.thruster <= 0f) rigidBody.velocity *= SlowingVelFactor;

        //Braking or driving requires being on the ground, so if the ship
        //isn't on the ground, exit this method
        if (!isOnGround) return;

        //If the ship is braking, apply the braking velocty reduction
        if (input.isBraking) rigidBody.velocity *= BrakingVelFactor;

        //Calculate and apply the amount of propulsion force by multiplying the drive force
        //by the amount of applied thruster and subtracting the drag amount
        float propulsion = DriveForce * input.thruster - drag * Mathf.Clamp(Speed, 0f, TerminalVelocity);
        rigidBody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);

        float wheelAngle = Mathf.Lerp(0, AngleOfWheelsRoll, Mathf.Lerp(0, 1, Speed / TerminalVelocity));
        // Quaternion wheelRotation = transform.rotation * Quaternion.Euler(wheelAngle, 0f, shipBody.eulerAngles.z);
        // wheelsFront.rotation = Quaternion.Lerp(wheelsFront.rotation, wheelRotation, Time.deltaTime * 200f);
        // wheelsBack.rotation = Quaternion.Lerp(wheelsBack.rotation, wheelRotation, Time.deltaTime * 200f);
        WheelsFront.localRotation = Quaternion.Euler(-wheelAngle, 0, 0);
        WheelsBack.localRotation = Quaternion.Euler(-wheelAngle, 0, 0);
    }


    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) {
            //...calculate how much upward impulse is generated and then push the vehicle down by that amount 
            //to keep it stuck on the track (instead up popping up over the wall)
            Vector3 upwardForceFromCollision = Vector3.Dot(collision.impulse, transform.up) * transform.up;
            rigidBody.AddForce(-upwardForceFromCollision, ForceMode.Impulse);
        }
    }


    public float GetSpeedPercentage() {
        //Returns the total percentage of speed the ship is traveling
        return rigidBody.velocity.magnitude / TerminalVelocity;
    }
}