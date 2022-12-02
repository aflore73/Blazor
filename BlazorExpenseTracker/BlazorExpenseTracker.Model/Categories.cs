using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorExpenseTracker.Model
{
    public class Categories
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required.")]
        public string? Name { get; set; }

    }
}
