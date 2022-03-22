import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { TicketsComponent } from './components/tickets/tickets.component';

const routes: Routes = [
  {path: '', component: TicketsComponent, pathMatch: 'full' },
  {path: 'tickets', component: TicketsComponent },
  {path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
