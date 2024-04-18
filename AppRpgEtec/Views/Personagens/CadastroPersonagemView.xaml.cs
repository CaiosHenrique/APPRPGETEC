using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views.Personagens;

public partial class CadastroPersonagemView : ContentPage
{
	private CadastroPersonagemViewModel cadviewModel;
	public CadastroPersonagemView()
	{
		InitializeComponent();

		cadviewModel = new CadastroPersonagemViewModel();
		BindingContext = cadviewModel;
		Title = "Novo de Personagem";
	}
}