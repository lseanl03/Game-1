using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject interactButton;

    public float radius;
    private void Start()
    {
        radius = 5f;
        HideButton();
    }
    private void Update()
    {
        CheckButton();
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckInteraction();    
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
    public void CheckInteraction()
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
        }
    }
    public void CheckButton()
    {
        bool hasInteractable = false;
        Collider2D[] collider2d = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in collider2d)
        {
            if (collider.transform.GetComponent<Interactable>())
            {
                hasInteractable = true;
                ShowButton();
                interactButton.transform.position = collider.transform.position + new Vector3(0, 1f, 0f);
            }
        }
        if (!hasInteractable)
        {
            HideButton();
        }
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
            }
        }
    }
}
