﻿using ApiUser.Enums;
using MongoDB.Bson;
using System;

namespace ApiUser.Models
{
    public class UserModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
        public Address Address { get; set; }
        public CreditCardModel CreditCard { get; set; }
}
}
