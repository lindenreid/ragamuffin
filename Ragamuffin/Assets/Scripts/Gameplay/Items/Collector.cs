using UnityEngine;

[RequireComponent(typeof(Collection))]
public class Collector : MonoBehaviour
{
  private Collection collection;

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
