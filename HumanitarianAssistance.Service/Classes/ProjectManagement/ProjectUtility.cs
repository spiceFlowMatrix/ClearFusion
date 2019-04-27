
namespace HumanitarianAssistance.Service.Classes.ProjectManagement
{
    public static class ProjectUtility
    {
        /// <summary>
        /// Format: 00001, 00011 & so on ..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GenerateCode(long id)
        {
            return string.Format("{0:D5}", id);
        }

        /// <summary>
        /// Format: P0001, P0011 & so on ..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GenerateProjectCode(long id)
        {
            //return "P" + string.Format("{0:D4}", id);
            return "P" + id.ToString("D4");
        }

        /// <summary>
        /// Format: Project#1 Job#1: P0001-J001
        ///         Project#1 Job#2: P0001-J002
        ///         
        ///         Project#2 Job#1: P0002-J001
        ///         Project#2 Job#2: P0002-J002
        /// </summary>
        /// <param name="projectCode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GenerateProjectJobCode(string projectCode, long id)
        {
            return projectCode + "-J" + string.Format("{0:D3}", id);
        }

        /// <summary>
        /// Format: Project#1 Budget_Line#1: P0001-B00001
        ///         Project#1 Budget_Line#2: P0001-B00002
        ///         Project#1 Budget_Line#3: P0001-B00003
        ///         
        ///         Project#2 Budget_Line#1: P0002-B00001
        ///         Project#2 Budget_Line#2: P0002-B00002
        ///         Project#2 Budget_Line#3: P0002-B00003
        /// </summary>
        /// <param name="projectCode"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GenerateProjectBudgetLineCode(string projectCode, long id)
        {
            return projectCode + "-B" + string.Format("{0:D5}", id);
        }

    }
}
