using projectMtuci.Domain.Entity;
using projectMtuci.Domain.Response;
using projectMtuci.Domain.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Service.Interfaces
{
    public interface ISubjectService
    {
        Task<IBaseResponse<IEnumerable<Subject>>> GetSubjects();

        Task<IBaseResponse<Subject>> GetSubject(int id);

        IBaseResponse<IEnumerable<Subject>> GetSubjectByName(string name);

        IBaseResponse<bool> DeleteSubject(int id);

        Task<IBaseResponse<SubjectViewModel>> CreateSubject(SubjectViewModel subjectViewModel);
    }
}
