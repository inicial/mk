using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WpfControlLibrary.Model.RequestJournal;

namespace WpfControlLibrary.Helpers
{
    public static class RequestProblemHelper
    {
        public static RequestProblem GetProblemRequest(DataRow r)
        {
            return new RequestProblem(-1, r.Field<int>("Id"), r.Field<string>("Name"), null);
        }

        public static RequestProblem GetRequestProblem(DataRow r)
        {
            return new RequestProblem(r.Field<int>("RequestId"), r.Field<int>("ProblemId"), r.Field<string>("ProblemName"), r.Field<int?>("USKey"));
        }
    }
}
