using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public bool canTeleport = true;
    public bool teleportActive = false;
    public bool isGround = true;
    public float teleportSpeed = 15f;

    private Vector3 teleportingTargetObjMovement;
    private Vector3 teleportingStartPos;

    public PlayerController playerController;
    public CameraController cameraController;

    public GameObject teleportLimitCircle;
    public GameObject teleportTargetSelect;
    public GameObject teleportEffect1;
    public GameObject teleportEffect2;
    private GameObject teleportLimitCircleObj;
    private GameObject teleportingTargetObj;
    private GameObject effect1;
    private GameObject effect2;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        CheckInGround();
        if (Input.GetKeyDown(KeyCode.Q) && canTeleport)
        {
            CheckTeleportActive();
        }
        if (teleportActive)
        {
            MoveTeleportTarget();
        }
    }
    public bool StartTeleporting()
    {
        if (isGround && canTeleport)
        {
            SpawnParticleObj();
            teleportingStartPos = transform.position;
            teleportActive = true;
            Time.timeScale = 0f;
            return true;
        }
        return false;
    }
    public void CheckTeleportActive()
    {
        if (!teleportActive)
        {
            StartTeleporting();
        }
        else
        {
            Teleport();
        }
    }
    public void TeleportTargetChanged(Vector2 input)
    {
        teleportingTargetObjMovement = new Vector2(input.x, input.y);
    }
    public void MoveTeleportTarget()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        TeleportTargetChanged(new Vector2(horizontalInput, verticalInput));
        teleportingTargetObj.transform.position += Time.unscaledDeltaTime * teleportSpeed * teleportingTargetObjMovement;
        cameraController.followPlayer = false;
        cameraController.transform.position += Time.unscaledDeltaTime * teleportSpeed * teleportingTargetObjMovement;
        CheckTeleportArea();

    }
    public void CheckTeleportArea()
    {
        if (Vector3.Distance(teleportingTargetObj.transform.position, teleportingStartPos) > 20f)
        {
            canTeleport = false;
        }
        else
        {
            canTeleport = true;
        }
    }
    public void Teleport()
    {
        SpawnEffectObj();
        gameObject.transform.position = teleportingTargetObj.transform.position;
        Time.timeScale = 1f;
        teleportActive = false;
        isGround = false;
        cameraController.followPlayer = true;
        DestroyParticleObj();
    }
    public void CheckInGround()
    {
        if (playerController.IsGrounded())
        {
            isGround = true;
        }
    }
    public void SpawnParticleObj()
    {
        teleportLimitCircleObj = Instantiate(teleportLimitCircle, gameObject.transform.position, teleportLimitCircle.transform.rotation);
        teleportingTargetObj = Instantiate(teleportTargetSelect, gameObject.transform.position, teleportTargetSelect.transform.rotation);
    }
    public void DestroyParticleObj()
    {
        Destroy(teleportingTargetObj);
        Destroy(teleportLimitCircleObj);
    }
    public void SpawnEffectObj()
    {
        effect1 = Instantiate(teleportEffect1, teleportLimitCircleObj.transform.position, teleportEffect1.transform.rotation);
        effect2 = Instantiate(teleportEffect2, teleportingTargetObj.transform.position, teleportEffect2.transform.rotation);
        Destroy(effect1,1f);
        Destroy(effect2,1f);
    }
}