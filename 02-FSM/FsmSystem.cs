using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FSMSystem  {

	private Dictionary<StateID,FSMState> states=new Dictionary<StateID, FSMState>();
	private StateID currentStateID;
	private FSMState currenState;

	public void Update(GameObject npc){
		currenState.Act (npc);
	}

	public void AddState(FSMState s){
		Debug.Log ("=============");
		if(s==null){
			Debug.LogError ("FSMState不能为空");return;
		}
		if(currenState==null){
			currenState = s;
			currentStateID = s.ID;
		}
		if (states.ContainsKey (s.ID)) {
			Debug.LogError ("状态"+s.ID+"已经存在，无法重复添加");return;
		}
		states.Add (s.ID, s);
	}

	public void DeleteState(StateID id){
		if(id==StateID.NullStateId){
			Debug.LogError ("无法删除空状态");return;
		}
		if (states.ContainsKey (id) == false) {
			Debug.LogError ("无法删除存在的状态：" + id);
			return;
		}
		states.Remove (id);
	}

	/// <summary>
	/// 状态切换
	/// </summary>
	/// <param name="trans">条件.</param>
	public void PerformTransiton(Transition trans){
		if (trans == Transition.NullTransiton) {
			Debug.LogError ("无法执行空的转换条件");
		}
		StateID id= currenState.GetOutputState (trans);
		if (id == StateID.NullStateId) {
			Debug.LogWarning ("当前状态" + currentStateID + "无法根据转换条件" + trans + "发生转换");
			return;
		}
		if(states.ContainsKey(id)==false){
			Debug.LogError ("在状态里面不存在状态" + id + ",无法进行状态转换！");return;
		}
		FSMState state = states [id];
		//因为要发生转换所以调用当前状态的离开函数
		currenState.DoAfterLeaving ();
		currenState = state;
		currentStateID = id;
		//调用新状态的进入函数
		currenState.DobeforeEntering ();
	}
}
