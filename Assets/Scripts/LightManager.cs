using System;
using UnityEngine;

public unsafe class LightManager : MonoBehaviour {

	public Transform camera;
	public Color darkness;
	public Material material;

	private void Awake() => camera = Camera.main.transform;

	private void Update() {
		transform.position = new Vector3(camera.position.x, camera.position.y, transform.position.z);
		if (material) material.SetColor("_Darkness", darkness);
	}
}