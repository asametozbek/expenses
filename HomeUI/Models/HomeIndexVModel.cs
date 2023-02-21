using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeUI.Models
{
    public class HomeIndexVModel
    {
        public IEnumerable<Expense> Expenses { get; set; }

        public double TodayExpense { get; set; }
        public double WeekedExpense { get; set; }
        public double MonthlyExpense { get; set; }

        public List<UserExpense> userExpenses { get; set; }

    }

    public class UserExpense
    {
        public string User { get; set; }
        public double Amount  { get; set; }
    }
}
