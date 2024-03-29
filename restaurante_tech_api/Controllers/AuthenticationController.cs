﻿using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using restaurante_tech_api.DTOs;
using restaurante_tech_api.Services.Interfaces;
using System.Threading.Tasks;

namespace restaurante_tech_api.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IUserRepository userRepository, ICryptographyService cryptographyService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<dynamic> CreateUser(CreateUserDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var encodedPassword = _cryptographyService.GetEncodedString(createDTO.password);
                var user = new User(createDTO.name, createDTO.email, encodedPassword);

                await _userRepository.Add(user);
                return Ok();
            }

            return StatusCode(400);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<dynamic> Authorize([FromBody] AuthorizeDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByEmail(createDTO.email);

                if(user == null)
                {
                    return StatusCode(400, "Usuário ou senha inválidos");
                }

                if (!_cryptographyService.VerifyEncodedString(createDTO.password, user.EncodedPassword))
                {
                    return StatusCode(400, "Usuário ou senha inválidos");
                }

                var token = _tokenService.GenerateToken(user);

                return new 
                { 
                    user = new 
                    { 
                        ID = user.ID,
                        Name = user.Name,
                        Email = user.Email
                    },
                    token = token,
                };
            }

            return StatusCode(400);
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => "Autnticado " + User.Identity.Name;
    }
}
