using System;

namespace BaiTap54
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public string DepartmentCode { get; set; }

        public Employee(string id, string name, string position, DateTime startDate, string deptCode)
        {
            EmployeeId = id;
            FullName = name;
            Position = position;
            StartDate = startDate;
            DepartmentCode = deptCode;
        }
    }
}
