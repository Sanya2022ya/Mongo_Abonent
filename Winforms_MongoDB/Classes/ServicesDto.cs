using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms_MongoDB.Classes
{
    public class ServicesDto
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MeasureUnit { get; set; }
        public double Consumption { get; set; }
        public double Rate { get; set;}
        public double Cost { get; set; }
        public double Payment { get; set; }
        public double Debt { get; set; }

        public ServicesDto() { }

        public ServicesDto(int id, string name, string measureUnit, double consumption, 
            double rate, double cost, double payment, double debt)
        {
            Id = id;
            Name = name;
            MeasureUnit = measureUnit;
            Consumption = consumption;
            Rate = rate;
            Cost = cost;
            Payment = payment;
            Debt = debt;
        }
    }
}
