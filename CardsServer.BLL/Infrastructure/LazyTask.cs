using System.Runtime.CompilerServices;

namespace CardsServer.BLL.Infrastructure
{
    /// <summary>
    /// Вариант ленивой "lazy" задачи. Создан в процессе ознакомления с конечным автоматом.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct LazyTask<T> : INotifyCompletion
    {
        private readonly Lazy<Task<T>> _lazy;

        public LazyTask(Func<Task<T>> factory)
        {
            this._lazy = new Lazy<Task<T>>(factory);
        }

        public LazyTask<T> GetAwaiter()
            => this;

        public bool IsCompleted
            => !this._lazy.IsValueCreated && this._lazy.Value.IsCompleted;

        public T GetResult()
            => this._lazy.Value.GetAwaiter().GetResult();

        public void OnCompleted(Action continuation)
            => this._lazy.Value.GetAwaiter().OnCompleted(continuation);
    }
}
