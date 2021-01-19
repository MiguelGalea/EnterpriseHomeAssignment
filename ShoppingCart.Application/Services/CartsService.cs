using AutoMapper;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Application.Services
{
    public class CartsService : ICartsService
    {
        private ICartsRepository _cartsRepo;
        private IMapper _mapper;

        public CartsService(ICartsRepository cartsRepository, IMapper mapper)
        {
            _cartsRepo = cartsRepository;
            _mapper = mapper;
        }
    }
}
