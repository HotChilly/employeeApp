// CSD 228 - Assignment 7 Solution - Nat Ballou
// Extra Implementation by Devon Gronquist

using System;
using System.Collections.Generic;

namespace Employees
{
    abstract public partial class Employee 
    {
        public static int NamespaceLength = 10;


        #region Nested benefit packages
        [Serializable]
        public class BenefitPackage
        {
            // Assume we have other members that represent
            // dental/health benefits, and so on.
            public virtual double ComputePayDeduction() { return 125.0; }
            public override string ToString() { return "Standard"; }
        }

        // Other benefit packages derive from BenefitPackage directly
        // and provide definitions for ComputePayDeduction and ToString
        [System.Serializable]
        sealed public class GoldBenefitPackage : BenefitPackage
        {
            public override double ComputePayDeduction() { return 150.0; }
            public override string ToString() { return "Gold"; }
        }

        [System.Serializable]
        sealed public class PlatinumBenefitPackage : BenefitPackage
        {
            public override double ComputePayDeduction() { return 250.0; }
            public override string ToString() { return "Platinum"; }
        }

        // Contain a BenefitPackage object.
        protected BenefitPackage empBenefits = new BenefitPackage();

        // Expose certain benefit behaviors of object.
        public double GetBenefitCost()
        { return empBenefits.ComputePayDeduction(); }

        // Expose object through a read-only property.
        public BenefitPackage Benefits
        {
            get { return empBenefits; }
        }
        #endregion

        #region Class methods 


        public virtual void GiveBonus(float amount)
        { currPay += amount; }

        // Move GivePromotion as virtual method on Employee
        public virtual void GivePromotion()
        {
            // Bump benefit package from Standard to Gold, Gold to Platinum
            if (empBenefits.GetType() == typeof(BenefitPackage))
                empBenefits = new GoldBenefitPackage();
            else if (empBenefits is GoldBenefitPackage)
                empBenefits = new PlatinumBenefitPackage();
        }

        public virtual void DisplayStats()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Role: {0}", GetType().ToString().Substring(10));
            Console.WriteLine("ID: {0}", Id);
            Console.WriteLine("Age: {0}", Age);
            Console.WriteLine("Pay: {0:C}", Pay);
            Console.WriteLine("SSN: {0}", SocialSecurityNumber);
        }

        /// <summary>
        /// Gets the name and value of the specified property.
        /// </summary>
        /// <param name="name">Returned name of the property</param>
        /// <param name="value">Returned value of the property</param>
        /// <param name="propId">given id of the property to look for. Starts at 1 and increments from there.</param>
        public virtual void GetSpareProp(int propId, ref string name, ref object value)
        {
            throw new ArgumentOutOfRangeException("Prop ID must be within the bounds of the type's listed properties.");
        }

        #endregion

        #region Employee sort oders
        // Sort employees by name.
        private class NameComparer : IComparer<Employee>
		{
            // Compare the name of each object.
            int IComparer<Employee>.Compare(Employee e1, Employee e2)
            {
				if (e1 != null && e2 != null)
					return String.Compare(e1.Name, e2.Name);
				else throw new ArgumentException("Parameter is not an Employee!");
			}
		}

		// Sort by age
		private class AgeComparer : IComparer<Employee>
		{
			// Compare the Age of each object.
			int IComparer<Employee>.Compare(Employee e1, Employee e2)
			{
				if (e1 != null && e2 != null)
					return e1.Age.CompareTo(e2.Age);
				else throw new ArgumentException("Parameter is not an Employee!");
			}
		}
		
        // Sort By pay
		private class PayComparer : IComparer<Employee>
		{
			// Compare the Pay of each object.
			int IComparer<Employee>.Compare(Employee e1, Employee e2)
			{
				if (e1 != null && e2 != null)
					return e1.Pay.CompareTo(e2.Pay);
				else
					throw new ArgumentException("Parameter is not an Employee!");
			}
		}

		// Static, read-only properties to return Employee Name, Age, or Pay comparers
		public static IComparer<Employee> SortByName { get; } = new NameComparer();
		public static IComparer<Employee> SortByAge { get; } = new AgeComparer();
		public static IComparer<Employee> SortByPay { get; } = new PayComparer();
        #endregion



        

        // Check if passed object is a valid value for spare prop1
        // Return error string if not valid, else return String.Empty (if valid)
        public static string SpareAddPropValid(object obj, IComparable minVal, IComparable maxVal)
        {

            if (obj is string)
            {
                string s = (string)obj;
                int value;

                if (int.TryParse(s, out value) && value.CompareTo(minVal) >= 0 && value.CompareTo(maxVal) <= 0)
                    return String.Empty;
            }

            // Error message for invalid values
            return String.Format("Range is {0:N0} to {1:N0}", minVal, maxVal);
        }



    }
}