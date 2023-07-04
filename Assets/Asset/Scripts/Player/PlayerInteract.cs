using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool canInteract;
    public bool interacting = false;
    public float radius =5f;
    public GameObject interactButton;
    private void Start()
    {

    }
    private void Update()
    {
        CheckInteractableObj();
        if (Input.GetKeyDown(KeyCode.F) && canInteract)
        {
            Interaction();
            FindObjectOfType<AudioManager>().PlaySFX("Interact");
        }
    }
     public void ShowButton()
    {
        interactButton.SetActive(true);
    }
    public void HideButton()
    {
        interactButton.SetActive(false);
    }
    public void Interaction()
    {
        Collider2D[] collider2d = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in collider2d)
        {
            if (collider.transform.GetComponent<Interactable>())
            {
                collider.transform.GetComponent<Interactable>().Interact();
                if (collider.transform.gameObject.GetComponent<PlatformSwitch>().turnOn)
                {
                    MovingPlatform();
                }
                return;
            }
            if (collider.transform.GetComponent<NPC>())
            {
                interacting = true;
                collider.transform.GetComponent<NPC>().Dialogue();
            }
        }
    }
    public void CheckInteractableObj()
    {
        canInteract = false;
        Collider2D[] collider2d = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in collider2d)
        {
            if (collider.transform.GetComponent<Interactable>())
            {
                Vector3 newVector = new Vector3(0, 5f, 0f);
                Active();
                interactButton.transform.position = collider.transform.position + newVector;
            }

            if (collider.transform.GetComponent<NPC>())
            {
                Vector3 newVector = new Vector3(0, 6f, 0f);
                Active();
                interactButton.transform.position = collider.transform.position + newVector;
                if (!collider.transform.GetComponent<NPC>().talking)
                {
                    if (interacting)
                    {
                        interacting = false;
                        return;
                    }
                }
            }
        }
        if (!canInteract)
        {
            HideButton();
            return;
        }
    }
    void Active()
    {
        canInteract = true;
        ShowButton();
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void MovingPlatform()
    {
        GameObject platforms = GameObject.Find("RemotePlatform");
        foreach (Transform platform in platforms.transform)
        {
            if (platform.GetComponent<MovingPlatform>() && platform.GetComponent<MovingPlatform>().useCase == global::MovingPlatform.UseCase.Remote)
            {
                platform.GetComponent<MovingPlatform>().unlocked = true;
                Debug.Log(platform.GetComponent<MovingPlatform>().unlocked +" set unlock");
                Debug.Log(" set unlock");
            }
        }
    }
}
