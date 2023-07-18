namespace Codebase.UI.Factory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void CreateHUD();
        void CreateMenu();
    }
}