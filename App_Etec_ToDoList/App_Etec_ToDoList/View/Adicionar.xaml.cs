using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Etec_ToDoList.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adicionar : ContentPage
    {

        public Adicionar()
        {

            InitializeComponent();

        }

        private async void btn_salvar_Clicked(object sender, EventArgs e)
        {

            try
            {

                Model.Tarefa registro = new Model.Tarefa()
                {

                    nome_tarefa = txt_nome.Text,

                    descricao_tarefa = txt_descricao.Text,

                    materia_tarefa = txt_materia.Text,

                    data_conclusao_tarefa = dtpck_data_conclusao.Date

                };

                await App.gerenciador_acesso.Insert(registro);

                await DisplayAlert("Operação concluída!", "Tarefa criada com sucesso.", "OK");

                await Navigation.PopAsync();

            }

            catch(Exception ex)
            {

                await DisplayAlert("Erro!", ex.Message, "OK");

            }

        }

    }

}