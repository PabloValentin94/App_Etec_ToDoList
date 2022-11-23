using System;
using System.Collections.Generic;
using System.Text;

namespace App_Etec_ToDoList.Model
{

    public class Tarefa
    {

        [SQLite.PrimaryKey, SQLite.AutoIncrement] // Configurações da variável ID.
        public int id_tarefa { get; set; }

        public string nome_tarefa { get; set; }

        public string descricao_tarefa { get; set; }

        public string materia_tarefa { get; set; }

        public DateTime data_conclusao_tarefa { get; set; }

    }

}
