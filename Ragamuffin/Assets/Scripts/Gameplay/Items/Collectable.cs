using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D col)
  {
    var collector = col.GetComponent<Collector>();

    if (collector == null) return;

    collector.Collect(gameObject);
    gameObject.SetActive(false);
  }
}
