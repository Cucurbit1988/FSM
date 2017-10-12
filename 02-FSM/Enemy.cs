using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private FSMSystem fsm;
	// Use this for initialization
	void Start () {
		ImitFSM ();
	}

	void ImitFSM(){
		fsm = new FSMSystem ();
		FSMState patrolState = new PatrolState (fsm);
		fsm.AddState (patrolState);
	}

	// Update is called once per frame
	void Update () {
		fsm.Update (this.gameObject);
	}
}
