using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CollectionHUD : MonoBehaviour
{
  public Collection collection;

  private RectTransform UIArea;

  void Start()
  {
    collection.OnAddObject += DisplayObject;
    UIArea = GetComponent<RectTransform>();
  }

  void DisplayObject(GameObject collectable)
  {
    var spriteRenderer = collectable.GetComponent<SpriteRenderer>();
    if (spriteRenderer == null || collection == null) return;

    GameObject obj = new GameObject();
    obj.AddComponent<RectTransform>();

    Image image = obj.AddComponent<Image>();
    image.sprite = spriteRenderer.sprite;

    obj.transform.SetParent(UIArea.transform, false);
  }
}
