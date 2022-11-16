using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace App_Etec_ToDoList.Model
{

    public class Tarefa
    {

        [PrimaryKey, AutoIncrement] // Definindo as propriedades da ID.
        public string marcador { get; set; }

        public int id { get; set; }

        public string descricao { get; set; }

        public DateTime data_conclusao { get; set; }

    }

}
