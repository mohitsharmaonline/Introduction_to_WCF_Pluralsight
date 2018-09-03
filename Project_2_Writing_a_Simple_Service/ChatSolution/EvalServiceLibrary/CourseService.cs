using System.Collections.Generic;
using System.ServiceModel;

namespace EvalServiceLibrary
{
    public interface ICourseService
    {
        [OperationContract]
        List<string> GetCourseList();
    }

    public class CourseServic : ICourseService
    {
        public List<string> GetCourseList()
        {
            List<string> courses = new List<string>();
            courses.Add("WCF Fundamentals");
            courses.Add("WF Fundamentals");
            courses.Add("WPF Fundamentals");
            courses.Add("Silverlight Fundamentals");
            return courses;
        }
    }
}
