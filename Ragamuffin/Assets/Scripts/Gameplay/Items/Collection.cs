using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
  public List<GameObject> objects;

  public void AddObject(GameObject collectable)
  {
    objects.Add(collectable);
  }
}
