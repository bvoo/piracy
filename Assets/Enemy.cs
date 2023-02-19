using UnityEngine;

public class Enemy : MonoBehaviour {
  public float health = 100f;

  public GameObject healthBar;

  private void Awake() {
    health = 45;
    OnHealthChanged();
  }

  private void OnHealthChanged() {
    var scale = healthBar.transform.localScale;

    scale.x = health / 100;

    healthBar.transform.localScale = scale;
  }
}
