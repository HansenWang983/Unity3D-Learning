using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public class debug_test : MonoBehaviour {
		// 限制 update 的输出次数
		private int UpdateTest;
		private int OnGuiTest;
		private int FixedUpdateTest;
		private int LateUpdateTest;

		void Start () {
			UpdateTest = FixedUpdateTest = 0;
			OnGuiTest = LateUpdateTest = 0;
			Debug.Log("Start");
		}

		void Update () {
			if (UpdateTest != 3) {
				Debug.Log("Update");
				UpdateTest += 1;
			}
		}

		private void Awake() {
			Debug.Log("Awake");
		}

		private void FixedUpdate() {
			if (FixedUpdateTest != 3) {
				Debug.Log("FixedUpdate");
				FixedUpdateTest += 1;
			}
		}

		private void LateUpdate() {
			if (LateUpdateTest != 3)
			{
				Debug.Log("LateUpdate");
				LateUpdateTest += 1;
			}
		}

		private void OnGUI() {
			if (OnGuiTest != 3) {
				Debug.Log("OnGUI");
				OnGuiTest += 1;
			}
		}

		private void OnDisable() {
			Debug.Log("OnDisable");
		}

		private void OnEnable() {
			Debug.Log("OnEnable");
		}
	}
