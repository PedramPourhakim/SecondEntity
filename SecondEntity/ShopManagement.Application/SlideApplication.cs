using _0_Framework.Application;
using ShopManagement.Application.Contracts.SlideContracts;
using ShopManagement.Domain.SliderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository slideRepository;
        private readonly IFileUploader fileUploader;
        public SlideApplication(ISlideRepository slideRepository,IFileUploader fileUploader)
        {
            this.slideRepository = slideRepository;
            this.fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

           var pictureName= fileUploader.Upload(command.Picture, "slides");

            var slide = new Slide(pictureName,
                command.PictureAlt,
                command.PictureTitle, command.Heading, 
                command.Title,command.Text,command.Link,
                command.BtnText);
            slideRepository.Create(slide);
            slideRepository.SaveChanges();
            return operation.Succeeded();
                
        }

        public OperationResult Edit(EditSlide command)
        {
           var Operation = new OperationResult();
            var slide = slideRepository.Get(command.Id);
            if (slide == null)
                return Operation.Failed(ApplicationMessages.RecordNotFound);

            var pictureName = fileUploader.Upload
                (command.Picture, "slides");

            slide.Edit(pictureName,command.PictureAlt,command.PictureTitle,
                command.Heading,command.Title,command.Text,
                command.Link,command.BtnText);
            slideRepository.SaveChanges();
            return Operation.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            return slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            slide.Remove();
            slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            slide.Restore();
            slideRepository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
