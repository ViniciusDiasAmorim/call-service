import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tickets-page',
  standalone: true,
  imports: [CommonModule], // Importado para usar *ngFor e outras diretivas
  templateUrl: './tickets-page.component.html',
  styleUrls: ['./tickets-page.component.css'] // Corrigido para 'styleUrls'
})
export class TicketsPageComponent {
  tickets = [
    {
      id: 1,
      title: 'Teste 1',
      description: 'Descrição do teste 1',
      status: 'open',
      createdAt: new Date(),
      updatedAt: new Date()
    },
    {
      id: 2,
      title: 'Teste 2',
      description: 'Descrição do teste 2',
      status: 'closed',
      createdAt: new Date(),
      updatedAt: new Date()
    }
  ];

  createTicket(): void {
    console.log('Ticket created');
  }

  viewTicket(ticketId: number): void {
    console.log(`Ticket ${ticketId} viewed`);
  }
}