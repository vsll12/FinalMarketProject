using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSide.Models
{
    internal class ReportElement
    {
        public DateTime Date {  get; set; }
        public string? ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public ReportElement() { }

        public ReportElement(DateTime date, string? productName, int productQuantity)
        {
            Date = date;
            ProductName = productName;
            ProductQuantity = productQuantity;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Date.ToString("dd.MM.yyyy")} {ProductName}  {ProductQuantity}");
        }
    }
}
