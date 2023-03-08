using BlazorExpenseTracker.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorExpenseTracker.Model
{
    public class Expense : IValidatableObject
    {
        private int DaysInTheFuture = 30;
        private DateTime date;
        public int Id { get; set; }
        //[Required]
        //[Range(1, double.MaxValue, ErrorMessage ="Amount needs to be greater than 0")]
        public decimal Amount { get; set; }
        [Required]
        public string CategoryId { get; set; }
        public Categories Category { get; set; }
        [Required]
        //[ExpenseTransactionDateValidator(DaysInTheFuture = 30)]
        public DateTime TransactionDate { get; set; }
        public ExpenseType ExpenseType { get; set; }

        public event Action OnSelectedExpenseChanged;
        //Este evento es llamado desde una grilla (ExpenseHistory). Luego se asigna a la clase dichos valores
        public void SelectedExpenseChanged(Expense expense)
        {
            Id = expense.Id;
            TransactionDate = expense.TransactionDate;
            Amount = expense.Amount;
            ExpenseType = expense.ExpenseType;
            CategoryId = expense.CategoryId;
            //notificar a todos los componentes suscritos a este evento que hay cambios en la clase
            NotifySelectedExpenseChanged();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (DateTime.TryParse(TransactionDate.ToString(), out date))
            {
                if (date == DateTime.MinValue)
                {
                    errors.Add(new ValidationResult($"Date shouldn't be empty.",
                        new[] { nameof(TransactionDate) }));
                }
                else if (TransactionDate > DateTime.Now.AddDays(DaysInTheFuture))
                {
                    errors.Add(new ValidationResult($"Date can't be greater than today plus {DaysInTheFuture}",
                        new[] { nameof(TransactionDate) }));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Invalid date.", new[] { nameof(TransactionDate) }));
            }
            
            if (ExpenseType == ExpenseType.Income && Amount < 0)
                errors.Add(new ValidationResult("Income can't be lesser than zero.", new[] { nameof(Amount) }));
            else 
                if (ExpenseType == ExpenseType.Expense && Amount > 0)
                    errors.Add(new ValidationResult("Expense can't be greater than zero.", new[] { nameof(Amount) }));
            return errors;
        }

        private void NotifySelectedExpenseChanged()
        {            
            OnSelectedExpenseChanged.Invoke();
        }
    }
}
