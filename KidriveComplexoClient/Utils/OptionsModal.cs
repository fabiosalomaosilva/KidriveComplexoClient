using Blazored.Modal;

namespace KidriveComplexoClient.Utils
{
    public class OptionsModal : IOptionsModal
    {
        public ModalOptions HideHeader()
        {
            return new ModalOptions()
            {
                HideHeader = true,
                Animation = ModalAnimation.FadeInOut(1),
                ContentScrollable = false,
                Position = ModalPosition.Center
            };

        }
    }
}
