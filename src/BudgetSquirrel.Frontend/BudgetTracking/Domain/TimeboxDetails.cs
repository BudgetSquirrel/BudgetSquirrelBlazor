using System;

namespace BudgetSquirrel.Frontend.BudgetTracking.Domain
{
  public partial class BudgetTrackingContext
  {
    public class TimeboxDetails
    {
      public int Id { get; private set; }
      
      public DateTime StartDate { get; private set; }

      public DateTime EndDate { get; private set; }

      public TimeboxDetails(int id, DateTime startDate, DateTime endDate)
      {
        this.Id = id;
        this.StartDate = startDate;
        this.EndDate = endDate;
      }
    }
  }
}