using System.Data;

namespace SQLLibrary
{
    public interface IExecute
    {
        /// <summary>
        /// Set data
        /// </summary>
        bool Execute();
        /// <summary>
        /// Get data
        /// </summary>
        bool Execute(out DataTable dataTable);
    }
}
