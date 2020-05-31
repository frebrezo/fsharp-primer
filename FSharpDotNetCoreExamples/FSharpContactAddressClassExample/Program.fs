// Learn more about F# at http://fsharp.org

open System
open System.Text

type Address () = class
    member val StreetAddress : string = null with get, set
    member val City : string = null with get, set
    member val PostalCode : string = null with get, set
    member val IsoCountrySubdivisionL1Code : string = null with get, set
    member val IsoCountryCode : string = null with get, set

    member this.HasUsAddress : bool =
        (this.IsoCountryCode |> String.IsNullOrEmpty) || (this.IsoCountryCode = "US")

    member this.UsStateAbbreviation : string =
        if this.IsoCountrySubdivisionL1Code |> String.IsNullOrEmpty then
            this.IsoCountrySubdivisionL1Code
        else
            this.IsoCountrySubdivisionL1Code
                .Replace(this.IsoCountryCode, String.Empty)
                .Replace("-", String.Empty)

    member this.UsMailingAddressLine2 : string =
        let mutable sb = new StringBuilder()

        if not (this.City |> String.IsNullOrEmpty) then
            sb <- sb.Append this.City
        if sb.Length > 0 then sb <- sb.Append " "
        if not (this.IsoCountrySubdivisionL1Code |> String.IsNullOrEmpty) then
            sb <- sb.Append this.UsStateAbbreviation
        if sb.Length > 0 then sb <- sb.Append " "
        if not (this.PostalCode |> String.IsNullOrEmpty) then
            sb <- sb.Append this.PostalCode

        sb.ToString()

    member this.UsMailingAddress : string =
        let mutable sb = new StringBuilder()

        if not (this.StreetAddress |> String.IsNullOrEmpty) then
            sb <- sb.Append this.StreetAddress
        if sb.Length > 0 then sb <- sb.Append ", "
        let address2 = this.UsMailingAddressLine2
        if not (address2 |> String.IsNullOrEmpty) then
            sb <- sb.Append address2

        sb.ToString()

    member this.GlobalMailingAddressLine2 : string =
        let mutable sb = new StringBuilder()

        if not (this.PostalCode |> String.IsNullOrEmpty) then
            sb <- sb.Append this.PostalCode
        if sb.Length > 0 then sb <- sb.Append " "
        if not (this.City |> String.IsNullOrEmpty) then
            sb <- sb.Append this.City
        if sb.Length > 0 then sb <- sb.Append " "
        if not (this.IsoCountrySubdivisionL1Code |> String.IsNullOrEmpty) then
            sb <- sb.Append this.IsoCountrySubdivisionL1Code

        sb.ToString()

    member this.GlobalMailingAddress : string =
        let mutable sb = new StringBuilder()

        if not (this.StreetAddress |> String.IsNullOrEmpty) then
            sb <- sb.Append this.StreetAddress
        if sb.Length > 0 then sb <- sb.Append ", "
        let address2 = this.GlobalMailingAddressLine2
        if not (address2 |> String.IsNullOrEmpty) then
            sb <- sb.Append address2

        sb.ToString()
end

type Contact () = class
    member val FirstName : string = null with get, set
    member val LastName : string = null with get, set
    member val PhoneNumber : string = null with get, set
    member val MobilePhoneNumber : string = null with get, set
    member val FaxNumber : string = null with get, set
    member val EmailAddress : string = null with get, set
    member val Address : Address = new Address() with get, set

    member this.MailingAddress : string =
        if this.Address.HasUsAddress then this.Address.UsMailingAddress
        else this.Address.GlobalMailingAddress

    member this.PreferredPhoneNumber : string =
        if not (this.MobilePhoneNumber |> String.IsNullOrEmpty) then
            this.MobilePhoneNumber
        else
            this.PhoneNumber
end

type ContactPreference =
    | Mail
    | Call
    | Email
    | Fax

let contact (contactObj : Contact) (contactPreference : ContactPreference) : string =
    match contactPreference with
    | Mail -> contactObj.MailingAddress
    | Call -> contactObj.PreferredPhoneNumber
    | Email -> contactObj.EmailAddress
    | Fax -> contactObj.FaxNumber

let display (contactObj : Contact) =
    //let contactInfo = (contactObj, Mail) ||> contact
    let contactInfo = contact contactObj Mail
    printfn "%s" contactInfo

    let contactInfo = contact contactObj Call
    printfn "%s" contactInfo

    let contactInfo = contact contactObj Email
    printfn "%s" contactInfo

    let contactInfo = contact contactObj Fax
    printfn "%s" contactInfo

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    let usContact = new Contact()
    usContact.FirstName <- "Steve"
    usContact.LastName <- "Rogers"
    usContact.PhoneNumber <- "619-867-5309"
    usContact.MobilePhoneNumber <- "619-555-2300"
    usContact.FaxNumber <- "619-555-1234"
    usContact.EmailAddress <- "steve.rogers@noemail.noemail"
    usContact.Address.StreetAddress <- "1191 2nd Ave"
    usContact.Address.City <- "Seattle"
    usContact.Address.PostalCode <- "98101"
    usContact.Address.IsoCountrySubdivisionL1Code <- "US-WA"
    usContact.Address.IsoCountryCode <- "US"

    display usContact

    let globalContact = new Contact()
    globalContact.FirstName <- "Brian"
    globalContact.LastName <- "Braddock"
    globalContact.PhoneNumber <- "01621-562593"
    globalContact.MobilePhoneNumber <- "01621-278393"
    globalContact.FaxNumber <- "01621-766213"
    globalContact.EmailAddress <- "brian.braddock@noemail.noemail"
    globalContact.Address.StreetAddress <- "123 Hign St"
    globalContact.Address.City <- "King George's Place, Maldon, Essex"
    globalContact.Address.PostalCode <- "CM9 5BZ"
    globalContact.Address.IsoCountrySubdivisionL1Code <- "UK-EN"
    globalContact.Address.IsoCountryCode <- "UK"

    display globalContact

    0 // return an integer exit code
