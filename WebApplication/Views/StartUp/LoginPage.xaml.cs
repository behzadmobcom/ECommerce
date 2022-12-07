using WebApplication.ViewModels.StartUp;

namespace WebApplication.Views.StartUp;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}