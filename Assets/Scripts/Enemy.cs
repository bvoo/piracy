using UnityEngine;

public class Enemy : MonoBehaviour {
  public float health = 100f;

  public GameObject bulletPrefab;
  public GameObject healthBar;
  public float shootTimer = 3f;

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
    if (shootTimer <= 0.5f) {
      
      Instantiate(bulletPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
      shootTimer = 3f;
    } else {
      shootTimer -= Time.deltaTime;
    }
  }
}
