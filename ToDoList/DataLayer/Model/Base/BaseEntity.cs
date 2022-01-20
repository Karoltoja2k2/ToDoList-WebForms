using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DataLayer.Model.Base
{
    [Serializable]
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}