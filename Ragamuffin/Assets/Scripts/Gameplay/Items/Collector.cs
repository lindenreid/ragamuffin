using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collection))]
public class Collector : MonoBehaviour
{
  Collection collection;

  void Start()
  {
    collection = GetComponent<Collection>();
  }

  public void Collect(GameObject collectable)
  {
    collection.AddObject(collectable);
    collectable.transform.parent = transform;
  }
}
