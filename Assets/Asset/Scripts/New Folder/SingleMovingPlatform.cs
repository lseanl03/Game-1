//using UnityEngine;
//using System.Collections;
//[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(Rigidbody))]
//public class Platform : MonoBehaviour
//{
//    public Transform startPoint;
//    public Transform endPoint;
//    public enum UsedAxis { X, Y, Z, X_Y, X_Z, Y_Z, X_Y_Z }
//    public UsedAxis usedAxis;
//    public enum UseCase { Auto, Manual, Remote }
//    public UseCase useCase;
//    public Collider button = null;
//    public GameObject player = null;
//    private float _moveSpeed = 0.75f;
//    private bool _return;
//    private bool _switch;
//    private Rigidbody _platformRigidBody = null;
//    private Transform _platform;
//    private ButtonSignal _buttonSignal;
//    void Start()
//    {
//        _platformRigidBody = transform.rigidbody;
//        _platformRigidBody.mass = 2000;
//        _platformRigidBody.isKinematic = true;
//        _platformRigidBody.useGravity = false;
//        _platform = transform;
//        player.gameObject.tag = "Player";
//        if (useCase == UseCase.Remote)
//        {
//            button.gameObject.AddComponent<ButtonSignal>();
//            _buttonSignal = button.GetComponent<ButtonSignal>();
//        }
//        InitializeWorkingAxis();
//    }
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0)) _switch = !_switch;
//    }
//    void FixedUpdate()
//    {
//        if (useCase == UseCase.Auto)
//        {
//            if (_platform.position == endPoint.position) _return = true;
//            if (_platform.position == startPoint.position) _return = false;
//            if (_return) _platform.position = Vector3.MoveTowards(_platform.position, startPoint.position, _moveSpeed * Time.fixedDeltaTime);
//            if (!_return) _platform.position = Vector3.MoveTowards(_platform.position, endPoint.position, _moveSpeed * Time.fixedDeltaTime);
//        }
//        if (useCase == UseCase.Manual)
//        {
//            if (_switch) _platform.position = Vector3.MoveTowards(_platform.position, startPoint.position, _moveSpeed * Time.fixedDeltaTime);
//            if (!_switch) _platform.position = Vector3.MoveTowards(_platform.position, endPoint.position, _moveSpeed * Time.fixedDeltaTime);
//        }
//        if (useCase == UseCase.Remote)
//        {
//            if (_buttonSignal._switch) _platform.position = Vector3.MoveTowards(_platform.position, startPoint.position, _moveSpeed * Time.fixedDeltaTime);
//            if (!_buttonSignal._switch) _platform.position = Vector3.MoveTowards(_platform.position, endPoint.position, _moveSpeed * Time.fixedDeltaTime);
//        }
//    }
//    void OnDrawGizmos()
//    {
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireCube(startPoint.position, transform.localScale);
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireCube(endPoint.position, transform.localScale);
//    }
//    void InitializeWorkingAxis()
//    {
//        switch (usedAxis)
//        {
//            case UsedAxis.X:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
//                break;
//            case UsedAxis.Y:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
//                break;
//            case UsedAxis.Z:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
//                break;
//            case UsedAxis.X_Y:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
//                break;
//            case UsedAxis.X_Z:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
//                break;
//            case UsedAxis.Y_Z:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
//                break;
//            case UsedAxis.X_Y_Z:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
//                break;
//            default:
//                _platformRigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
//                break;
//        }
//    }
//}
//[RequireComponent(typeof(BoxCollider))]
//public class ButtonSignal : MonoBehaviour
//{
//    public bool _switch = false;
//    private BoxCollider _buttonTrigger;
//    void Start()
//    {
//        _buttonTrigger = GetComponent<BoxCollider>();
//        _buttonTrigger.isTrigger = true;
//    }
//    void OnTriggerEnter(Collider c)
//    {
//        if (c.gameObject.tag == "Player")
//        {
//            _switch = !_switch;
//        }
//    }