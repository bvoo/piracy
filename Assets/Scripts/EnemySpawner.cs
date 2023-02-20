using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  public GameObject enemyPrefab;

  public float spawnTimer = 3f;

  private void FixedUpdate() {
    if (spawnTimer <= 0.5f) {
      var randomPos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
      Instantiate(enemyPrefab, transform.position + randomPos, Quaternion.Euler(randomPos));
      spawnTimer = 3f;
    }
    else {
      spawnTimer -= Time.deltaTime;
    }
  }
}
