﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestSystem.ScriptabelObjects {
	public class Task_Composite_SO : TaskSO {
		
		// the following tasks in the quest ar the subtasks
		[SerializeField] private int count;
		public List<TaskSO> subTasks;
		public int subTasksCount => count;
		public int actualCount => subTasks.Count;
		private QuestSO quest;
		
///// Public Functions ///////////////////////////////////////////////////////////////////////////// 

		public void SetQuest(QuestSO questSO) {
			quest = questSO;
		}

		public void SetSubTasks(List<TaskSO> tasks) {
			subTasks.Clear();
			subTasks = tasks;
		}

///// TaskSO Overrides /////////////////////////////////////////////////////////////////////////////
		protected override TaskType Type { get; } = TaskType.Composite; 
		public override string BaseName { get; } = "Composite";
		
		public override bool IsDone() {
			done = subTasks.All(task => task.IsDone());
			
			return done;
		}

		public override void ResetTask() {
			subTasks.ForEach(task => task.ResetTask());
			done = false;
			active = false;
		}

		public override void StartTask() {
			active = true;
			subTasks.ForEach(task => task.StartTask());
		}

		public override void StopTask() {
			active = false;
			subTasks.ForEach(task => task.StopTask());
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////
		
		private void Awake() {
			subTasks = new List<TaskSO>();
		}
	}
}