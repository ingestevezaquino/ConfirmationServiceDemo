import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Table } from 'primeng/table';
import { interval, Subscription, timeout } from 'rxjs';
import { Ticket } from 'src/app/models/ticket';
import { TicketsService } from 'src/app/services/tickets/tickets.service';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit, OnDestroy {

  tickets: Ticket[] = [];
  ticketsSubscription: Subscription;
  @ViewChild('ticketsTable') ticketsTable: Table;
  isFacilities: boolean = false;
  subscriberNumber: string = "";
  constructor(private _ticketsService: TicketsService) { }

  ngOnInit(): void {
    this.isFacilities = false;
    this.getTickets();
  }

  ngOnDestroy(): void {
    this.tickets = [];
    this.ticketsSubscription?.unsubscribe();
  }

  getTickets(){
   this.ticketsSubscription = this._ticketsService.getTickets().subscribe(tickets => {
    if(tickets.length > 0){
      this.tickets = tickets;
    }
   }, (error: HttpErrorResponse) =>{
    this.showErrorDialog(error);
   });
  }

  showErrorDialog(error: HttpErrorResponse){
    console.log(error);
  }

  showFacilities(subscriberNumber: string){
    this.subscriberNumber = subscriberNumber;
    this.isFacilities = true;
  }

  goToTicketScreen(){
    this.isFacilities = false;
    this.getTickets();
  }

  clear(ticketsTable: Table){
    ticketsTable.clear();
  }

}
