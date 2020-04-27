// CSD 228 - Assignment 7 Solution - Nat Ballou
// Extra Implementation by Devon Gronquist

using System;
using System.Collections.Generic;

namespace Employees
{
	// SupportPerson works a shift
    public enum ShiftName { One, Two, Three }

    [System.Serializable]
    public class SupportPerson : Employee
    {
        public ShiftName Shift { get; set; } = ShiftName.One;

        new static public string prop1Name => "Shift:";
        new static public object prop1DefaultValue => new List<string>() { ShiftName.One.ToString(), ShiftName.Two.ToString(), ShiftName.Three.ToString() };
        new static public IComparable prop1MinValue => ShiftName.One;
        new static public IComparable prop1MaxValue => ShiftName.Three;

        #region constructors 


        public SupportPerson() { }

		public SupportPerson(string firstName, string lastName, DateTime age, float currPay, 
                             string ssn, ShiftName shift)
          : base(firstName, lastName, age, currPay, ssn)
        {
            // This property is defined by the SupportPerson class.
            Shift = shift;
		}
		#endregion

		public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Shift: {0}", Shift);
		}

        public override void GetSpareProp(int propId, ref string name, ref object value)
        {
            switch (propId)
            {
                case 1:
                    name = prop1Name;
                    value = Shift;
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
                if (obj is int) return (ShiftName)obj;
                else if (obj is string)
                {
                    string s = (string)obj;
                    ShiftName value;

                    if (ShiftName.TryParse(s, out value)) return value;
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
