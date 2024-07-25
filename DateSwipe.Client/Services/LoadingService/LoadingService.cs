namespace DateSwipe.Client.Services.LoadingService
{
    public class LoadingService
    {
        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }

        public async Task RunWithLoading(Func<Task> action)
        {
            Show();
            try
            {
                await action();
            }
            finally
            {
                Hide();
            }
        }
    }
}
