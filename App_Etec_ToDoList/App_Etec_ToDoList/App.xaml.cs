using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App_Etec_ToDoList.Helper;
using System.IO; // Biblioteca que permite o uso do Path.Combine().

namespace App_Etec_ToDoList
{

    public partial class App : Application
    {

        /* Definimos a variável abaixo como estática, pois ela única, ou seja, é uma variável (Do banco de dados.)
         * para toda a aplicação, e, se ela é única, não precisa ser instanciada toda vez que for usada, portanto,
         * ela é estática. */

        static SQLite_Database_Helper arquivo_database;

        public static SQLite_Database_Helper gerenciador_acesso
        {

            get
            {

                // Condição que será acionada caso o arquivo do banco de dados ainda não exista.

                if(arquivo_database == null)
                {

                    string caminho_arquivo = Path.Combine(

                            // Especificando o caminho do diretório em que o arquivo pode ser gerado.

                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),

                            // Especificando o nome que o arquivo terá.

                            "database.db3"

                        );

                    // Acionando o método construtor da classe SQLite_Database_Helper.

                    arquivo_database = new SQLite_Database_Helper(caminho_arquivo);

                }

                return arquivo_database;

            }

        }

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
