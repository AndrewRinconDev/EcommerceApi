﻿using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class FeatureService : BaseService<Feature>, IFeatureService
    {
        IFeatureRepository _FeatureRepository;
        public FeatureService(IBaseRepository<Feature> repository) : base(repository) {
            _FeatureRepository = (IFeatureRepository)repository;
        }
        
        public async Task<IEnumerable<Feature>> GetByCategory(Guid featureCategoryId)
        {
            return await _FeatureRepository.GetByCategory(featureCategoryId);
        }
    }
}