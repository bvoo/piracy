using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float moveSpeedMax = 5f;
  public float moveSpeedMul = 5f;
  public Rigidbody2D rb;

  private void FixedUpdate() {
    var mul = InputHelper.MoveInput * moveSpeedMul;

    rb.velocity = new Vector2(
      Math.Clamp(rb.velocity.x + mul.x, -moveSpeedMax, moveSpeedMax),
      Math.Clamp(rb.velocity.y + mul.y, -moveSpeedMax, moveSpeedMax)
    );
  }
}