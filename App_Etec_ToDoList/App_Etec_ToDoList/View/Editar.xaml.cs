using App_Etec_ToDoList.Model;
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
    public partial class Editar : ContentPage
    {

        public Editar()
        {

            InitializeComponent();

        }

        private async void btn_salvar_Clicked(object sender, EventArgs e)
        {

            try
            {

                Tarefa tarefa_atual = BindingContext as Tarefa;

                Model.Tarefa registro = new Model.Tarefa()
                {

                    //id_tarefa = ((Tarefa) BindingContext).id_tarefa,

                    id_tarefa = tarefa_atual.id_tarefa,

                    nome_tarefa = txt_nome.Text,

                    descricao_tarefa = txt_descricao.Text,

                    materia_tarefa = txt_materia.Text,

                    data_conclusao_tarefa = dtpck_data_conclusao.Date

                };

                await App.gerenciador_acesso.Update(registro);

                await DisplayAlert("Operação concluída!", "Tarefa atualizada com sucesso.", "OK");

                await Navigation.PushAsync(new View.Lista());

            }

            catch (Exception ex)
            {

                await DisplayAlert("Erro!", ex.Message, "OK");

            }

        }

    }

}