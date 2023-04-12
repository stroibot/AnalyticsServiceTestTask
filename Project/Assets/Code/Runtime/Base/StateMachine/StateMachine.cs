using System;
using System.Collections.Generic;
using UnityEngine;

namespace stroibot.Base.StateMachine
{
	public class StateMachine<TTag>
		where TTag : Enum
	{
		private static readonly string LogTag = $"{nameof(StateMachine<TTag>)}<{typeof(TTag).Name}>";

		public IState CurrentState => _stateHistory.Count > 0 ? _stateHistory.Peek() : null;

		private readonly Dictionary<TTag, IState> _states;
		private readonly Stack<IState> _stateHistory;
		private readonly ILogger _logger;

		public StateMachine(
			ILogger logger)
		{
			_logger = logger;
			_states = new Dictionary<TTag, IState>();
			_stateHistory = new Stack<IState>();
		}

		public void Add(TTag tag, IState state)
		{
			if (_states.ContainsKey(tag))
			{
				return;
			}

			_states.Add(tag, state);
		}

		public void SwitchToState(IState newState)
		{
			if (newState == CurrentState)
			{
				_logger.Log(LogTag, $"Already in {newState}");
				return;
			}

			if (newState == null)
			{
				throw new ArgumentNullException(nameof(newState));
			}

			var currentState = CurrentState;
			_stateHistory.Push(newState);

			if (currentState != null)
			{
				_logger.Log(LogTag, $"Exiting {currentState}");
				currentState.OnExit();
			}

			_logger.Log(LogTag, $"Entering {newState}");
			newState.OnEnter();
		}

		public void Enter(TTag tag)
		{
			if (_states.TryGetValue(tag, out IState newState))
			{
				SwitchToState(newState);
			}
			else
			{
				_logger.LogWarning(LogTag, $"State Machine does not contain state with a tag {tag}");
			}
		}
	}
}
