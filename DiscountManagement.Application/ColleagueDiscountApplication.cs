using AutoMapper;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Linq;


namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository colleagueDiscountRepository;
        private readonly IMapper mapper;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository, IMapper mapper)
        {
            this.colleagueDiscountRepository = colleagueDiscountRepository;
            this.mapper = mapper;
        }
        public OperationResult Delete(long id)
        {
            OperationResult op = new();
            var product = colleagueDiscountRepository.Get(id);
            if (product == null)
                return op.Failed(ApplicationMessages.NotFoundMessage);
            else
            {
                colleagueDiscountRepository.Delete(product);
                return op.Succeeded();
            }
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            OperationResult operation = new();
            var data = new ColleagueDiscount(command.Name, command.ProductId, command.DiscountPercentage);
            if (colleagueDiscountRepository.Exists(x => x.Name == data.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            colleagueDiscountRepository.Create(data);
            return operation.Succeeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            OperationResult operation = new();
            var data = colleagueDiscountRepository.Get(command.Id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);

            if (colleagueDiscountRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedMessage);

            data.Edit(command.Name, command.ProductId, command.DiscountPercentage);

            colleagueDiscountRepository.Update(data);

            return operation.Succeeded();
        }

        //doesnt fill products
        public EditColleagueDiscount EditGet(long id)
        {
            var data = colleagueDiscountRepository.Get(id);
            if (data == null)
                return null;
            return mapper.Map<EditColleagueDiscount>(data);
        }

        public OperationResult Remove(long id)
        {
            OperationResult operation = new();
            var data = colleagueDiscountRepository.Get(id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Remove();
            colleagueDiscountRepository.Update(data);
            return operation.Succeeded();


        }

        public OperationResult Restore(long id)
        {
            OperationResult operation = new();
            var data = colleagueDiscountRepository.Get(id);

            if (data == null)
                return operation.Failed(ApplicationMessages.NotFoundMessage);
            data.Restore();
            colleagueDiscountRepository.Update(data);
            return operation.Succeeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel command)
        {

            List<ColleagueDiscountViewModel> data = colleagueDiscountRepository.Search(command);

            if (data == null)
                return new List<ColleagueDiscountViewModel>();

            return data.Select(x => mapper.Map<ColleagueDiscountViewModel>(x)).ToList();
        }

        public bool Exists(long id)
        {
            return colleagueDiscountRepository.Exists(x => x.Id == id);
        }
    }
}
