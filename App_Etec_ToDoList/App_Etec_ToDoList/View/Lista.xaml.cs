using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App_Etec_ToDoList.Model;
using System.Collections.ObjectModel;

namespace App_Etec_ToDoList.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {

        /* Uma ObservableCollection é como uma List<>, mas, que atualiza a interface gráfica automáticamente
         * após um item ser adicionado nela (É como o DataGridView do Windows.Forms). */

        ObservableCollection<Tarefa> lista_tarefas = new ObservableCollection<Tarefa>();

        public Lista()
        {

            InitializeComponent();

        }

        private async void btn_adicionar_Clicked(object sender, EventArgs e)
        {

            try
            {

                await Navigation.PushAsync(new Adicionar());

            }

            catch(Exception ex)
            {

                await DisplayAlert("Erro!", ex.Message, "OK");

            }

        }

        private async void btn_total_tarefas_Clicked(object sender, EventArgs e)
        {

            try
            {

                await DisplayAlert("Tarefas Pendentes:", this.lista_tarefas.Count + " precisa(m) ser feita(s).", "OK");

            }

            catch(Exception ex)
            {

                await DisplayAlert("Erro!", ex.Message, "OK");

            }

        }

        /* O método abaixo é basicamente um evento de loading da página. Tudo que quisermos que aconteça assim
         * que a página for carregada, devemos colocar dentro desse método. */

        protected override void OnAppearing()
        {

            //base.OnAppearing();

            /* Criando uma tarefa que irá agir paralelamente ao loading da página. Essa tarefa será responsável
             * por pegar todos os registros de tarefas que existam no arquivo do banco de dados. */

            if(this.lista_tarefas.Count == 0)
            {

                System.Threading.Tasks.Task.Run(async () =>
                {

                    List<Tarefa> lista_temporaria = await App.gerenciador_acesso.Select();

                    foreach (Tarefa item in lista_temporaria)
                    {

                        this.lista_tarefas.Add(item);

                    }

                    refv_carregamento.IsRefreshing = false;

                });

                lstv_registros.ItemsSource = this.lista_tarefas;

            }

        }

        private async void btn_deletar_Clicked(object sender, EventArgs e)
        {

            MenuItem disparador = (MenuItem)sender;

            Tarefa tarefa_selecionada = (Tarefa)disparador.BindingContext;

            bool condicao = await DisplayAlert("Atenção!", "Deseja mesmo apagar a tarefa selecionada?",
                                               "Confirmar", "Cancelar");

            if(condicao)
            {

                await App.gerenciador_acesso.Delete(tarefa_selecionada.id_tarefa);

                this.lista_tarefas.Remove(tarefa_selecionada);

            }

        }

        private void srcbar_tarefas_TextChanged(object sender, TextChangedEventArgs e)
        {

            // Criando uma variável para a barra de pesquisa.

            string pesquisa = e.NewTextValue;

            //Console.WriteLine(pesquisa);

            /* Criando uma tarefa que irá agir paralelamente ao loading da página. Essa tarefa será responsável
             * por pegar um registro de tarefa específico no arquivo do banco de dados. */

            if (this.lista_tarefas.Count > 0)
            {

                System.Threading.Tasks.Task.Run(async () =>
                {

                    List<Tarefa> lista_temporaria = await App.gerenciador_acesso.SelectByName(pesquisa);

                    /* Limpando a lista de registros, para somente depois usa-lá. Fazemos isso porque a variável
                     * "lista_tarefas" é pública, o que fará, com que, além dos registros encontrados na pesquisa,
                     * apareçam os que a variável já possuir dentro de si, podendo causar duplicações na interface. */

                    this.lista_tarefas.Clear();

                    foreach(Tarefa item in lista_temporaria)
                    {

                        this.lista_tarefas.Add(item);

                    }

                    refv_carregamento.IsRefreshing = false;

                });

                lstv_registros.ItemsSource = this.lista_tarefas;

            }

        }

        private async void lstv_registros_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Tarefa tarefa_selecionada = (Tarefa)e.SelectedItem;

            View.Editar pagina_update = new View.Editar()
            {

                BindingContext = tarefa_selecionada

            };

            await Navigation.PushAsync(pagina_update);

        }

    }

}