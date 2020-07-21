using ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TodoItem
        : BaseEntity
    {
        //public string Title { get; set; } = string.Empty;
        //public string Description { get; set; }
        //public bool IsDone { get; private set; }

        //public int ListId { get; set; }

        //public void MarkComplete()
        //{
        //    IsDone = true;

        //    //Events.Add(new ToDoItemCompletedEvent(this));
        //}

        //public override string ToString()
        //{
        //    string status = IsDone ? "Done!" : "Not done.";
        //    return status;
        //    //return $"{Id}: Status: {status} - {Title} - {Description}";
        //}

        public int ListId { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public bool Done { get; set; }

        public DateTime? Reminder { get; set; }


        public TodoList List { get; set; }
    }
}
