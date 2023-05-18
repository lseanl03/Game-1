using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicFireManager : MonoBehaviour
{
    public GameObject magicFire;
    private GameObject magicFireObj;
    public Transform magicFirePos;
    private void Start()
    {
        magicFireObj=Instantiate(magicFire, magicFirePos.transform.position, Quaternion.identity);
        magicFireObj.transform.parent = gameObject.transform;
    }
    void Update()
    {
        Follow();
    }
    void Follow()
    {
        magicFireObj.transform.position = magicFirePos.position;
    }
    private void OnDestroy()
    {
        Destroy(magicFireObj);
    }
}
