import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { TicketsPageComponent } from './components/tickets-page/tickets-page.component';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {path: 'tickets-page', component: TicketsPageComponent}
];
