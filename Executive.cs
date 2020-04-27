// CSD 228 - Assignment 7 Solution - Nat Ballou
using System;
using System.Collections.Generic;

namespace Employees
{
    // Executives have titles
    public enum ExecTitle { CEO, CTO, CFO, VP }

    [System.Serializable]
    public class Executive : Manager
    {
		#region constructors 
		// Executives start with Gold benefits and 10,000 stock options
		public Executive() : base()
        {
            empBenefits = new GoldBenefitPackage();
            StockOptions = 10000;
        }

		public Executive(string firstName, string lastName, DateTime age, float currPay, 
                         string ssn, int numbOfOpts = 10000, ExecTitle title = ExecTitle.VP)
          : base(firstName, lastName, age, currPay, ssn, numbOfOpts)
        {
			// Title defined by the Executive class.
			Title = title;
            empBenefits = new GoldBenefitPackage();
		}
        #endregion

        public ExecTitle Title { get; set; } = ExecTitle.VP;

        public override string Role { get { return base.Role + ", " + Title; } }

        new static public string prop1Name => "Stock Options:";
        new static public object prop1DefaultValue => 500;
        new static public IComparable prop1MinValue => 0;
        new static public IComparable prop1MaxValue => 10000;
        new static public string prop2Name => "Title:";
        new static public object prop2DefaultValue => new List<string>() { ExecTitle.CEO.ToString(), ExecTitle.CFO.ToString(), ExecTitle.CTO.ToString(), ExecTitle.VP.ToString() };
        new static public IComparable prop2MinValue => ExecTitle.CEO;
        new static public IComparable prop2MaxValue => ExecTitle.VP;

        public override void DisplayStats()
		{
			base.DisplayStats();
			Console.WriteLine("Executive Title: {0}", Title);
		}

        // Executives get an extra 1000 bonus and 10,000 stock options on promotion
        // Move GivePromotion
        public override void GivePromotion()
        {
            base.GivePromotion();
            GiveBonus(1000);
            StockOptions += 10000;
        }

		// Methods for adding reports
		public override void AddReport(Employee newReport)
        {
            // Check for proper report to Executive
            if (newReport is Manager || newReport is SalesPerson)
            {
                base.AddReport(newReport);
            }
            else if (newReport != null)
            {
                // Raise exception for report that is not a Manager or Salesperson
                Exception ex = new AddReportException("Executive report not a Manager or Salesperson");

                // Add Manager custom data dictionary
                ex.Data.Add("Manager", this.Name);

                // Add report that failed to be added, and throw exception
                ex.Data.Add("New Report", newReport.Name);
                throw ex;
            }            
        }

        new public static object SpareAddPropConvert(object obj, int id)
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
            if (id == 2)
            {
                if (obj is int) return (ExecTitle)obj;
                else if (obj is string)
                {
                    string s = (string)obj;
                    ExecTitle value;

                    if (ExecTitle.TryParse(s, out value)) return value;
                }
            }

            // Not a valid value
            return null;
        }

    }
}