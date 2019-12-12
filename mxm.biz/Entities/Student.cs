using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class Student
    {
        public Student()
        {
            Connections = new HashSet<Connection>();
            Documents = new HashSet<Document>();
            EvaluationStudentCourses = new HashSet<EvaluationStudentCourse>();
            EvaluationStudentMatters = new HashSet<EvaluationStudentMatter>();
            Notifications = new HashSet<Notification>();
            Quotes = new HashSet<Quote>();
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Curp { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public int DistrictId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime? Activated { get; set; }
        public bool Active { get; set; }

        public Company Company { get; set; }
        public District District { get; set; }
        public User User { get; set; }
        public ICollection<Connection> Connections { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<EvaluationStudentCourse> EvaluationStudentCourses { get; set; }
        public ICollection<EvaluationStudentMatter> EvaluationStudentMatters { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Quote> Quotes { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
