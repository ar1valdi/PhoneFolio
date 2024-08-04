import { Contact } from "../../models/Contact";

export class AddNewContactRequest {
    name: string;
    surname: string;
    email: string;
    password: string;
    category: string;
    subcategory: string | undefined;
    phoneNumber: string;
    birthDate: Date;

    constructor(
        name: string,
        surname: string,
        email: string,
        password: string,
        category: string,
        subcategory: string | undefined,
        phoneNumber: string,
        birthDate: Date
    ) {
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.password = password;
        this.category = category;
        this.subcategory = subcategory;
        this.phoneNumber = phoneNumber;
        this.birthDate = birthDate;
    }

    static fromContact(contact: Contact): AddNewContactRequest {
        return new AddNewContactRequest(
            contact.name,
            contact.surname,
            contact.email ?? '',
            contact.password ?? '',
            contact.category ?? '',
            contact.subcategory,
            contact.phoneNumber ?? '',
            contact.birthDate ?? new Date(0)
        );
    }
}