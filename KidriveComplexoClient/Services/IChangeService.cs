using System;

namespace KidriveComplexoClient.Services
{
    public interface IChangeService
    {
        event Action OnChange;
        void Change();
        void ChangeAsync();
    }
}
