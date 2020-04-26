using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	public Animator animator;
	[Range(0f, 10f)]
	public float speed = 1f;
	private static readonly int State = Animator.StringToHash("State");

	private void Update() {
		animator.speed = speed;
	}

	public void SetState(AnimationState state) => animator.SetInteger(State, (int) state);
}

public enum AnimationState {
	Idle,
	Run,
	InAir
}
