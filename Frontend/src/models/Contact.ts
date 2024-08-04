export interface Contact {
    id: number;
    name: string;
    surname: string;
    email: string | undefined;
    password: string | undefined;
    category: string | undefined;
    subcategory: string | undefined;
    phoneNumber: string | undefined;
    birthDate: Date | undefined;
    username: string | undefined;
}