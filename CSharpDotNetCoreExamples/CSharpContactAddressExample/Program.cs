using System;
using System.Text;

namespace CSharpContactAddressExample
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string IsoCountrySubdivisionL1Code { get; set; }
        public string IsoCountryCode { get; set; }

        public bool HasUsAddress =>
            string.IsNullOrEmpty(IsoCountryCode)
            || IsoCountryCode == "US";

        protected string GetUsStateAbbreviation() =>
            IsoCountrySubdivisionL1Code?.Replace(IsoCountryCode, string.Empty)
                .Replace("-", string.Empty);

        protected string GetUsMailAddressLine2()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(City))
                sb.Append(City);
            if (sb.Length > 0) sb.Append(" ");
            if (!string.IsNullOrEmpty(IsoCountrySubdivisionL1Code))
                sb.Append(GetUsStateAbbreviation());
            if (sb.Length > 0) sb.Append(" ");
            if (!string.IsNullOrEmpty(PostalCode))
                sb.Append(PostalCode);

            return sb.ToString();
        }

        public string GetUsMailingAddress()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(StreetAddress))
                sb.Append(StreetAddress);
            if (sb.Length > 0) sb.Append(", ");
            var address2 = GetUsMailAddressLine2();
            if (!string.IsNullOrEmpty(address2))
                sb.Append(address2);

            return sb.ToString();
        }

        protected string GetGlobalMailAddressLine2()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(PostalCode))
                sb.Append(PostalCode);
            if (sb.Length > 0) sb.Append(" ");
            if (!string.IsNullOrEmpty(City))
                sb.Append(City);
            if (sb.Length > 0) sb.Append(" ");
            if (!string.IsNullOrEmpty(IsoCountrySubdivisionL1Code))
                sb.Append(IsoCountrySubdivisionL1Code);

            return sb.ToString();
        }

        public string GetGlobalMailingAddress()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(StreetAddress))
                sb.Append(StreetAddress);
            if (sb.Length > 0) sb.Append(", ");
            var address2 = GetGlobalMailAddressLine2();
            if (!string.IsNullOrEmpty(address2))
                sb.Append(address2);

            return sb.ToString();
        }
    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public Address Address { get; set; }

        public string GetMailingAddress()
        {
            if (Address?.HasUsAddress ?? false)
                return Address?.GetUsMailingAddress();
            return Address?.GetGlobalMailingAddress();
        }

        public string GetPhoneNumber()
        {
            if (!string.IsNullOrEmpty(MobilePhoneNumber))
                return MobilePhoneNumber;
            return PhoneNumber;
        }
    }

    public enum ContactPreference
    {
        Mail,
        Call,
        Email,
        Fax
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var usContact = new Contact
            {
                FirstName = "Steve",
                LastName = "Rogers",
                PhoneNumber = "619-867-5309",
                MobilePhoneNumber = "619-555-2300",
                FaxNumber = "619-555-1234",
                EmailAddress = "steve.rogers@noemail.noemail",
                Address = new Address
                {
                    StreetAddress = "1191 2nd Ave",
                    City = "Seattle",
                    PostalCode = "98101",
                    IsoCountrySubdivisionL1Code = "US-WA",
                    IsoCountryCode = "US"
                }
            };

            DisplayContact(usContact);

            var globalContact = new Contact
            {
                FirstName = "Brian",
                LastName = "Braddock",
                PhoneNumber = "01621-562593",
                MobilePhoneNumber = "01621-278393",
                FaxNumber = "01621-766213",
                EmailAddress = "brian.braddock@noemail.noemail",
                Address = new Address
                {
                    StreetAddress = "123 Hign St",
                    City = "King George's Place, Maldon, Essex",
                    PostalCode = "CM9 5BZ",
                    IsoCountrySubdivisionL1Code = "UK-EN",
                    IsoCountryCode = "UK"
                }
            };

            DisplayContact(globalContact);
        }

        public static string GetContactInfo(Contact contact, ContactPreference contactPreference)
        {
            switch (contactPreference)
            {
                case ContactPreference.Mail:
                    return contact?.GetMailingAddress();
                case ContactPreference.Call:
                    return contact?.GetPhoneNumber();
                case ContactPreference.Email:
                    return contact?.EmailAddress;
                case ContactPreference.Fax:
                    return contact?.FaxNumber;
                default:
                    return null;
            }
        }

        public static void DisplayContact(Contact contact)
        {
            string contactInfo = null;

            contactInfo = GetContactInfo(contact, ContactPreference.Mail);
            Console.WriteLine(contactInfo);

            contactInfo = GetContactInfo(contact, ContactPreference.Call);
            Console.WriteLine(contactInfo);

            contactInfo = GetContactInfo(contact, ContactPreference.Email);
            Console.WriteLine(contactInfo);

            contactInfo = GetContactInfo(contact, ContactPreference.Fax);
            Console.WriteLine(contactInfo);
        }
    }
}
