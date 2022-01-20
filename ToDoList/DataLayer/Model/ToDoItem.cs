using System;
using System.ComponentModel.DataAnnotations;
using ToDoList.DataLayer.Model.Base;

namespace ToDoList.DataLayer.Model
{
    [Serializable]
    public class ToDoItem : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        
        [MaxLength(4096)]
        public string Description { get; set; }

        [Required]
        public bool IsDone { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int AssignedTo { get; set; }
    }
}