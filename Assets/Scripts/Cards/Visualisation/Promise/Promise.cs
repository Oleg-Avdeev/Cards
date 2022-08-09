using System;

namespace Witches.Cards.Visualisation.Promises
{
	public class Resolver
	{
		private Action _onComplete;

		public Resolver(Action onComplete)
		{
			_onComplete = onComplete;
		}

		public void Resolve()
		{
			_onComplete.Invoke();
		}

		public void Reject()
		{

		}
	}

	public class Promise
	{
		public event Action OnCompletion;
		private Action<Resolver> _resolver;

		private Promise(Action<Resolver> resolver)
		{
			_resolver = resolver;
		}

		public static Promise Create(Action<Resolver> action)
		{
			return new Promise(action);
		}

		public void Run()
		{
			_resolver.Invoke(new Resolver(() => OnCompletion?.Invoke()));
		}
	}
}