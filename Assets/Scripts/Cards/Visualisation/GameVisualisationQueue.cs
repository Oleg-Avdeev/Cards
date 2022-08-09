using Witches.Cards.System;
using System;
using System.Collections.Generic;
using UnityEngine;
using Witches.Cards.Visualisation.Promises;

namespace Witches.Cards.Visualisation
{
	public class EventVisualisationProcess
	{
		public bool IsRunning;
		public GameEvent Event;

		private readonly Promise _visualisationPromise;

		public EventVisualisationProcess(GameEvent gameEvent, Promise visualisationPromise)
		{
			Event = gameEvent;
			_visualisationPromise = visualisationPromise;
		}
		
		public void Run()
		{
			IsRunning = true;
			_visualisationPromise.OnCompletion += () => IsRunning = false;
			_visualisationPromise.Run();
		}
	}

	public class GameVisualisationQueue : MonoBehaviour
	{
		private Queue<EventVisualisationProcess> _visualisationQueue = new Queue<EventVisualisationProcess>();
		private EventVisualisationProcess _currentProcess;

		public void Enqueue(EventVisualisationProcess process)
		{
			_visualisationQueue.Enqueue(process);
			
			if (_currentProcess == null)
			{
				_currentProcess = process;
				_currentProcess.Run();
			}
		}

		private void Update()
		{
			if (_currentProcess == null) return;
			if (_currentProcess.IsRunning) return;
			
			_currentProcess = null;

			if (_visualisationQueue.TryDequeue(out _currentProcess))
				_currentProcess.Run();
		}
	}
}