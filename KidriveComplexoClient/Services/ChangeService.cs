using System;

namespace KidriveComplexoClient.Services
{
    public class ChangeService : IChangeService
    {
        public event Action OnChange;
        public void Change()
        {
            OnChange?.Invoke();
        }
    }
}
