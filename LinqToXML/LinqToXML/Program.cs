using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Parse(System.IO.File.ReadAllText("../../App_Data/people.xml"));

            List<Person> person = doc.Descendants("Person").Select(item => {
                var nameNode = item.Descendants("Name");
                var addressNode = item.Descendants("Address");
                return new Person()
                {
                    Id = (int)item.Attribute("id"),
                    DOB = DateTime.ParseExact((string)item.Attribute("dob"), "M/d/yyyy", CultureInfo.InvariantCulture),
                    FirstName = nameNode.Select(x => x.Attribute("firstname").Value).First().ToString(),
                    MiddleName = nameNode.Select(x => x.Attribute("middlename").Value).First().ToString(),
                    LastName = nameNode.Select(x => x.Attribute("lastname").Value).First().ToString(),
                    Country = addressNode.Select(x => x.Attribute("country").Value).First().ToString(),
                    City = addressNode.Select(x => x.Attribute("city").Value).First().ToString(),
                    ZipCode = addressNode.Select(x => x.Attribute("zipcode").Value).First().ToString(),
                    State = addressNode.Select(x => x.Attribute("state").Value).First().ToString(),
                    AddressLine1 = addressNode.Select(x => x.Attribute("addressLine1").Value).First().ToString(),
                    AddressLine2 = addressNode.Select(x => x.Attribute("addressLine2").Value).First().ToString(),
                };
            }).ToList();



            List<People> people = doc.Descendants("Person").Select(item1 => new People()
            {
                Id = (int)item1.Attribute("id"),
                DOB = DateTime.ParseExact((string)item1.Attribute("dob"), "M/d/yyyy", CultureInfo.InvariantCulture),
                Name = item1.Descendants("Name").Select(item2 => new Name()
                {
                    FirstName = (string)item2.Attribute("firstname"),
                    LastName = (string)item2.Attribute("lastname"),
                    MiddleName = (string)item2.Attribute("middlename")
                }).First(),
                Address = item1.Descendants("Address").Select(item2 => new Address()
                {
                    Country = (string)item2.Attribute("country"),
                    City = (string)item2.Attribute("city"),
                    State = (string)item2.Attribute("state"),
                    ZipCode = (string)item2.Attribute("zipcode"),
                    AddressLine1 = (string)item2.Attribute("addressLine1"),
                    AddressLine2 = (string)item2.Attribute("addressLine2")
                }).First()
            }).ToList();
        }
    }

    class Person
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }

    class People
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
    }

    class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }

    class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
