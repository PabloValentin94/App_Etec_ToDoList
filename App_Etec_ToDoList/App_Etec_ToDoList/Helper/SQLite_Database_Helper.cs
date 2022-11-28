using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using App_Etec_ToDoList.Model;
using SQLite;
using System.Threading.Tasks; /* Threads permitem que o aplicativo execute tarefas assíncronas/parelelas,
                               * ou seja, que ocorrem ao mesmo tempo que outras. */

namespace App_Etec_ToDoList.Helper
{

    public class SQLite_Database_Helper
    {

        /* connection: conexão.
         * path: caminho. */

        // Criando uma variável de conexão com o arquivo do database, do tipo somente leitura.

        readonly SQLiteAsyncConnection _connection;

        // Não é necessário criar um arquivo para o database, pois ele será gerado automaticamente.

        public SQLite_Database_Helper(string path)
        {

            // Criando uma conexão com o arquivo do database.

            _connection = new SQLiteAsyncConnection(path);

            /* No comando abaixo, a palavra Tarefa refere-se a um tipo, não a um nome. Não confunda.
             * Esse comando cria uma tabela, e, o método Wait, faz com que o sistema espere a tabela ser criada
             * para somente depois permitir o uso dos outros métodos da classe. */

            _connection.CreateTableAsync<Tarefa>().Wait();

        }

        public Task<int> Insert(Tarefa registro)
        {

            // Inserindo um registro.

            return _connection.InsertAsync(registro);

        }

        public Task<List<Tarefa>> Update(Tarefa registro)
        {

            // Criando uma variável que será um parâmetro à Query.

            string sql = "UPDATE Tarefa SET nome_tarefa = ?, descricao_tarefa = ?, materia_tarefa = ?, " +
                         "data_conclusao_tarefa = ? WHERE id_tarefa = ?";

            // Executando uma Query/operação.

            return _connection.QueryAsync<Tarefa>(sql, registro.nome_tarefa, registro.descricao_tarefa, registro.materia_tarefa, 
                                           registro.data_conclusao_tarefa, registro.id_tarefa);

        }

        public Task<int> Delete(int id)
        {

            /* Apagando todos os registros que tiverem a ID igual ao parâmetro do método Delete (Para fazer isso,
             * usamos uma função Lambda.). */

            return _connection.Table<Tarefa>().DeleteAsync(i => i.id_tarefa == id);

        }

        public Task<List<Tarefa>> Select()
        {

            // Obtendo todas os registros existentes da tabela Tarefa.

            return _connection.Table<Tarefa>().ToListAsync();

        }

        public Task<List<Tarefa>> SelectByName(string pesquisa)
        {

            // Obtendo um registro específico da tabela Tarefa.

            string sql = "SELECT * FROM Tarefa WHERE nome_tarefa LIKE '%" + pesquisa + "%'";

            return _connection.QueryAsync<Tarefa>(sql);

        }

    }

}
