using System;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
  public delegate void Notify(GameObject collectable);
  public event Notify OnAddObject;

  public List<GameObject> objects;

  public void AddObject(GameObject collectable)
  {
    objects.Add(collectable);

    if (OnAddObject != null)
    {
      OnAddObject(collectable);
    }
  }
}
