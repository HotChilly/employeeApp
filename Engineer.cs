// CSD 228 - Assignment 7 Solution - Nat Ballou
// Extra Implementation by Devon Gronquist

using System;
using System.Collections.Generic;

namespace Employees
{
	// Engineers have degrees
    public enum DegreeName { BS, MS, PhD }

    [System.Serializable]
    public class Engineer : Employee
    {

        public DegreeName HighestDegree { get; set; } = DegreeName.BS;

        new static public string prop1Name => "Degree:";
        new static public object prop1DefaultValue => new List<string>() { DegreeName.BS.ToString(), DegreeName.MS.ToString(), DegreeName.PhD.ToString() };
        new static public IComparable prop1MinValue => DegreeName.BS;
        new static public IComparable prop1MaxValue => DegreeName.PhD;

        #region constructors 
        public Engineer() { }

		public Engineer(string firstName, string lastName, DateTime age, float currPay, string ssn, 
                        DegreeName degree)
          : base(firstName, lastName, age, currPay, ssn)
        {
            // This property is defined by the Engineer class.
            HighestDegree = degree;
		}
		#endregion

		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Highest Degree: {0}", HighestDegree);

		}

        public override void GetSpareProp(int propId, ref string name, ref object value)
        {
            switch (propId)
            {
                case 1:
                    name = prop1Name;
                    value = HighestDegree;
                    break;
                default:
                    base.GetSpareProp(propId, ref name, ref value);
                    break;
            }
        }
        public static object SpareAddPropConvert(object obj, int id)
        {
            if (id == 1)
            {
                if (obj is int) return (DegreeName)obj;
                else if (obj is string)
                {
                    string s = (string)obj;
                    DegreeName value;

                    if (DegreeName.TryParse(s, out value)) return value;
                }
            }

            // Not a valid value
            return null;
        }

        public override void GivePromotion()
        {
            base.GivePromotion();
            this.GiveBonus(300);
        }

    }
}
