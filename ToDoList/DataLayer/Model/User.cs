using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.DataLayer.Model.Base;

namespace ToDoList.DataLayer.Model
{
    [Serializable]
    public class User : BaseEntity
    {
        [Required, MaxLength(128)]
        [Index("idx_Name", IsUnique = true)]
        public string Name { get; set; }

        [ForeignKey("AssignedTo")]
        public IList<ToDoItem> ToDoItems { get; set; }
    }
}