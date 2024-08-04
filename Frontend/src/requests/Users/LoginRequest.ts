import { User } from "../../models/User";

export class LoginRequest {
    username: string;
    password: string;
    
    constructor(username: string, password: string) {
        this.username = username;
        this.password = password;
    }

    static fromUser(user: User): LoginRequest {
        return new LoginRequest(user.username, user.password);
    }
}