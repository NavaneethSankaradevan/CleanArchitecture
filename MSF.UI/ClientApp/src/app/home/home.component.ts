import { Component } from '@angular/core';
import { AuthService } from '../services/authHandler';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
   token:string;
  constructor(private auth:AuthService){
       this.token = localStorage.getItem('access_token');
       if(!this.token){
         auth.login(environment.guestUser,environment.guestPwd);
       } 
  }
}

