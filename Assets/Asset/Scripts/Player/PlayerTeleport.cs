using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public bool canTeleport = true;
    public bool teleportActive = false;
    public bool isGround = true;
    public bool remainingEnergy = false;
    public float teleportSpeed = 15f;
    public float energyConsumption = 20f;
    public float maxTeleportTime = 10f;
    public float currentTeleportTime;

    private Rigidbody2D rb2d;

    private Vector3 teleportingTargetObjMovement;
    private Vector3 teleportingStartPos;

    public TeleportBar teleportBar;
    public PlayerController playerController;
    public CameraController cameraController;
    public EnergyManager energyManager;

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
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        currentTeleportTime = maxTeleportTime;
        teleportBar.SetMaxTeleportTime(maxTeleportTime);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canTeleport)
        {
                CheckTeleportActive();
        }
        if (teleportActive)
        {
            MoveTeleportTarget();
        }
        CheckTeleportTime();
        CheckInGround();
        GravityChange();
    }
    public bool StartTeleporting()
    {
        if (isGround && canTeleport && remainingEnergy)
        {
            teleportBar.SetTeleportTime(currentTeleportTime);
            SpawnParticleObj();
            teleportingStartPos = transform.position;
            teleportActive = true;
            Time.timeScale = 0.99f;
            return true;
        }
        return false;
    }
    public void CheckTeleportActive()
    {
        if (!teleportActive)
        {
            CheckEnergy();
            if (energyManager.canUseEnergy)
            {
                remainingEnergy = true;
                StartTeleporting();
            }
            else
            {
                remainingEnergy = false;
                Debug.Log("OutOfEnergy");
                return;
            }
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
        if(teleportingTargetObj!= null)
        {
            TeleportTarget teleportTarget = teleportingTargetObj.GetComponent<TeleportTarget>();
            if (Vector3.Distance(teleportingTargetObj.transform.position, teleportingStartPos) > 20f || !teleportTarget.canTele)
            {
                teleportBar.fillSpeed = maxTeleportTime / 2;
                canTeleport = false;
                teleportingTargetObj.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                teleportBar.fillSpeed = maxTeleportTime / 10;
                canTeleport = true;
                teleportingTargetObj.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }
    public void Teleport()
    {
        teleportBar.gameObject.SetActive(false);
        gameObject.transform.position = teleportingTargetObj.transform.position;
        SpawnEffectObj();
        FindObjectOfType<AudioManager>().PlaySFX("Teleport");
        Time.timeScale = 1f;
        teleportActive = false;
        isGround = false;
        cameraController.followPlayer = true;
        DestroyParticleObj();
        DestroyEffect();
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
        teleportLimitCircleObj = Instantiate(teleportLimitCircle, transform.position, teleportLimitCircle.transform.rotation);
        teleportingTargetObj = Instantiate(teleportTargetSelect, transform.position, teleportTargetSelect.transform.rotation);
        if (teleportingTargetObj != null || teleportLimitCircleObj != null)
        {
            SetParent(teleportLimitCircleObj);
            SetParent(teleportingTargetObj);
        }
    }
    public void DestroyParticleObj()
    {
        Destroy(teleportingTargetObj);
        Destroy(teleportLimitCircleObj);
    }
    void DestroyEffect()
    {
        Destroy(effect1, 1f);
        Destroy(effect2, 1f);
    }
    public void SpawnEffectObj()
    {
        effect1 = Instantiate(teleportEffect1, teleportLimitCircleObj.transform.position, teleportEffect1.transform.rotation);
        effect2 = Instantiate(teleportEffect2, teleportingTargetObj.transform.position, teleportEffect2.transform.rotation);
        if (effect1 != null || effect2 != null)
        {
            SetParent(effect1);
            SetParent(effect2);
        }
    }
    public void GravityChange()
    {
        if (Time.timeScale != 1)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if(Time.timeScale==1)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void CheckEnergy()
    {
        if(!teleportActive && isGround)
        {
            energyManager.ConsumptionEnergy(energyConsumption);
        }
    }
    public void CheckTeleportTime()
    {
        if (teleportBar.slider.value <= 0 && teleportBar.gameObject.activeSelf)
        {
            Time.timeScale = 1f;
            teleportActive = false;
            isGround = false;
            canTeleport = true;
            cameraController.followPlayer = true;
            GravityChange();
            DestroyParticleObj();
            rb2d.gravityScale = rb2d.gravityScale +0.001f;
        }
    }
    void SetParent(GameObject obj)
    {
        GameObject objSpawn = GameObject.Find("SpawnParticleEffects");
        if(objSpawn != null)
        {
            obj.transform.parent = objSpawn.transform;
        }
        else
        {
            Debug.Log("a");
        }
    }
}