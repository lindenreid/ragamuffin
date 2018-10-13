using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collection))]
public class Collector : MonoBehaviour
{
  private Collection collection;
  private GameObject collectionHUD;

  void Start()
  {
    collection = GetComponent<Collection>();
    collectionHUD = GameObject.Find("CollectionHUD");
  }

  public void Collect(GameObject collectable)
  {
    collection.AddObject(collectable);
    collectable.transform.parent = transform;
    addCollectableToHUD(collectable);
  }

  private void addCollectableToHUD(GameObject collectable)
  {
    var sprRnd = collectable.GetComponent<SpriteRenderer>();

    if (sprRnd == null || collectionHUD == null) return;

    GameObject obj = new GameObject();
    obj.AddComponent<RectTransform>();

    Image image = obj.AddComponent<Image>();
    image.sprite = sprRnd.sprite;

    obj.transform.SetParent(collectionHUD.transform, false);
  }
}
