using AutoMapper;
using Crims.Core.Failures;
using Crims.Data.Dtos;
using Crims.Data.Entities;
using Crims.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Domain.Services
{
    public interface IEstablishmentService
    {
        Task Delete(string id);
        Task<List<EstablishmentDto>> GetAll();
        Task<List<EstablishmentDto>> GetUserEstablishment(string userId);
        Task<EstablishmentDto> Register(EstablishmentDto establishmentDto);
        Task<EstablishmentDto> Update(string id, EstablishmentDto establishment);
    }
    public class EstablishmentService(EstablishmentRepository repository, IMapper mapper) : IEstablishmentService
    {
        private readonly EstablishmentRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<List<EstablishmentDto>> GetAll()
        {
            var list = await repository.GetItems();
            return mapper.Map<List<EstablishmentDto>>(list);
        }

        public async Task<EstablishmentDto> Register(EstablishmentDto establishmentDto)
        {
            var saved = await repository.Add(mapper.Map<EstablishmentEntity>(establishmentDto));
            return mapper.Map<EstablishmentDto>(saved);
        }

        public async Task Delete(string id)
        {
            var establishment = await repository.GetItem(where => where.Id.ToString() == id) ?? throw new NotFoundFailure("Empresa não encontrada.");
            await repository.Delete(establishment);            
        }

        public async Task<EstablishmentDto> Update(string id, EstablishmentDto establishment)
        {
            var establishmentSaved = await repository.GetItem(where => where.Id.ToString() == id) ?? throw new NotFoundFailure("Empresa não encontrada.");
            var newOject = mapper.Map<EstablishmentEntity>(establishment);
            newOject.Id = establishmentSaved.Id;
            establishmentSaved = await repository.Update(newOject);
            return mapper.Map<EstablishmentDto>(establishmentSaved);
        }

        public Task<List<EstablishmentDto>> GetUserEstablishment(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
