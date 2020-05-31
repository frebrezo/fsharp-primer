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

let hasUsAddress (contact' : Contact) : bool =
    (contact'.address.isoCountryCode |> String.IsNullOrEmpty)
    || (contact'.address.isoCountryCode = "US")

let usStateAbbreviation (contact' : Contact) : string =
    if contact'.address.isoCountrySubdivisionL1Code |> String.IsNullOrEmpty then
        contact'.address.isoCountrySubdivisionL1Code
    else
        contact'.address.isoCountrySubdivisionL1Code
            .Replace(contact'.address.isoCountryCode, String.Empty)
            .Replace("-", String.Empty)

let usMailingAddressLine2 (contact' : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contact'.address.city |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.city
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contact'.address.isoCountrySubdivisionL1Code |> String.IsNullOrEmpty) then
        sb <- sb.Append (contact' |> usStateAbbreviation)
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contact'.address.postalCode |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.postalCode

    sb.ToString()

let usMailingAddress (contact' : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contact'.address.streetAddress |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.streetAddress
    if sb.Length > 0 then sb <- sb.Append ", "
    let address2 = contact' |> usMailingAddressLine2
    if not (address2 |> String.IsNullOrEmpty) then
        sb <- sb.Append address2

    sb.ToString()

let globalMailingAddressLine2 (contact' : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contact'.address.postalCode |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.postalCode
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contact'.address.city |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.city
    if sb.Length > 0 then sb <- sb.Append " "
    if not (contact'.address.isoCountrySubdivisionL1Code |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.isoCountrySubdivisionL1Code

    sb.ToString()

let globalMailingAddress (contact' : Contact) : string =
    let mutable sb = new StringBuilder()

    if not (contact'.address.streetAddress |> String.IsNullOrEmpty) then
        sb <- sb.Append contact'.address.streetAddress
    if sb.Length > 0 then sb <- sb.Append ", "
    let address2 = contact' |> globalMailingAddressLine2
    if not (address2 |> String.IsNullOrEmpty) then
        sb <- sb.Append address2

    sb.ToString()

let mailingAddress (contact' : Contact) : string =
    contact'
    |> if contact' |> hasUsAddress then usMailingAddress
        else globalMailingAddress

let phoneNumber (contact' : Contact) : string =
    if not (contact'.mobilePhoneNumber |> String.IsNullOrEmpty) then
        contact'.mobilePhoneNumber
    else
        contact'.phoneNumber

let contact (contact' : Contact) (contactPreference : ContactPreference) : string =
    match contactPreference with
    | Mail -> contact' |> mailingAddress
    | Call -> contact' |> phoneNumber
    | Email -> contact'.emailAddress
    | Fax -> contact'.faxNumber

let display (contact' : Contact) =
    //let contactInfo = (contact', Mail) ||> contact
    let contactInfo = contact contact' Mail
    printfn "%s" contactInfo

    let contactInfo = contact contact' Call
    printfn "%s" contactInfo

    let contactInfo = contact contact' Email
    printfn "%s" contactInfo

    let contactInfo = contact contact' Fax
    printfn "%s" contactInfo


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    let usContact : Contact = {
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

    display usContact

    let globalContact : Contact = {
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

    display globalContact

    0 // return an integer exit code
