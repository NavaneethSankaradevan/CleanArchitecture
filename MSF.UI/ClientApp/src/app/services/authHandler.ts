import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Injectable, Component } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class AuthService {

    constructor(private httpClient:HttpClient){

    }

    login(userEmail:string, password:string) {
        return this.httpClient.post<{accessToken:  string,user:string}>(environment.apiBaseURI+ 'user/getToken', 
            {userEmail, password}).subscribe(res => {
            localStorage.setItem('access_token', res.accessToken);
            localStorage.setItem('user',res.user);
        })
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('access_token');
    }
}