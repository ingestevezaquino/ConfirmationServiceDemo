import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TicketsComponent } from './tickets.component';
import { PrimeModule } from 'src/app/prime/prime.module';
import { FacilitiesComponent } from './facilities/facilities.component';



@NgModule({
  declarations: [
    TicketsComponent,
    FacilitiesComponent
  ],
  imports: [
    CommonModule,
    PrimeModule
  ]
})
export class TicketsModule { }
