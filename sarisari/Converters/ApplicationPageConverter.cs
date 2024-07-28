namespace sarisari
{
    public static class ApplicationPageConverter
    {
        public static WpfCore.BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.MainPage:
                    return new MainPage(viewModel as MainPageViewModel);
                case ApplicationPage.ProductPage:
                    return new ProductPage(viewModel as ProductPageViewModel);
                case ApplicationPage.SettingsPage:
                    return new SettingsPage(viewModel as SettingsPageViewModel);
                case ApplicationPage.UserPage:
                    return new UserPage(viewModel as UserPageViewModel);
                case ApplicationPage.SupplierPage:
                    return new SupplierPage(viewModel as SupplierPageViewModel);
                case ApplicationPage.RolePage:
                    return new RolePage(viewModel as RolePageViewModel);
                case ApplicationPage.CategoryPage:
                    return new CategoryPage(viewModel as CategoryPageViewModel);
                case ApplicationPage.SizePage:
                    return new SizePage(viewModel as SizePageViewModel);
                case ApplicationPage.StockPage:
                    return new StockPage(viewModel as StockPageViewModel);
                default:
                    return null;
            }
        }
    }
}
