using UnityEngine;

public class Enemy : MonoBehaviour {
  public float health = 100f;

  public GameObject bulletPrefab;
  public GameObject healthBar;

  private void Awake() {
    OnHealthChanged();
  }

  private void OnHealthChanged() {
    var scale = healthBar.transform.localScale;

    scale.x = health / 100;

    healthBar.transform.localScale = scale;

    if (health <= 0) {
      Destroy(gameObject);
    }
  }

  private void FixedUpdate() {
    if (Physics2D.OverlapCircle(transform.position, 0.75f, LayerMask.GetMask("Bullet"))) {
      health -= 10;
      OnHealthChanged();
    }
  }
}
