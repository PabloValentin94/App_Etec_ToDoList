using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using System.Threading.Tasks;
using SQLite;

using App_Etec_ToDoList.Model;

namespace App_Etec_ToDoList.Helper
{

    public class SQLite_Database_Helper
    {

        SQLiteAsyncConnection conexao; // Criando uma variável de "conexao" com o SQLite.

        // Método construtor.

        public SQLite_Database_Helper(string caminho_arquivo)
        {

            // Criando uma "conexao" com o SQLite.

            conexao = new SQLiteAsyncConnection(caminho_arquivo);

            // Criando a tabela Tarefa.

            conexao.CreateTableAsync<Tarefa>().Wait();

        }

        public Task<int> Insert(Tarefa registro)
        {

            // Adicionando um registro ao banco de dados.

            return conexao.InsertAsync(registro);

        }

        public void Update(Tarefa registro)
        {

            string sql = "UPDATE Tarefa SET marcador = ?, descricao = ?, data_conclusao = ? WHERE id = ?";

            // Executando uma query (consulta.).

            conexao.QueryAsync<Tarefa>(sql, registro.marcador, registro.descricao, registro.data_conclusao, registro.id).Wait();

        }

        public Task<int> Delete(int id)
        {

            // Removendo um registro do banco de dados.

            return conexao.Table<Tarefa>().DeleteAsync(i => i.id == id);

        }

        public Task<List<Tarefa>> Select()
        {

            // Pegando todos os registros existentes dentro da tabela.

            return conexao.Table<Tarefa>().ToListAsync();

        }

        /*public Task<Tarefa> SelectById(int id)
        {

            return new Tarefa();

        }*/

    }

}
