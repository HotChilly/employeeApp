// CSD228 - Assignment9 starter code - Nat Ballou
using System;

namespace Employees
{
    [Serializable]
    public enum ExpenseCategory { Conference, Lodging, Meals, Misc, Travel }

    [Serializable]
    public class Expense
    {
        #region Data members / properties
        public DateTime Date { get; set; } = DateTime.Today;
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        #endregion

        #region Constructors
        public Expense() { }

        public Expense(DateTime expDate, ExpenseCategory category, string description, double amount)
        {
            Date = expDate;
            Category = category;
            Description = description;
            Amount = amount;
        }
        #endregion
    }
}
