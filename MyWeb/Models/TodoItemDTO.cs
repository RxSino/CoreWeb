using System;
using System.ComponentModel.DataAnnotations;

namespace MyWeb.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "名字不能为空")]
        public string Name { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        public string OtherName { get; set; }

    }
}
