﻿using Data;
using Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Home.Api.Extensions
{
    public class Auth : IJwtAuth
    {
        //private readonly string username = "kirtesh";
        //private readonly string password = "Demo1";
        private readonly IRepository<Household> _reposHousehold = new EfCoreRepository<Household>();
        private readonly string key;    
        public Auth(string key)
        {          
            this.key = key;            
        }
   
        public string Authentication(string username, string password)
        {
            var user = _reposHousehold.GetAll();//.Where(x => x.UserName == username & x.Password == password);
            if (user==null)
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //3. Create JETdescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),                        
                    }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}
