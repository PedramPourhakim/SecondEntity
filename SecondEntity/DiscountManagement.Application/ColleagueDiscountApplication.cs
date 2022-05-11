using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository ColleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository ColleagueDiscountRepository)
        {
            this.ColleagueDiscountRepository = ColleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if (ColleagueDiscountRepository.Exists(x => x.ProductId
             == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var colleaguediscount = new ColleagueDiscount(
                command.ProductId,command.DiscountRate);
            ColleagueDiscountRepository.Create(colleaguediscount);
            ColleagueDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleaguediscount = ColleagueDiscountRepository
                .Get(command.Id);
            if (colleaguediscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (ColleagueDiscountRepository.Exists(
                x => x.ProductId== command.ProductId && 
             x.DiscountRate == command.DiscountRate &&
             x.Id !=command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            colleaguediscount.Edit(command.ProductId, command.DiscountRate);

            ColleagueDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return ColleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleaguediscount = ColleagueDiscountRepository
                .Get(id);
            if (colleaguediscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

        
            colleaguediscount.Remove();

            ColleagueDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleaguediscount = ColleagueDiscountRepository
                .Get(id);
            if (colleaguediscount == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);


            colleaguediscount.Restore();

            ColleagueDiscountRepository.SaveChanges();

            return operation.Succeeded();
        }

        

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return ColleagueDiscountRepository.Search(searchModel);
        }
    }
}
