// CSD 228 - Assignment 7 Solution - Nat Ballou
// Extra Implementation by Devon Gronquist

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Employees
{
    [Serializable]
    public partial class Employee 
    {
        // Field data.
        private int empID;
        private float currPay;
        private DateTime empDOB;
        private string empSSN;
        public List<Expense> Expenses { get; } = new List<Expense>();

        public static string prop1Name { get { return null; } }
        public static object prop1DefaultValue { get { return null; } }
        public static IComparable prop1MaxValue { get { return null; } }
        public static IComparable prop1MinValue { get { return null; } }

        public static string prop2Name { get { return null; } }
        public static object prop2DefaultValue { get { return null; } }
        public static IComparable prop2MaxValue { get { return null; } }
        public static IComparable prop2MinValue { get { return null; } }

        public static string prop3Name { get { return null; } }
        public static object prop3DefaultValue { get { return null; } }
        public static IComparable prop3MaxValue { get { return null; } }
        public static IComparable prop3MinValue { get { return null; } }

        public static string prop4Name { get { return null; } }
        public static object prop4DefaultValue { get { return null; } }
        public static IComparable prop4MaxValue { get { return null; } }
        public static IComparable prop4MinValue { get { return null; } }

        // Used to create unique IDs.
        static int NextId = 1;

        #region Constructors
        // Default constructor handles Employee ID
        public Employee()
        {
            // Catch Employee ID overflow
            if (NextId == Int32.MaxValue)
                throw new System.OverflowException("Employee ID has reached max value");

            // Assign Employee ID
            empID = NextId++;
        }

        public Employee(string firstName, string lastName, DateTime birthday, float pay, string ssn)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
            empDOB = birthday;
            currPay = pay;
            empSSN = ssn;
        }
        #endregion

        #region Properties 
        public string FirstName { get; }
        public string LastName { get; }
        public string Name { get { return FirstName + " " + LastName; } }
        public int Id { get { return empID; } }
        public float Pay { get { return currPay; } }
        public int Age { get { return (DateTime.Now.Year - empDOB.Year); } }
        public DateTime DateOfBirth { get { return empDOB; } }
        public string SocialSecurityNumber { get { return empSSN; } }
        public virtual string Role { get { return GetType().ToString().Substring(10); } }

        #endregion

        #region Serialization customization for NextId
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // Called when the deserialization process is complete.
            if (empID >= NextId)
            {
                // Catch Employee ID overflow
                if (empID == Int32.MaxValue)
                    throw new System.OverflowException("Employee ID has reached max value");

                NextId = empID + 1;
            }
        }
        #endregion

    }
}
