using System.Data;
using System.Threading.Tasks;

namespace HiSUP.Data
{
    public interface IDatabaseRepository
    {
        Task<int> RegisterStudentAsync(string firstName, string lastName, string email, int deptId);
        Task<bool> EnrollInCourseAsync(int studentId, int sectionId);
        Task<DataTable> SearchLibraryAdvancedAsync(string searchString);
        Task<DataTable> GetStudentDashboardDataAsync(int studentId);
        Task<bool> ProcessFeePaymentAsync(int studentId, decimal amount, string paymentMode);
    }
}
