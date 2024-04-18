using AppRpgEtec.Models;
using AppRpgEtec.Models.Enuns;
using AppRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRpgEtec.ViewModels.Personagens
{
    class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("token", string.Empty);
            pService = new PersonagemService(token);
            _ = ObterClasses();

            SalvarCommand = new Command(async () =>
            {
                await SalvarPersonagem();
            });
            CancelarCadastroCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });
        }

        public ICommand CancelarCadastroCommand { get; set; }
        public ICommand SalvarCommand { get; }

        private int id;
        private string nome;
        private int  pontosVida;
        private int forca;
        private int defesa;
        private int inteligencia;
        private int disputas;
        private int vitorias;
        private int derrotas;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }

        }

        public string Nome
        {
            get => nome;
            set
            {
                nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        public int PontosVida
        {
            get => pontosVida;
            set
            {
                pontosVida = value;
                OnPropertyChanged(nameof(PontosVida));
            }
        }

        public int Forca
        {
            get => forca;
            set
            {
                forca = value;
                OnPropertyChanged(nameof(Forca));
            }
        }

        public int Defesa
        {
            get => defesa;
            set
            {
                defesa = value;
                OnPropertyChanged(nameof(Defesa));
            }
        }


        public int Inteligencia
        {
            get => inteligencia;
            set
            {
                inteligencia = value;
                OnPropertyChanged(nameof(Inteligencia));
            }
        }


        public int Disputas
        {
            get => disputas;
            set
            {
                disputas = value;
                OnPropertyChanged(nameof(Disputas));
            }
        }

        public int Vitorias
        {
            get => vitorias;
            set
            {
                vitorias = value;
                OnPropertyChanged(nameof(Vitorias));
            }
        }

        public int Derrotas
        {
            get => derrotas;
            set
            {
                derrotas = value;
                OnPropertyChanged(nameof(Derrotas));
            }
        }


        private ObservableCollection<TipoClasse> listaTiposClasses;

        public ObservableCollection<TipoClasse> ListaTiposClasses
        {
            get => listaTiposClasses;
            set
            {
                listaTiposClasses = value;
                OnPropertyChanged(nameof(ListaTiposClasses));
            }
        }

        public async Task ObterClasses()
        {
            try
            {
                listaTiposClasses = new ObservableCollection<TipoClasse>();
                ListaTiposClasses.Add(new TipoClasse { Id = 1, Descricao = "Cavaleiro" });
                ListaTiposClasses.Add(new TipoClasse { Id = 2, Descricao = "Mago" });
                ListaTiposClasses.Add(new TipoClasse { Id = 3, Descricao = "Clerigo" });
                OnPropertyChanged(nameof(ListaTiposClasses));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        private TipoClasse tipoClasseSelecionado;
        public TipoClasse TipoClasseSelecionado
        {
            get { return tipoClasseSelecionado; }
            set
            {
                if (value != null)
                { 
                tipoClasseSelecionado = value;
                OnPropertyChanged(nameof(TipoClasseSelecionado));
                }

            }
        }

        public async Task SalvarPersonagem()
        {
            try
            {
                Personagem personagem = new Personagem()
                {
                    Id = this.id,
                    Nome = this.nome,
                    PontosVida = this.pontosVida,
                    Forca = this.forca,
                    Defesa = this.defesa,
                    Inteligencia = this.inteligencia,
                    Disputas = this.disputas,
                    Vitorias = this.vitorias,
                    Derrotas = this.derrotas,
                    Classe = (ClasseEnum)tipoClasseSelecionado.Id


                };
                if (personagem.Id == 0)
                {
                    await pService.PostPersonagemAsync(personagem);
                    await App.Current.MainPage.DisplayAlert("Sucesso", "Personagem cadastrado com sucesso", "Ok");
                    await Shell.Current.GoToAsync("..");
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "Detalhes" + ex.InnerException, "Ok");
            }
        }
    }
}
