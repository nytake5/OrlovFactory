export class UserAccessToken {
    token: string;
    login: string;

    constructor(accessToken: string, login: string) {
        this.token = accessToken;
        this.login = login;
    }
}