using API.Domain.Entities;
using API.Domain.Errors;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Presenters.Cases;
using API.Presenters.Responses;

namespace API.Domain.Cases
{
    public class GetAllLabelsCase : IGetAllLabelsCase
    {
        private readonly ILabelRepository labelRepository;
        private readonly IFileService fileService;

        public GetAllLabelsCase(ILabelRepository labelRepository, IFileService fileService)
        {
            this.labelRepository = labelRepository;
            this.fileService = fileService;
        }

        public IEnumerable<GetAllLabelsResponse> Execute()
        {
            IEnumerable<Label> labels = labelRepository.GetAll();
            if (!labels.Any())
                throw new BaseEmptyException();

            IEnumerable<GetAllLabelsResponse> responses = labels.Select(x =>
            {
                string path = string.Format("{0}//Photos//Labels//{1}", Directory.GetCurrentDirectory(), x.Photo);
                string image = fileService.FileToBase64(path);

                return new GetAllLabelsResponse
                {
                    LabelId = x.LabelId,
                    Description = x.Description,
                    Image = image,
                };
            });

            return responses;
        }
    }
}
