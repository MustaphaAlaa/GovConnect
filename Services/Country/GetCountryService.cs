﻿using System.Linq.Expressions;
using AutoMapper;
using IRepository;
using IServices;
using IServices.ICountryServices;
using Models.Types;

namespace Services.CountryServices;

public class GetCountryService : IGetCountry

{
    private readonly IGetRepository<Country> _getCountry;
    private IMapper _mapper;


    public GetCountryService(IGetRepository<Country> getCountry, IMapper mapper)
    {
        _getCountry = getCountry;
        _mapper = mapper;
    }


    public async Task<Country?> GetByAsync(Expression<Func<Country?, bool>> predicate)
    {
        return await _getCountry.GetAsync(predicate);
    }
}