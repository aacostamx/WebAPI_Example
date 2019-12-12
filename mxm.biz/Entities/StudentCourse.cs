using System;
using System.Collections.Generic;

namespace mxm.biz.Entities
{
    public partial class StudentCourse
    {
        public StudentCourse()
        {
            StudentContents = new HashSet<StudentContent>();
            StudentMatters = new HashSet<StudentMatter>();
            StudentSubTopics = new HashSet<StudentSubTopic>();
            StudentTopics = new HashSet<StudentTopic>();
        }

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double Progress { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
        public ICollection<StudentContent> StudentContents { get; set; }
        public ICollection<StudentMatter> StudentMatters { get; set; }
        public ICollection<StudentSubTopic> StudentSubTopics { get; set; }
        public ICollection<StudentTopic> StudentTopics { get; set; }
    }
}
