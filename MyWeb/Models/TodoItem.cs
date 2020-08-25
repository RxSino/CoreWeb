using System;
using System.ComponentModel.DataAnnotations;

namespace MyWeb.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }

        public string Secret { get; set; }
    }
}
