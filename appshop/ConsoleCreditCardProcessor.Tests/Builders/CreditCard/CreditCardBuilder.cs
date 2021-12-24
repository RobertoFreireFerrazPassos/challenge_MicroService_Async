using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValueObjects = ApiAppShop.Domain.ValueObjects;
using Events = ApiAppShop.Domain.Events;

namespace ConsoleCreditCardProcessor.Tests.Builders.CreditCard
{
    public class CreditCardImpl : Events.CreditCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string CVV { get; set; }
        public string ExpirationDateMMYYYY { get; set; }
    }

    public class CreditCardBuilder : IBuilder<CreditCardImpl>
    {
        private ValueObjects.CreditCard _creditCard;

        public CreditCardBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._creditCard = new ValueObjects.CreditCard();
        }

        public CreditCardImpl Get()
        {
            var creditCardImpl = new CreditCardImpl()
            {
                CVV = this._creditCard.CVV,
                ExpirationDateMMYYYY = this._creditCard.ExpirationDateMMYYYY,
                Name = this._creditCard.Name,
                Number = this._creditCard.Number
            };

            this.Reset();
            return creditCardImpl;
        }

        public Func<CreditCardImpl> BuildRandomCompleteCreditCard()
        {
            BuildCreditCardCVV();
            BuildCreditCardExpirationDateMMYYYY();
            BuildCreditCardName();
            BuildCreditCardNumber();
            return this.Get;
        }

        public Func<CreditCardImpl> BuildCreditCardCVV()
        {
            var random = new Random();
            int value = random.Next(1000);
            this._creditCard.CVV = value.ToString("000");
            return this.Get;
        }

        public Func<CreditCardImpl> BuildCreditCardExpirationDateMMYYYY()
        {
            var random = new Random();
            var months = new List<string>{"01","02","03","04","05","06","07","08","09","10","11","12"};
            var years = new List<string> {"2020","2021","2022","2023","2024","2025","2026"};

            int indexMonth = random.Next(months.Count);
            string month = months[indexMonth];
            int indexYear = random.Next(years.Count);
            string year = years[indexYear];

            this._creditCard.ExpirationDateMMYYYY = month + year;

            return this.Get;
        }

        public Func<CreditCardImpl> BuildCreditCardName()
        {
            var random = new Random();
            var names = new List<string>{
                "Isabel Diana Mendes",
                "Florência Maximino Alves",
                "Tácito Thalita Cabral",
                "Mateus Xandinho Vale",
                "Virgílio Cesário Esteves",
                "Topsy Wat Cookson",
                "Esmee Clifford Morse",
                "Junior Chance Macey",
                "Ravenna Rafferty Edwards",
                "Braxton Haven Ward"};

            int index = random.Next(names.Count);
            this._creditCard.Name = names[index];

            return this.Get;
        }        

        public Func<CreditCardImpl> BuildCreditCardNumber()
        {
            var random = new Random();
            int[] checkArray = new int[15];

            var cardNum = new int[16];

            for (int d = 14; d >= 0; d--)
            {
                cardNum[d] = random.Next(0, 9);
                checkArray[d] = (cardNum[d] * (((d + 1) % 2) + 1)) % 9;
            }

            cardNum[15] = (checkArray.Sum() * 9) % 10;

            var sb = new StringBuilder();

            for (int d = 0; d < 16; d++)
            {
                sb.Append(cardNum[d].ToString());
            }

            this._creditCard.Number = sb.ToString();

            return this.Get;
        }
    }
}
