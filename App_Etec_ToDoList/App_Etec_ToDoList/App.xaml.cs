using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Helper.SQLite_Database_Helper;

using System.IO;

namespace App_Etec_ToDoList
{

    static SQLite_Database_Helper database_connection;

    public static SQLite_Database_Helper database_access_manager
    {

        get
        {

            if(this.database_connection == null)
            {

                // Criando uma variável de caminho ao Banco de Dados.

                string caminho_arquivo = Path.Combine(

                    // Especificando onde o caminho pode ser gravado/encontrado.

                    Environment.GetFolderPath(Environment.SpecialFolder.localApplicationData),

                    // Dando um nome para o arquivo do Banco de Dados.

                    "db_tarefa.db3"

                );

                this.database_connection = new SQLite_Database_Helper(caminho_arquivo);

            }

            return this.database_connection;

        }

    }

    public partial class App : Application
    {

        public App()
        {

            InitializeComponent();

            MainPage = new NavigationPage(new View.Lista());

        }

        protected override void OnStart()
        {



        }

        protected override void OnSleep()
        {



        }

        protected override void OnResume()
        {



        }

    }

}
