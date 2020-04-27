// CSD 228 - Assignment 7 Solution - Nat Ballou
// Extra Implementation by Devon Gronquist

using System;
using System.Collections;

namespace Employees
{
    // Salespeople need to know their number of sales.
    [System.Serializable]
    public class SalesPerson : Employee
    {
        new static public string prop1Name => "Sales:";
        new static public object prop1DefaultValue => 0;
        new static public IComparable prop1MinValue => 0;
        new static public IComparable prop1MaxValue => 5000;
        #region constructors 
        public SalesPerson() { }

        // As a general rule, all subclasses should explicitly call an appropriate
        // base class constructor.
        public SalesPerson(string firstName, string lastName, DateTime age, float currPay, 
                           string ssn, int numbOfSales)
          : base(firstName, lastName, age, currPay, ssn)
        {
            // This belongs with us!
            SalesNumber = numbOfSales;
        }
        #endregion

        public int SalesNumber { get; set; }

        public override void GetSpareProp(int propId, ref string name, ref object value)
        {
            switch (propId)
            {
                case 1:
                    name = prop1Name;
                    value = string.Format("{0:N0}", SalesNumber);
                    break;
                default:
                    base.GetSpareProp(propId, ref name, ref value);
                    break;
            }
        }

        // A salesperson's bonus is influenced by the number of sales.
        public override sealed void GiveBonus(float amount)
        {
            int salesBonus = 0;
            if (SalesNumber >= 0 && SalesNumber <= 100)
                salesBonus = 10;
            else
            {
                if (SalesNumber >= 101 && SalesNumber <= 200)
                    salesBonus = 15;
                else
                    salesBonus = 20;
            }
            base.GiveBonus(amount * salesBonus);
        }

        // A SalesPerson gets an extra 300 on promotion
        public override sealed void GivePromotion()
        {
            base.GivePromotion();
            GiveBonus(300);
        }

        public override void DisplayStats()
        {
            base.DisplayStats();
            Console.WriteLine("Number of sales: {0:N0}", SalesNumber);
        }

        public static object SpareAddPropConvert(object obj, int id)
        {
            if (id == 1)
            {
                if (obj is int) return obj;
                else if (obj is string)
                {
                    string s = (string)obj;
                    int value;

                    if (int.TryParse(s, out value)) return value;
                }
            }

            // Not a valid value
            return null;
        }
    }
}
