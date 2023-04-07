using UnityEngine;

public class FireController : MonoBehaviour
{
    public Transform magicStickPos;

    //private void Start()
    //{
    //    FindObjectOfType<AudioManager>().PlaySFX("");
    //}
    private void Update()
    {
        Vector2 pos= magicStickPos.position;
        transform.position=new Vector2(pos.x,pos.y);
    }
}