using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorExpenseTracker.Model
{
    public enum ExpenseType
    {
        Income,
        Expense
    }
    public static class TypeOfExpense
    {
        public static string SourceIcon(ExpenseType etype)
        {
            return DicIcon[etype].ToString();
        }

        private static Dictionary<ExpenseType, string> DicIcon = new Dictionary<ExpenseType, string>()
        {
            {ExpenseType.Expense, "minus_green.png"}, {ExpenseType.Income, "plus_green.png"}
        };
   }

}
