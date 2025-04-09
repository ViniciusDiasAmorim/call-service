import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';

  login() {
    console.log('Login com:', this.username, this.password);
    let url = 'http://localhost:5226/api/Auth/login';
    let body = {
      username: this.username,
      password: this.password
    };
    
  }
}
