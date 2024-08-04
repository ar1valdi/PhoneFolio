import { User } from "../../models/User";

export class AddNewUserRequest {
    username: string;
    password: string;

    constructor (
        username: string,
        password: string,
    ) {
        this.username = username;
        this.password = password;
    }

    static fromUser(user: User): AddNewUserRequest {
        return new AddNewUserRequest(user.username, user.password);
    }
}