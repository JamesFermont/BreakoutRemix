using UnityEngine;

public class DataPack : MonoBehaviour {
	public int fallSpeed;
	public int energyGiven;
	private void OnEnable() {
		Mathf.Clamp(fallSpeed, 10, 300);
		Mathf.Clamp(energyGiven, 1, 10000);
	}

	private void Start() {
		GetComponent<Rigidbody2D>().velocity = Vector2.down * fallSpeed * Time.fixedDeltaTime;
	}
}
