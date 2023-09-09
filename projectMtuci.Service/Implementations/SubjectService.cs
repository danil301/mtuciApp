using Microsoft.EntityFrameworkCore;
using projectMtuci.DAL.Interfaces;
using projectMtuci.Domain.Entity;
using projectMtuci.Domain.Enum;
using projectMtuci.Domain.Response;
using projectMtuci.Domain.ViewModels.Subject;
using projectMtuci.Service.Interfaces;


namespace projectMtuci.Service.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IBaseResponse<Subject>> GetSubject(int id)
        {
            var baseResponse = new BaseResponse<Subject>();
            try
            {
                var subject = await _subjectRepository.Get(id);
                if (subject == null)
                {
                    baseResponse.Description = "Такого объекта не найдено";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = subject;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Subject>()
                {
                    Description = $"[GetSubject] : {ex.Message}"
                };
            }
        }

        public IBaseResponse<IEnumerable<Subject>> GetSubjectByName(string name)
        {
            var baseResponse = new BaseResponse<IEnumerable<Subject>>();
            try
            {
                var subject = _subjectRepository.GetByName(name);
                if (subject == null)
                {
                    baseResponse.Description = "Такого объекта не найдено";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = subject;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Description = $"[GetSubjectByName] : {ex.Message}"
                };
            }
        }

        public IBaseResponse<bool> DeleteSubject(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var subject = _subjectRepository.Get(id);
                if (subject == null)
                {
                    baseResponse.Description = "Такого объекта не найдено";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }

                _subjectRepository.Delete(subject.Result);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteSubject] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<SubjectViewModel>> CreateSubject(SubjectViewModel subjectViewModel)
        {
            var baseResponse = new BaseResponse<SubjectViewModel>();
            try
            {
                var subject = new Subject()
                {
                    Name = subjectViewModel.Name,
                    Course = subjectViewModel.Course,
                    Degree = subjectViewModel.Degree,
                    PathToFile = subjectViewModel.PathToFile
                };
                await _subjectRepository.Create(subject);
            }
            catch (Exception ex)
            {
                return new BaseResponse<SubjectViewModel>()
                {
                    Description = $"[CreateSubject] : {ex.Message}"
                };
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<IEnumerable<Subject>>> GetSubjects()
        {
            var baseResponse = new BaseResponse<IEnumerable<Subject>>();
            try
            {
                var subjects = await _subjectRepository.Select();
                if (subjects.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = subjects;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Subject>>()
                {
                    Description = $"[GetSubjects] : {ex.Message}"
                };
            }
        }
    }
}
