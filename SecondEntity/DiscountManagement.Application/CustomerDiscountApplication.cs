using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            this.customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCostumerDiscount command)
        {
            var operation = new OperationResult();
            if (customerDiscountRepository.Exists(x
                => x.ProductId == command.ProductId &&
                x.DiscountRate == command.DiscountRate))
                return operation.Failed(
                    ApplicationMessages.DuplicatedRecord);

            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();

            var customerdiscount = new CustomerDiscount(
                command.ProductId,command.DiscountRate,
                startdate,enddate,command.Reason);
            customerDiscountRepository.Create(customerdiscount);
            customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCostumerDiscount command)
        {
            var operation = new OperationResult();

            var customerdiscount = customerDiscountRepository.
                Get(command.Id);

            if (customerdiscount == null)
                return operation.Failed(ApplicationMessages.
                    RecordNotFound);

            if (customerDiscountRepository.Exists(x =>
            x.ProductId == command.ProductId && x.DiscountRate
            == command.DiscountRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.
                    DuplicatedRecord);

            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();

            customerdiscount.Edit(command.ProductId,
                command.DiscountRate,
                startdate, enddate, command.Reason);
            customerDiscountRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCostumerDiscount GetDetails(long id)
        {
            return customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return customerDiscountRepository.Search(searchModel);
        }
    }
}
