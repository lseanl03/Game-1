using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDespawn : MonoBehaviour
{
  private void Update()
  {
      Invoke("Destroy", 0.5f);
  }
  void Destroy()
  {
      Destroy(gameObject);
  }
}
