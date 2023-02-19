using UnityEngine;

public class Stars : MonoBehaviour {
  public int starCount = 100;

  public AnimationCurve starSize = new();
  public Vector2 starRect = new(25f, 25f);

  public float parallaxFactor;

  public Gradient starColor;

  public Transform cameraTransform;

  private Vector2 _centerOffset;

  private ParticleSystem _particles;
  private ParticleSystem.Particle[] _stars;

  private void Awake() {
    _stars = new ParticleSystem.Particle[starCount];
    _particles = GetComponent<ParticleSystem>();

    if (Camera.main is not null) starRect = Camera.main.pixelRect.size / 64;

    _centerOffset = starRect / 2;

    var min = starSize.keys[0].value;
    var max = starSize.keys[1].value;

    for (var i = 0; i < starCount; i++) {
      var size = starSize.Evaluate(Random.value);

      var color = starColor.Evaluate(Remap(size, min, max));

      var position = transform.position;

      _stars[i].position = RandomPosition(starRect) + new Vector2(position.x, position.y);
      _stars[i].startSize = size;
      _stars[i].startColor = color;
    }

    _particles.SetParticles(_stars, _stars.Length);
  }

  private void Update() {
    for (var i = 0; i < starCount; i++) {
      var pos = _stars[i].position + transform.position;

      if (pos.x < cameraTransform.position.x - _centerOffset.x)
        pos.x += starRect.x;
      else if (pos.x > cameraTransform.position.x + _centerOffset.x) pos.x -= starRect.x;

      if (pos.y < cameraTransform.position.y - _centerOffset.y)
        pos.y += starRect.y;
      else if (pos.y > cameraTransform.position.y + _centerOffset.y) pos.y -= starRect.y;

      _stars[i].position = pos - transform.position;
    }

    _particles.SetParticles(_stars, _stars.Length);

    var newPos = cameraTransform.position / parallaxFactor;

    newPos.z = 1;

    transform.position = newPos;
  }

  private static float Remap(float value, float min, float max) { return (value - min) / (max - min); }

  private Vector2 RandomPosition(Vector2 rect) {
    var x = Random.Range(0, rect.x);
    var y = Random.Range(0, rect.y);

    return new Vector2(x, y) - _centerOffset;
  }
}
