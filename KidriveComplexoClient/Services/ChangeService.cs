using System;
using System.Threading.Tasks;

namespace KidriveComplexoClient.Services
{
    public class ChangeService : IChangeService
    {
        public event Action OnChange;
        public void Change()
        {
            OnChange?.Invoke();
        }

        public async void ChangeAsync()
        {
            await Task.Run(() => OnChange?.Invoke());
        }
    }
}
