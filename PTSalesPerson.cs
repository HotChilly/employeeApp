// CSD 228 - Assignment 7 Solution - Nat Ballou
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    [System.Serializable]
    public sealed class PTSalesPerson : SalesPerson
    {
        new static public string prop1Name => "Sales:";
        new static public object prop1DefaultValue => 0;
        new static public IComparable prop1MinValue => 0;
        new static public IComparable prop1MaxValue => 5000;

        public PTSalesPerson(string firstName, string lastName, DateTime age, float currPay, 
                             string ssn, int numbOfSales)
          : base(firstName, lastName, age, currPay, ssn, numbOfSales)
        {
        }
        // Assume other members here...

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
