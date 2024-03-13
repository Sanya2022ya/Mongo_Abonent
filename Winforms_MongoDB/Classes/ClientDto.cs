using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winforms_MongoDB.Classes
{
    public class ClientDto
    {
        public ObjectId _id { get; set; }
        public int LShet { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public List<ServicesDto> Services { get; set; }

        public ClientDto() { }

        public ClientDto(ObjectId id, int lShet, string fIO, string address, List<ServicesDto> services)
        {
            _id = id;
            LShet = lShet;
            FIO = fIO;
            Address = address;
            Services = services;
        }
    }
}
