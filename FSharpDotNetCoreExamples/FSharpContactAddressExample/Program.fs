open System.ComponentModel.DataAnnotations

// Learn more about F# at http://fsharp.org

open System
open System.Text

type Address = {
    streetAddress : string;
    city : string;
    postalCode : string;
    isoCountrySubdivisionL1Code : string;
    isoCountryCode : string;
}

type Contact = {
    firstName : string;
    lastName : string;
    phoneNumber : string;
    mobilePhoneNumber : string;
    faxNumber : string;
    emailAddress : string;
    address : Address;
}

type ContactPreference =
    | Mail
    | Call
    | Email
    | Fax

let hasUsAddress (contactRecord : Contact) : bool =
    (contactRecord.address.isoCountryCode |> String.IsNullOrEmpty)
    || (contactRecord.address.isoCountryCode = "US")

let usStateAbbreviation (contactRecord : Contact) : string =
    if contactRecord.address.isoCountrySubdivisionL1Code |> String.IsNullOrEmpty then
        contactRecord.address.isoCountrySubdivisionL1Code
    else
        contactRecord.address.isoCountrySubdivisionL1Code
            .Replace(contactRecord.address.isoCountryCode, String.Empty)
            .Replace("-", String.Empty)

let usMailingAddressLine2 (contactRecord : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contactRecord.address.city |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.city
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contactRecord.address.isoCountrySubdivisionL1Code |> String.IsNullOrEmpty) then
        sb <- sb.Append (contactRecord |> usStateAbbreviation)
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contactRecord.address.postalCode |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.postalCode

    sb.ToString()

let usMailingAddress (contactRecord : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contactRecord.address.streetAddress |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.streetAddress
    if sb.Length > 0 then sb <- sb.Append ", "
    let address2 = contactRecord |> usMailingAddressLine2
    if not (address2 |> String.IsNullOrEmpty) then
        sb <- sb.Append address2

    sb.ToString()

let globalMailingAddressLine2 (contactRecord : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contactRecord.address.postalCode |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.postalCode
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contactRecord.address.city |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.city
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contactRecord.address.isoCountrySubdivisionL1Code |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.isoCountrySubdivisionL1Code

    sb.ToString()

let globalMailingAddress (contactRecord : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contactRecord.address.streetAddress |> String.IsNullOrEmpty) then
        sb <- sb.Append contactRecord.address.streetAddress
    if sb.Length > 0 then sb <- sb.Append ", "
    let address2 = contactRecord |> globalMailingAddressLine2
    if not (address2 |> String.IsNullOrEmpty) then
        sb <- sb.Append address2

    sb.ToString()

let mailingAddress (contactRecord : Contact) : string =
    contactRecord
    |> if contactRecord |> hasUsAddress then usMailingAddress
        else globalMailingAddress

let phoneNumber (contactRecord : Contact) : string =
    if not (contactRecord.mobilePhoneNumber |> String.IsNullOrEmpty) then
        contactRecord.mobilePhoneNumber
    else
        contactRecord.phoneNumber

let contact (contactRecord : Contact) (contactPreference : ContactPreference) : string =
    match contactPreference with
    | Mail -> contactRecord |> mailingAddress
    | Call -> contactRecord |> phoneNumber
    | Email -> contactRecord.emailAddress
    | Fax -> contactRecord.faxNumber

let display (contactRecord : Contact) =
    //let contactInfo = (contactRecord, Mail) ||> contact
    let contactInfo = contact contactRecord Mail
    printfn "%s" contactInfo

    let contactInfo = contact contactRecord Call
    printfn "%s" contactInfo

    let contactInfo = contact contactRecord Email
    printfn "%s" contactInfo

    let contactInfo = contact contactRecord Fax
    printfn "%s" contactInfo


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    let usContactRecord : Contact = {
        firstName = "Steve";
        lastName = "Rogers";
        phoneNumber = "619-867-5309";
        mobilePhoneNumber = "619-555-2300"
        faxNumber = "619-555-1234";
        emailAddress = "steve.rogers@noemail.noemail";
        address = {
            streetAddress = "1191 2nd Ave";
            city = "Seattle";
            postalCode = "98101";
            isoCountrySubdivisionL1Code = "US-WA";
            isoCountryCode = "US"
        }
    }

    display usContactRecord

    let globalContactRecord : Contact = {
        firstName = "Brian";
        lastName = "Braddock";
        phoneNumber = "01621-562593";
        mobilePhoneNumber = "01621-278393"
        faxNumber = "01621-766213";
        emailAddress = "brian.braddock@noemail.noemail";
        address = {
            streetAddress = "123 Hign St";
            city = "King George's Place, Maldon, Essex";
            postalCode = "CM9 5BZ";
            isoCountrySubdivisionL1Code = "UK-EN";
            isoCountryCode = "UK"
        }
    }

    display globalContactRecord

    0 // return an integer exit code
