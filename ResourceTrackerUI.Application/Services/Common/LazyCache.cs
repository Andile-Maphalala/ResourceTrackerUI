

namespace ResourceTrackerUI.Application.Services.Common
{
    public class LazyCache<TResult>
    {
        private TResult? _value;
        private bool _loaded;
        private readonly Func<CancellationToken, Task<TResult>> _fetcher;

        public LazyCache(Func<CancellationToken, Task<TResult>> fetcher)
        {
            _fetcher = fetcher;
        }

        public async Task<TResult> GetAsync(bool forceRefresh, CancellationToken cancellationToken)
        {
            if (!_loaded || forceRefresh)
            {
                _value = await _fetcher(cancellationToken);
                _loaded = true;
            }
            return _value!;
        }

        public void Invalidate() => _loaded = false;
    }
}
